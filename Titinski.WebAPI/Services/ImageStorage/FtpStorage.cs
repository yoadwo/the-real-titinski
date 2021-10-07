using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net;
using Titinski.WebAPI.Interfaces.Storage;
using Titinski.WebAPI.Models;

namespace Titinski.WebAPI.Services.ImageStorage
{
    public class FtpStorage : IImageStorage
    {
        private readonly IOptions<AppSettings.FtpConfig> _ftpConfig;
        private readonly ILogger<FtpStorage> _logger;

        private readonly string IMAGES_DIR_ABSOLUTE_PATH;

        public FtpStorage(
            IOptions<AppSettings.FtpConfig> ftpConfig,
            ILogger<FtpStorage> logger
            )
        {
            _ftpConfig = ftpConfig;
            _logger = logger;

            IMAGES_DIR_ABSOLUTE_PATH = _ftpConfig.Value.Address + _ftpConfig.Value.RootPath + _ftpConfig.Value.ImagesPath;
        }
        
        // from https://docs.microsoft.com/en-us/dotnet/framework/network-programming/how-to-upload-files-with-ftp
        public string SaveRant(RantPost rant)
        {
            
            var fileName = $"/{DateTime.Now.ToString("s")}.{rant.ImageFile.FileName}";

            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(IMAGES_DIR_ABSOLUTE_PATH + fileName);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            _logger.LogDebug("Upload request created. File name: " + fileName);

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(_ftpConfig.Value.Username, _ftpConfig.Value.Password);

            // Copy the contents of the file to the request stream.
            request.ContentLength = rant.ImageFile.Length;

            try
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    rant.ImageFile.CopyTo(requestStream);
                    _logger.LogInformation("Uploading File " + rant.ImageFile.FileName);
                }
            }
            catch (WebException e)
            {
                if (e.Response is FtpWebResponse)
                {
                    _logger.LogError(e, (e.Response as FtpWebResponse).StatusDescription);
                }
                else
                {
                    _logger.LogError(e, e.Message);
                }
                throw;
            }            
            

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                _logger.LogInformation($"Upload File Complete, status description: '{response.StatusDescription}'");
            }

            return fileName;
        }

        // from https://docs.microsoft.com/en-us/dotnet/framework/network-programming/how-to-download-files-with-ftp
        public async System.Threading.Tasks.Task<Stream> LoadRantAsync(string path)
        {
            _logger.LogDebug("FTP service: get image from path '{0}'", path);
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(IMAGES_DIR_ABSOLUTE_PATH + path);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(_ftpConfig.Value.Username, _ftpConfig.Value.Password);

            MemoryStream memoryStream = new MemoryStream();
            try
            {
                _logger.LogInformation("Creating connection to FTP server");
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();                

                await response.GetResponseStream().CopyToAsync(memoryStream);
                _logger.LogInformation($"Logger: Download Complete, status {response.StatusDescription}");
                response.Close();
            }
            catch (Exception e)
            {
                memoryStream = null;
                _logger.LogError(e, "Error getting FTP image");
            }
            
            return memoryStream;
        }
    }
}
