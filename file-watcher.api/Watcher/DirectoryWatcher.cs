using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace file_watcher.api.Watcher
{
    public class DirectoryWatcher : IHostedService, IDisposable
    {
        private FileSystemWatcher _fileSystemWatcher;

        // Habilita os eventos aos quais o filewathcer deve reagir
        private void EnableNotifies()
        {
            _fileSystemWatcher.NotifyFilter = NotifyFilters.LastAccess
                                | NotifyFilters.LastWrite
                                | NotifyFilters.FileName
                                | NotifyFilters.DirectoryName;
        }

        // Habilita filtro ao watcher
        private void EnableFileFilter()
        {
            // irá observar apenas alterações em arquivos .txt
            _fileSystemWatcher.Filter = "*.txt";
        }

        private void EnableListeners()
        {
            _fileSystemWatcher.Changed += OnChanged;
            _fileSystemWatcher.Created += OnChanged;
            _fileSystemWatcher.Deleted += OnChanged;
            _fileSystemWatcher.Renamed += OnRenamed;
        }

        public void StartWatch()
        {
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        public void StopWatch()
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            MockDatabase.GetInstance().SetDateLastUpdated();
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");
        }
            

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            MockDatabase.GetInstance().SetDateLastUpdated();
            Console.WriteLine($"File: {e.OldFullPath} renamed to {e.FullPath}");
        }
            

        public void Dispose()
        {
            _fileSystemWatcher.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _fileSystemWatcher = new FileSystemWatcher("C:\\projects\\adiq\\pasta-observar");
            EnableNotifies();
            EnableFileFilter();
            EnableListeners();
            StartWatch();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
