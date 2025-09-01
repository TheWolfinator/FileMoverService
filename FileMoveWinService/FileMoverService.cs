using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using Serilog;

namespace FileMoverService
{
    public class FileMoverService : ServiceBase
    {
        private FileSystemWatcher _watcher;
        private readonly string sourceFolder = @"C:\Folder1";
        private readonly string targetFolder = @"C:\Folder2";

        public FileMoverService()
        {
            ServiceName = "FileMoverService";

            // Configure Serilog for .NET 4.8
            Log.Logger = new LoggerConfiguration()
               .WriteTo.EventLog("FileMoverService", manageEventSource: false)
                .WriteTo.File(
                    @"C:\Folder1\FileMoverService-.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7
                )
                .CreateLogger();
        }

        protected override void OnStart(string[] args)
        {
            Log.Information("FileMoverService started at {Time}", DateTime.Now);

            // Ensure directories exist
            Directory.CreateDirectory(sourceFolder);
            Directory.CreateDirectory(targetFolder);

            // Setup FileSystemWatcher
            _watcher = new FileSystemWatcher(sourceFolder)
            {
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };
            _watcher.Created += OnCreated;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                string targetPath = Path.Combine(targetFolder, Path.GetFileName(e.FullPath));

                // Wait until file is ready
                for (int i = 0; i < 10; i++)
                {
                    if (IsFileReady(e.FullPath))
                        break;
                    Thread.Sleep(500);
                }

              
                using (FileStream fs = File.Open(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    // check if the file is ready
                    fs.Close(); 
                }
                Log.Information("Moved file {FileName} to {Target}", e.Name, targetFolder);
                File.Move(e.FullPath, targetPath);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error moving file {FileName}", e.Name);
            }
        }

        private bool IsFileReady(string filename)
        {
            FileStream stream = null;
            try
            {
                stream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
        }

        protected override void OnStop()
        {
            Log.Information("FileMoverService stopped at {Time}", DateTime.Now);
            if (_watcher != null)
            {
                _watcher.Dispose();
                _watcher = null;
            }
        }

#if DEBUG
        public void StartServiceForDebug()
        {
            OnStart(null);
        }

        public void StopServiceForDebug()
        {
            OnStop();
        }
#endif
    }
}
