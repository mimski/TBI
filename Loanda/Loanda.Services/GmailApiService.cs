using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loanda.Services
{
    public class GmailApiService
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API .NET Quickstart";

        internal static void GetEmailsFromGmail()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");

            var emailListRequest = service.Users.Messages.List("tbiloanda@gmail.com");

            var emailListRespons = emailListRequest.ExecuteAsync().Result;

            var from = string.Empty;
            var receivedDate = string.Empty;
            var subject = string.Empty;
            //byte[] attachments = null;
            byte attachmentSize = 0;
            var countOfAttachments = 0;
            double? totalAttachmentsSize = 0;
            var body = string.Empty;


            foreach (var email in emailListRespons.Messages)
            {
                var emailInfoRequest = service.Users.Messages.Get("tbiloanda@gmail.com", email.Id);

                var emailInfoResponse = emailInfoRequest.ExecuteAsync().Result;

                var emailText = emailInfoResponse.Snippet;

                subject = emailInfoResponse.Payload.Headers.FirstOrDefault(e => e.Name.Equals("Subject")).Value;
                from = emailInfoResponse.Payload.Headers.FirstOrDefault(e => e.Name.Equals("From")).Value;

                // TODO: the time zone calculations must be consider
                receivedDate = emailInfoResponse.Payload.Headers.FirstOrDefault(e => e.Name.Equals("Date")).Value.ToString().Split(';').ToList().Last().Trim();

                // Email do not have attachments
                if (emailInfoResponse.Payload.Parts[0].MimeType.Contains("text/plain"))
                {
                    var tuksam = string.Empty;
                    body = emailInfoResponse.Payload.Parts.First().Body.Data;

                    //TODO: Decode string

                    var decodedBody = Base64Decode(body);

                }
                else // Email have attachments
                {
                    body = emailInfoResponse.Payload.Parts.First().Parts.First().Body.Data;

                    var decodedBody = Base64Decode(body);

                    //TODO: Decode string

                    var attachments = emailInfoResponse.Payload.Parts.Skip(1).ToList();

                    foreach (var attachment in attachments)
                    {
                        totalAttachmentsSize += double.Parse(attachment.Body.Size.ToString());
                    }
                    countOfAttachments = attachments.Count;

                    totalAttachmentsSize /= 1024;

                    var tuksam = string.Empty;
                }
                totalAttachmentsSize = 0;
                //attachments = emailInfoResponse.Payload.Parts..Headers.FirstOrDefault(e => e.Name.Equals("Subject")).Value;
                var tuk = string.Empty;
            }
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
