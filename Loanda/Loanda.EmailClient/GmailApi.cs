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
        private readonly IEmailAttachmentService emailAttachmentService;

        public GmailApi(IEmailService emailService, IEmailAttachmentService emailAttachmentService)
        {
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.emailAttachmentService = emailAttachmentService ?? throw new ArgumentNullException(nameof(emailAttachmentService));
        }

        static string[] Scopes = { GmailService.Scope.GmailReadonly, GmailService.Scope.GmailModify };

        public async Task GetUnreadEmailsFromGmailAsync()
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

            //var emailListRequest = service.Users.Messages.List(SERVICEACCOUNTEMAIL);

            var emailThreads = service.Users.Threads.List(SERVICEACCOUNTEMAIL);

            // Get emails only form inbox which are unread
            var labels = new Google.Apis.Util.Repeatable<string>(new List<string> { "INBOX", "UNREAD" });
            emailThreads.LabelIds = labels;
            emailThreads.IncludeSpamTrash = false;
            emailThreads.MaxResults = 5000;

            //emailThreads.Q = "has:attachment";
            //emailThreads.Q = "in:anywhere";

            var emailThreadResult = await emailThreads.ExecuteAsync();

            if (emailThreadResult != null && emailThreadResult.Threads != null && emailThreadResult.Threads.Any())
            {
                foreach (var thread in emailThreadResult.Threads)
                {
                    var tdata = await service.Users.Threads.Get(SERVICEACCOUNTEMAIL, thread.Id).ExecuteAsync();

                    var gmailId = tdata?.Id;

                    // Loop through each email and get what fields you want...
                    foreach (var emailInfoResponse in tdata.Messages)
                    {
                        if (emailInfoResponse != null)
                        {
                            string body = string.Empty;

                            var dateReceived = emailInfoResponse?.Payload?.Headers?.FirstOrDefault(s => s.Name == "Date")?.Value ?? string.Empty;
                            var from = emailInfoResponse?.Payload?.Headers?.FirstOrDefault(s => s.Name == "From")?.Value ?? string.Empty;
                            var subject = emailInfoResponse?.Payload?.Headers?.FirstOrDefault(s => s.Name == "Subject")?.Value ?? string.Empty;

                            if (dateReceived != "" && from != "")
                            {
                                try
                                {
                                    if (emailInfoResponse.Payload.Parts == null && emailInfoResponse.Payload.Body != null)
                                    {
                                        body = emailInfoResponse.Payload.Body.Data;
                                    }
                                    else
                                    {
                                        if (emailInfoResponse.Payload.Parts[0].Body.Data == null)
                                        {
                                            body = GetNestedParts(emailInfoResponse.Payload.Parts, "");
                                        }
                                        else
                                        {
                                            body = emailInfoResponse.Payload.Parts[0].Body.Data;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                                // Now you have the data you want...                         
                            }

                            var emailDto = new EmailDTO
                            {
                                Subject = subject,
                                Body = body,
                                DateReceived = dateReceived,
                                From = from,
                                GmailEmailId = gmailId
                            };
                            await this.emailService.CreateAsync(emailDto);

                            var markAsReadRequest = new ModifyThreadRequest { RemoveLabelIds = new[] { "UNREAD" } };
                            await service.Users.Threads.Modify(markAsReadRequest, SERVICEACCOUNTEMAIL, thread.Id).ExecuteAsync();
                        }
                    }
                }
            }
        }

        public async Task GetEmailByGmailId(string emailId)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
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
            emailListRequest.MaxResults = 5000;
            emailListRequest.Q = "in:anywhere"; //Get emails from all emails
            emailListRequest.IncludeSpamTrash = false;

            //emailListRequest.Q = "has:attachment";

            var emailListRespons = await emailListRequest.ExecuteAsync();

            var emailInfoRequest = service.Users.Messages.Get(SERVICEACCOUNTEMAIL, emailId);

            if (emailListRespons != null)
            {
                var emailInfoResponse = await emailInfoRequest.ExecuteAsync();

                var header = emailInfoResponse?.Payload?.Headers;
                var parts = emailInfoResponse.Payload.Parts;

                var emailDto = new EmailDTO
                {
                    Subject = header?.FirstOrDefault(e => e.Name.Equals("Subject"))?.Value ?? string.Empty,
                    From = header?.FirstOrDefault(e => e.Name.Equals("From"))?.Value,
                };

                double? attachmentSize = 0;
                int countOfAttachments = 0;
                double? totalAttachmentsSizeInMb = 0;

                // Email do not have attachments
                if (emailInfoResponse.Payload.Parts[0].MimeType.Contains("text/plain"))
                {
                    emailDto.Body = parts.First().Body.Data;
                    emailDto.AttachmentsTotalSizeInMB = 0;
                    emailDto.TotalAttachments = 0;
                }
                else // Email have attachments
                {
                    emailDto.Body = parts.First().Parts.First().Body.Data;

                    var attachments = parts.Skip(1).ToList();

                    var attachmentsToAdd = new List<EmailAttachmentDTO>();

                    foreach (var attachment in attachments)
                    {
                        attachmentSize = double.Parse(attachment.Body.Size.ToString());

                        var emailAttachmentDto = new EmailAttachmentDTO
                        {
                            FileSizeInMb = attachmentSize,
                        };

                        totalAttachmentsSizeInMb += attachmentSize;

                        attachmentsToAdd.Add(emailAttachmentDto);
                    }
                    totalAttachmentsSizeInMb /= (1024 * 1024);
                    emailDto.AttachmentsTotalSizeInMB = totalAttachmentsSizeInMb;
                    emailDto.TotalAttachments = attachments.Count;
                }

                await this.emailService.CreateAsync(emailDto);

                var markAsReadRequest = new ModifyMessageRequest { RemoveLabelIds = new[] { "UNREAD" } };
                await service.Users.Messages.Modify(markAsReadRequest, SERVICEACCOUNTEMAIL, emailInfoResponse.Id).ExecuteAsync();
            }
        }

        //emailListRequest.LabelIds = "UNREAD";
        //emailListRequest.IncludeSpamTrash = false;
        //var emailListRespons = await emailListRequest.ExecuteAsync();

        //var body = string.Empty;
        //var receivedDate = DateTime.MaxValue;

        //if (emailListRespons != null && emailListRespons.Messages != null && emailListRespons.Messages.Any())
        //{
        //    foreach (var email in emailListRespons.Messages)
        //    {
        //        var emailInfoRequest = service.Users.Messages.Get(SERVICEACCOUNTEMAIL, email.Id);

        //        var emailInfoResponse = await emailInfoRequest.ExecuteAsync();

        //        var gmailId = emailInfoResponse?.Id;

        //        var header = emailInfoResponse?.Payload?.Headers;

        //        var subject = header?.FirstOrDefault(e => e.Name.Equals("Subject"))?.Value ?? string.Empty;

        //        var from = header?.FirstOrDefault(e => e.Name.Equals("From"))?.Value;


        //        // TODO: Check if take the correct date
        //        var dateEmail = header?.FirstOrDefault(e => e.Name.Equals("Date"))?.Value.Split(new[] { '-' })[0].Trim();

        //        DateTime.TryParse(dateEmail, out receivedDate);

        //        // Email do not have attachments
        //        if (emailInfoResponse.Payload.Parts[0].MimeType.Contains("text/plain"))
        //        {
        //            body = emailInfoResponse.Payload.Parts.First().Body.Data;
        //            var decodedBody = Base64Decode(body);
        //        }
        //        else // Email have attachments
        //        {
        //            body = emailInfoResponse.Payload.Parts.First().Parts.First().Body.Data;
        //            var decodedBody = Base64Decode(body);
        //        }

        //        var emailDto = new EmailDTO
        //        {
        //            Subject = subject,
        //            Body = body,
        //            DateReceived = receivedDate,
        //            From = from,
        //            GmailEmailId = gmailId
        //        };

        //        await this.emailService.CreateAsync(emailDto);

        //        var markAsReadRequest = new ModifyMessageRequest { RemoveLabelIds = new[] { "UNREAD" } };
        //        await service.Users.Messages.Modify(markAsReadRequest, SERVICEACCOUNTEMAIL, emailInfoResponse.Id).ExecuteAsync();
        //}
        //    }

        //public async Task GetEmailByGmailId(string emailId)
        //{
        //    UserCredential credential;

        //    using (var stream =
        //        new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
        //    {
        //        // The file token.json stores the user's access and refresh tokens, and is created
        //        // automatically when the authorization flow completes for the first time.
        //        string credPath = "token.json";
        //        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
        //            GoogleClientSecrets.Load(stream).Secrets,
        //            Scopes,
        //            "user",
        //            CancellationToken.None,
        //            new FileDataStore(credPath, true)).Result;
        //        Console.WriteLine("Credential file saved to: " + credPath);
        //    }

        //    // Create Gmail API service.
        //    var service = new GmailService(new BaseClientService.Initializer()
        //    {
        //        HttpClientInitializer = credential
        //    });

        //    // Define parameters of request.
        //    UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");

        //    var emailListRequest = service.Users.Messages.List(SERVICEACCOUNTEMAIL);

        //    // Get emails only form inbox
        //    //emailListRequest.LabelIds = "UNREAD";
        //    //emailListRequest.IncludeSpamTrash = false;
        //    emailListRequest.MaxResults = 5000;
        //    //emailListRequest.Q = "has:attachment";
        //    emailListRequest.Q = "in:anywhere";

        //    var emailListRespons = await emailListRequest.ExecuteAsync();

        //    var subject = string.Empty;
        //    var body = string.Empty;
        //    //var receivedDate = DateTime.MaxValue;
        //    //var dateEmail = string.Empty;
        //    var senderEmail = string.Empty;
        //    var senderName = string.Empty;
        //    byte attachmentSize = 0;
        //    var countOfAttachments = 0;
        //    double? totalAttachmentsSize = 0;

        //    if (emailListRespons != null && emailListRespons.Messages != null && emailListRespons.Messages.Any())
        //    {
        //        foreach (var email in emailListRespons.Messages)
        //        {
        //            var emailInfoRequest = service.Users.Messages.Get(SERVICEACCOUNTEMAIL, email.Id);

        //            var emailInfoResponse = await emailInfoRequest.ExecuteAsync();

        //            //var gmailId = emailInfoResponse?.Id;

        //            var header = emailInfoResponse?.Payload?.Headers;

        //            subject = header?.FirstOrDefault(e => e.Name.Equals("Subject"))?.Value ?? string.Empty;

        //            var from = header?.FirstOrDefault(e => e.Name.Equals("From"))
        //                ?.Value;
        //            //.Split(new[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries)
        //            //.ToList();

        //            //senderEmail = from
        //            //    .Last()
        //            //    .Trim();

        //            //senderName = from
        //            //    .First()
        //            //    .Trim();


        //            // TODO: the time zone calculations must be consider
        //            //receivedDate = emailInfoResponse.Payload.Headers.FirstOrDefault(e => e.Name.Equals("Date")).Value.ToString().Split(';').ToList().Last().Trim();


        //            // TODO: Check if take the correct date
        //            //dateEmail = header?.FirstOrDefault(e => e.Name.Equals("Date"))?.Value.Split(new[] { '-' })[0].Trim();

        //            //DateTime.TryParse(dateEmail, out receivedDate);

        //            //receivedDate = header?.FirstOrDefault(e => e.Name.Equals("Date"))?.Value.Split(';').ToList().Last().Trim();

        //            // Email do not have attachments
        //            if (emailInfoResponse.Payload.Parts[0].MimeType.Contains("text/plain"))
        //            {
        //                body = emailInfoResponse.Payload.Parts.First().Body.Data;

        //                //TODO: Decode string

        //                var decodedBody = Base64Decode(body);

        //            }
        //            else // Email have attachments
        //            {
        //                body = emailInfoResponse.Payload.Parts.First().Parts.First().Body.Data;

        //                var decodedBody = Base64Decode(body);

        //                //TODO: Decode string

        //                var attachments = emailInfoResponse.Payload.Parts.Skip(1).ToList();

        //                foreach (var attachment in attachments)
        //                {
        //                    totalAttachmentsSize += double.Parse(attachment.Body.Size.ToString());
        //                }
        //                countOfAttachments = attachments.Count;

        //                totalAttachmentsSize /= 1024;
        //            }
        //            totalAttachmentsSize = 0;
        //            //attachments = emailInfoResponse.Payload.Parts..Headers.FirstOrDefault(e => e.Name.Equals("Subject")).Value;

        //            var emailDto = new EmailDTO
        //            {
        //                Subject = subject,
        //                Body = body,
        //                //DateReceived = receivedDate,
        //                From = from,
        //                //SenderName = senderName,
        //                //SenderEmail = senderEmail,
        //                GmailEmailId = gmailId
        //            };

        //            await this.emailService.CreateAsync(emailDto);

        //            var markAsReadRequest = new ModifyMessageRequest { RemoveLabelIds = new[] { "UNREAD" } };
        //            await service.Users.Messages.Modify(markAsReadRequest, SERVICEACCOUNTEMAIL, emailInfoResponse.Id).ExecuteAsync();
        //        }
        //    }
        //}

        //public static string Base64Decode(string base64EncodedData)
        //{
        //    base64EncodedData = base64EncodedData.Replace('-', '+');
        //    base64EncodedData = base64EncodedData.Replace('_', '/');
        //    byte[] encode = Convert.FromBase64String(base64EncodedData);
        //    return Encoding.UTF8.GetString(encode);
        //}

        private string GetNestedParts(IList<MessagePart> part, string curr)
        {
            string str = curr;
            if (part == null)
            {
                return str;
            }
            else
            {
                foreach (var parts in part)
                {
                    if (parts.Parts == null)
                    {
                        if (parts.Body != null && parts.Body.Data != null)
                        {
                            return parts.Body.Data;
                        }
                    }
                    else
                    {
                        return GetNestedParts(parts.Parts, str);
                    }
                }

                return str;
            }
        }
    }
}
