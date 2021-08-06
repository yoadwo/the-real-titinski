﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Titinski.WebAPI.Models;

namespace Titinski.WebAPI.Services.ImageRepository
{
    public class FtpRepo : IImageRepo
    {
        private readonly IOptions<AppSettings.FtpConfig> _ftpConfig;
        private readonly ILogger<FtpRepo> _logger;

        public FtpRepo(
            IOptions<AppSettings.FtpConfig> ftpConfig,
            ILogger<FtpRepo> logger
            )
        {
            _ftpConfig = ftpConfig;
            _logger = logger;
        }
        // from https://docs.microsoft.com/en-us/dotnet/framework/network-programming/how-to-upload-files-with-ftp
        public void AddRant(RantPost rant)
        {
            var address = _ftpConfig.Value.Address + _ftpConfig.Value.RootPath + _ftpConfig.Value.ImagesPath;
            var fileName = $"/{DateTime.Now.ToString("s")}.{rant.ImageFile.FileName}";

            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(address + fileName);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            _logger.LogInformation("Upload request created. File name: " + fileName);

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(_ftpConfig.Value.Username, _ftpConfig.Value.Password);

            // Copy the contents of the file to the request stream.
            request.ContentLength = rant.ImageFile.Length;

            try
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    rant.ImageFile.CopyTo(requestStream);
                }
            }
            catch (System.Net.WebException e)
            {
                if (e.Response is FtpWebResponse)
                {
                    _logger.LogError(e, (e.Response as FtpWebResponse).StatusDescription);
                }
                throw;
            }
            

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                _logger.LogInformation($"Upload File Complete, status description: '{response.StatusDescription}'");
            }
        }

        public Rant GetRant(string id)
        {
            throw new NotImplementedException();
        }
    }
}
