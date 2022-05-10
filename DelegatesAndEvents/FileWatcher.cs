namespace DelegatesAndEvents
{
    public class FileWatcher : IDisposable
    {
        private bool InProcess = false;
        public string Path;
        public bool EnableRaisingEvents = false;

        public event EventHandler? FileFoundEventHandler = null;

        public FileWatcher(string path)
        {
            Path = path;
        }

        public async Task Start(Func<bool> stopCallBack)
        {
            InProcess = true; 

            await Task.Run(() =>
            {
                foreach (string fileName in Directory.EnumerateFiles(Path, "*", SearchOption.AllDirectories))
                {
                    if (!InProcess || stopCallBack()) break;

                    if (EnableRaisingEvents)
                    {
                        FileFoundEventHandler?.Invoke(this, new FileArgs() { FileName = fileName });
                    }

                }
            });
        }

        public void Stop()
        {
            InProcess = false;
            Console.WriteLine("Watcher stopped");
        }

        public void Dispose()
        {
            if (!InProcess)
            {
                Stop();
            }
        }
    }
}
