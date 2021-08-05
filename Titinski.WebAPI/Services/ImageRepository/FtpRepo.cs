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
        private const string FTP_IMAGES_DIR = "//home/u520350406/domains/wolfsthal.org/trt1/the_real_titinski/uploaded/images";
        private readonly IOptions<AppSettings.FtpConfig> _ftpConfig;

        public FtpRepo(
            IOptions<AppSettings.FtpConfig> ftpConfig
            )
        {
            _ftpConfig = ftpConfig;
        }
        // from https://docs.microsoft.com/en-us/dotnet/framework/network-programming/how-to-upload-files-with-ftp
        public async void AddRant(RantPost rant)
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(_ftpConfig.Value.Address + FTP_IMAGES_DIR);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(_ftpConfig.Value.Username, _ftpConfig.Value.Password);

            // Copy the contents of the file to the request stream.
            byte[] fileContents;
            using (var ms = new System.IO.MemoryStream())
            {
                await rant.ImageFile.CopyToAsync(ms);
                fileContents = ms.ToArray();
                /*string s = Convert.ToBase64String(fileContents);
                r = new Models.Rant
                {
                    Description = newPost.Description,
                    ImageBase64 = s
                };*/
            }

            
            /*using (StreamReader sourceStream = new StreamReader("testfile.txt"))
            {
                fileContents = System.Text.Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            }*/

            request.ContentLength = fileContents.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
            }
        }

        public Rant GetRant(string id)
        {
            throw new NotImplementedException();
        }
    }
}
