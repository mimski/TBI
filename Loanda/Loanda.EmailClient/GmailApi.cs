using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Loanda.EmailClient.Contracts;
using Loanda.Services.Contracts;
using Loanda.Services.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loanda.EmailClient
{
    public class GmailApi : IGmailApi
    {
        const string SERVICEACCOUNTEMAIL = "tbiloanda@gmail.com";

        private readonly IEmailService emailService;

        public GmailApi(IEmailService emailService)
        {
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService)); ;
        }

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        static string[] Scopes = { GmailService.Scope.GmailReadonly, GmailService.Scope.GmailModify };

        public async Task GetEmailsFromGmailAsync()
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
                HttpClientInitializer = credential
            });

            // Define parameters of request.
            UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");

            var emailListRequest = service.Users.Messages.List(SERVICEACCOUNTEMAIL);

            // Get emails only form inbox
            emailListRequest.LabelIds = "UNREAD";

            var emailListRespons = await emailListRequest.ExecuteAsync();

            var subject = string.Empty;
            var body = string.Empty;
            var receivedDate = DateTime.MaxValue;
            var dateEmail = string.Empty;
            var senderEmail = string.Empty;
            var senderName = string.Empty;
            byte attachmentSize = 0;
            var countOfAttachments = 0;
            double? totalAttachmentsSize = 0;

            if (emailListRespons != null && emailListRespons.Messages != null && emailListRespons.Messages.Any())
            {
                foreach (var email in emailListRespons.Messages)
                {

                    var emailInfoRequest = service.Users.Messages.Get("tbiloanda@gmail.com", email.Id);

                    var emailInfoResponse = await emailInfoRequest.ExecuteAsync();

                    var gmailId = emailInfoResponse?.Id;

                    var header = emailInfoResponse?.Payload?.Headers;

                    subject = header?.FirstOrDefault(e => e.Name.Equals("Subject"))?.Value ?? string.Empty;

                    var from = header?.FirstOrDefault(e => e.Name.Equals("From"))
                        ?.Value
                        .Split(new[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList();

                    senderEmail = from
                        .Last()
                        .Trim();

                    senderName = from
                        .First()
                        .Trim();


                    // TODO: the time zone calculations must be consider
                    //receivedDate = emailInfoResponse.Payload.Headers.FirstOrDefault(e => e.Name.Equals("Date")).Value.ToString().Split(';').ToList().Last().Trim();


                    // TODO: Check if take the correct date
                    dateEmail = header?.FirstOrDefault(e => e.Name.Equals("Date"))?.Value.Split(new[] { '-' })[0].Trim();

                    DateTime.TryParse(dateEmail, out receivedDate);

                    //receivedDate = header?.FirstOrDefault(e => e.Name.Equals("Date"))?.Value.Split(';').ToList().Last().Trim();

                    // Email do not have attachments
                    if (emailInfoResponse.Payload.Parts[0].MimeType.Contains("text/plain"))
                    {
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
                    }
                    totalAttachmentsSize = 0;
                    //attachments = emailInfoResponse.Payload.Parts..Headers.FirstOrDefault(e => e.Name.Equals("Subject")).Value;

                    var emailDto = new EmailDTO
                    {
                        Subject = subject,
                        Body = body,
                        DateReceived = receivedDate,
                        SenderName = senderName,
                        SenderEmail = senderEmail,
                        GmailEmailId = gmailId
                    };

                    await this.emailService.CreateAsync(emailDto);

                    var markAsReadRequest = new ModifyMessageRequest { RemoveLabelIds = new[] { "UNREAD" } };
                    await service.Users.Messages.Modify(markAsReadRequest, SERVICEACCOUNTEMAIL, emailInfoResponse.Id).ExecuteAsync();
                }
            }
        }

        public static string Base64Decode(string base64EncodedData)
        {
            base64EncodedData = base64EncodedData.Replace('-', '+');
            base64EncodedData = base64EncodedData.Replace('_', '/');
            byte[] encode = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(encode);
        }
    }
}
