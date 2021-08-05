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
        private const string FTP_IMAGES_DIR = "/the_real_titinski/uploaded/images";
        private readonly IOptions<AppSettings.FtpConfig> _ftpConfig;

        public FtpRepo(
            IOptions<AppSettings.FtpConfig> ftpConfig
            )
        {
            _ftpConfig = ftpConfig;
        }
        // from https://docs.microsoft.com/en-us/dotnet/framework/network-programming/how-to-upload-files-with-ftp
        public void AddRant(RantPost rant)
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(_ftpConfig.Value.Address + FTP_IMAGES_DIR + $"/{rant.ImageFile.Name}_{DateTime.Now.ToString("s")}");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(_ftpConfig.Value.Username, _ftpConfig.Value.Password);

            // Copy the contents of the file to the request stream.
            byte[] fileContents;
            
            request.ContentLength = rant.ImageFile.Length;


            using (Stream requestStream = request.GetRequestStream())
            {
                //requestStream.Write(fileContents, 0, fileContents.Length);
                rant.ImageFile.CopyTo(requestStream);
            }

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                Console.WriteLine(reader.ReadToEnd());

                Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
            }
        }

        public Rant GetRant(string id)
        {
            throw new NotImplementedException();
        }
    }
}
