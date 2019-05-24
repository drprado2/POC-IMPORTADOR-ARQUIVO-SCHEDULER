using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace file_watcher.api.Schedule
{
    public class ScheduledInspector : IHostedService, IDisposable
    {
        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var intervaloUltimaAlteracao = DateTime.Now - MockDatabase.GetInstance().GetDateLastUpdated().Value;

            if (intervaloUltimaAlteracao.TotalSeconds > 5)
                Console.WriteLine($"FUDEUUU NAO ATUALIZOU FAZER ALGO");
            else
                Console.WriteLine($"OPAA DESSA VEZ ATUALIZOU");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
