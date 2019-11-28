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
using System.Threading;
using System.Threading.Tasks;

namespace Loanda.EmailClient
{
    public class GmailApi : IGmailApi
    {
        const string SERVICE_ACCOUNT_EMAIL = "tbiloanda@gmail.com";

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

            var emailThreads = service.Users.Threads.List(SERVICE_ACCOUNT_EMAIL);

            // Get emails only form inbox which are unread
            var labels = new Google.Apis.Util.Repeatable<string>(new List<string> { "INBOX", "UNREAD" });
            emailThreads.LabelIds = labels;
            emailThreads.IncludeSpamTrash = false;
            emailThreads.MaxResults = 5000;

            var emailThreadResult = await emailThreads.ExecuteAsync();

            if (emailThreadResult != null && emailThreadResult.Threads != null && emailThreadResult.Threads.Any())
            {
                foreach (var thread in emailThreadResult.Threads)
                {
                    var data = await service.Users.Threads.Get(SERVICE_ACCOUNT_EMAIL, thread.Id).ExecuteAsync();

                    var gmailId = data?.Id;

                    if (data != null)
                    {
                        foreach (var emailInfoResponse in data.Messages)
                        {
                            if (emailInfoResponse != null)
                            {
                                string body = string.Empty;

                                var dateReceived =
                                    emailInfoResponse.Payload?.Headers?.FirstOrDefault(s => s.Name == "Date")?.Value ??
                                    string.Empty;
                                var from = emailInfoResponse.Payload?.Headers?.FirstOrDefault(s => s.Name == "From")
                                               ?.Value ?? string.Empty;
                                var subject =
                                    emailInfoResponse.Payload?.Headers?.FirstOrDefault(s => s.Name == "Subject")
                                        ?.Value ?? string.Empty;

                                var emailDto = new EmailDTO
                                {
                                    Subject = subject,
                                    Body = body,
                                    DateReceived = dateReceived,
                                    From = from,
                                    GmailEmailId = gmailId,
                                };

                                var parts = emailInfoResponse.Payload?.Parts;

                                double totalAttachmentsSizeInMb = 0;
                                var attachmentsToAdd = new List<EmailAttachmentDTO>();

                                if (parts != null)
                                {
                                    if (parts.First().MimeType.Contains("text/plain")) // Email do not have attachments
                                    {
                                        emailDto.Body = parts.First().Body.Data;
                                        emailDto.AttachmentsTotalSizeInMB = 0;
                                        emailDto.TotalAttachments = 0;
                                    }
                                    else // Email have attachment(s)
                                    {
                                        emailDto.Body = parts.First().Parts.First().Body.Data;

                                        var attachments = parts.Skip(1).ToList();

                                        foreach (var attachment in attachments)
                                        {
                                            var attachmentName = attachment.Filename;

                                            double attachmentSize;
                                            try
                                            {
                                                attachmentSize = double.Parse(attachment.Body.Size.ToString());
                                            }
                                            catch (Exception)
                                            {
                                                attachmentSize = 0.0;
                                            }

                                            var emailAttachmentDto = new EmailAttachmentDTO
                                            {
                                                FileSizeInMb = attachmentSize,
                                                AttachmentName = attachmentName
                                            };

                                            totalAttachmentsSizeInMb += attachmentSize;

                                            attachmentsToAdd.Add(emailAttachmentDto);
                                        }

                                        totalAttachmentsSizeInMb /= (1024 * 1024);
                                        emailDto.AttachmentsTotalSizeInMB = totalAttachmentsSizeInMb;
                                        emailDto.TotalAttachments = attachments.Count;
                                    }
                                }

                                var id = await this.emailService.CreateAsync(emailDto);

                                if (attachmentsToAdd.Count > 0)
                                {
                                    foreach (var attachment in attachmentsToAdd)
                                    {
                                        attachment.ReceivedEmailId = id;
                                        await this.emailAttachmentService.AddAttachmentAsync(attachment);
                                    }
                                }

                                var markAsReadRequest = new ModifyThreadRequest { RemoveLabelIds = new[] { "UNREAD" } };
                                await service.Users.Threads.Modify(markAsReadRequest, SERVICE_ACCOUNT_EMAIL, thread.Id)
                                    .ExecuteAsync();
                            }
                        }
                    }
                }
            }
        }
    }
}
