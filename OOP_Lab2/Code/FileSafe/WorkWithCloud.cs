using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;

namespace OOP_Lab2.FileSafe
{
    class WorkWithCloud
    {

        private static DriveService GetService()
        {
            var tokenResponse = new TokenResponse
            {
                AccessToken = "ya29.a0AZYkNZgFtrEGQuiENFrZrKI8AZ0hwQ41FukU6KsmyNc9hX6xGoA1ZRzJ20C6ObxSsHnXosS6PvcivlmzJ8PoEpzz-I-gN9uk04VuBZqYNSkv8Vf7RkGQsWkcK3UB6ayHxPA781qhrWs37bfHxbI5bhgFXOoscMD7R32ctBuiaCgYKAesSARASFQHGX2MiKQh1awinzssbNNh0MaDRWA0175",
                RefreshToken = "1//04qKWPCiQQGndCgYIARAAGAQSNwF-L9IrkgiF7DM4sl6TREWRXM9NIu7zC2q1P77XXmYum9oDJoFbbDQ7TWTmU4CmRWI4tGqzMZ4",
            };


            var applicationName = "Lab";// Use the name of the project in Google Cloud
            var username = "pavel1zd@gmail.com"; // Use your email


            var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "1049876216335-9l4p6hc4gd2c039ioelj8n0uneo6ceq1.apps.googleusercontent.com",
                    ClientSecret = "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"
                },
                Scopes = new[] { DriveService.Scope.Drive },
                DataStore = new FileDataStore(applicationName)
            });


            var credential = new UserCredential(apiCodeFlow, username, tokenResponse);



            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });
            return service;
        }

        public string UploadFile(string fileName, int type, string folder, string fileDescription)
        {
            DriveService service = GetService();
            string fileMime="";
            var t = GetFiles(folder);
            foreach(var a in t)
            {
                if (a.Name == fileName) return "-1";
            }
            Stream file= new FileStream("mem.json", FileMode.Open, FileAccess.Read); 
            if (System.IO.File.Exists("C:/Users/pavel/source/repos/OOP_Lab2/OOP_Lab2/bin/Debug/net8.0"+fileName))
            {
                file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }
            else
            {
                file.Close();
                System.IO.File.WriteAllText(fileName, "");
                file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
               // System.IO.File.Delete(fileName);
                if (type == 0)
                {
                    fileMime = "text/plain";
                }
                if (type == 1)
                {
                    fileMime = "text/json";
                }
                if (type == 2)
                {
                    fileMime = "text/md";
                }
                if (type == 3)
                {
                    fileMime = "text/xml";
                }
                var driveFiler = new Google.Apis.Drive.v3.Data.File();
                driveFiler.Name = fileName;
                // driveFile.Description = "111";
                driveFiler.MimeType = fileMime;
                driveFiler.Parents = new string[] { folder };
                //service.Files.Get
                //
                // service.Files.Update(driveFile, file, driveFile.MimeType,);
                var requestr = service.Files.Create(driveFiler, file, driveFiler.MimeType);
                requestr.Fields = "id";

                var responser = requestr.Upload();
                file.Close();
                System.IO.File.Delete(fileName);
                return requestr.ResponseBody.Id;
            }
            //Stream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            if (type == 0)
            {
                fileMime = "text/plain";
            }
            if (type == 1)
            {
                fileMime = "text/json";
            }
            if (type == 2)
            {
                fileMime = "text/md";
            }
            if (type == 3)
            {
                fileMime = "text/xml";
            }
            if (type == 4)
            {
                fileMime = "text/rtf";
            }
            var driveFile = new Google.Apis.Drive.v3.Data.File();
            driveFile.Name = fileName;
           // driveFile.Description = "111";
            driveFile.MimeType = fileMime;
            driveFile.Parents = new string[] { folder };
            //service.Files.Get
            //
           // service.Files.Update(driveFile, file, driveFile.MimeType,);
            var request = service.Files.Create(driveFile, file, driveFile.MimeType);
            request.Fields = "id";

            var response = request.Upload();
            if (response.Status != UploadStatus.Completed)
                throw response.Exception;
            file.Close();
            return request.ResponseBody.Id;
        }

        public IEnumerable<Google.Apis.Drive.v3.Data.File> GetFiles(string FolderId)
        {
            var service = GetService();
            
            var fileList = service.Files.List();
            fileList.Q = $"mimeType!='application/vnd.google-apps.folder' and '{FolderId}' in parents";
            fileList.Fields = "nextPageToken, files(id, name, size, mimeType)";

            var result = new List<Google.Apis.Drive.v3.Data.File>();
            string pageToken = null;
            do
            {
                fileList.PageToken = pageToken;
                var filesResult = fileList.Execute();
                var files = filesResult.Files;
                pageToken = filesResult.NextPageToken;
               
                result.AddRange(files);
            } while (pageToken != null);
        

        return result;
        }
        public string GetFileText(string FolderId,string fileId)
        {
            var service = GetService();
            var request = service.Files.Get(fileId);
            using (var stream = new MemoryStream())
            {
                request.Download(stream); // По умолчанию использует alt=media
                stream.Position = 0;

                // Для текстовых файлов:
                using (var reader = new StreamReader(stream))
                {
                    string content = reader.ReadToEnd();
                    return content;
                }

                // Для бинарных файлов:
                // File.WriteAllBytes("output.png", stream.ToArray());
            }
            
        }
        public string UpdateFile(string fileName, int type, string folder, string fileDescription,string fileId)
        {
            var t = GetFiles(folder);
            foreach (var a in t)
            {
                if (a.Name == fileName) fileId = a.Id;
            }
            DriveService service = GetService();
            string fileMime = "";
            Stream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            if (type == 0)
            {
                fileMime = "text/plain";
            }
            if (type == 1)
            {
                fileMime = "text/json";
            }
            if (type == 2)
            {
                fileMime = "text/md";
            }
            if (type == 3)
            {
                fileMime = "text/xml";
            }
            if (type == 4)
            {
                fileMime = "text/rtf";
            }
            var driveFile = new Google.Apis.Drive.v3.Data.File();
            //driveFile.Name = fileName;
            //driveFile.Description = "111";
            driveFile.MimeType = fileMime;
            var fileMetadata = service.Files.Get(fileId).Execute();
            driveFile.Name = fileMetadata.Name;
            //else driveFile.Name = Console.ReadLine()+".txt";
                //driveFile.addParents = new string[] { "1YjkitbfUoFkCHissqIBfZR9UTjfuqxQ8" };
                //

            var request = service.Files.Update(driveFile, fileId, file, driveFile.MimeType);
            request.Fields = "id, name, webViewLink";

            var response = request.Upload();
            if (response.Status != UploadStatus.Completed)
                throw response.Exception;
            file.Close();
            return driveFile.Name;
        }
        public void Delete(string fileId,string folder)
        {
            var service = GetService();
            var t = GetFiles(folder);
            foreach (var a in t)
            {
                if (a.Id == fileId)
                {
                    var command = service.Files.Delete(fileId);
                    var result = command.Execute();
                }
            }
            //var command = service.Files.Delete(fileId);
            //var result = command.Execute();
        }
    }
}
