using Loanda.EmailClient.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loanda.EmailClient.BackgroundService
{
    public class GmailHostedService : IHostedService, IDisposable
    {
        const uint INTERVAL_IN_MINUTES = 1;

        private readonly IServiceProvider serviceProvider;
        private Timer timer;

        public GmailHostedService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = new Timer(GetUnreadEmailsFromGmail, null, TimeSpan.Zero, TimeSpan.FromMinutes(INTERVAL_IN_MINUTES));

            return Task.CompletedTask;
        }

        private async void GetUnreadEmailsFromGmail(object state)
        {
            using(var scope = this.serviceProvider.CreateScope())
            {
                var unreadEmails = scope.ServiceProvider.GetRequiredService<IGmailApi>();

                await unreadEmails.GetUnreadEmailsFromGmailAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {

            this.timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this.timer?.Dispose();
        }
    }
}
