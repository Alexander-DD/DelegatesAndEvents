namespace DelegatesAndEvents
{
    public class FileWatcher : IDisposable
    {
        private bool InProcess = false;
        public string Path;
        public bool EnableRaisingEvents = false;

        public event EventHandler? FileFound = null;

        public FileWatcher(string path)
        {
            Path = path;
            Start();
        }

        private async void Start()
        {
            InProcess = true;

            await Task.Run(() =>
            {
                foreach (string fileName in Directory.EnumerateFiles(Path, "*", SearchOption.AllDirectories))
                {
                    if (!InProcess) break;

                    if (EnableRaisingEvents)
                    {
                        FileFound?.Invoke(this, new FileArgs() { FileName = fileName });
                    }

                    // Для того, чтобы можно было успеть остановить просмотр файлов.
                    Thread.Sleep(2000);
                }
                Console.WriteLine("=====End of file watch=====");
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
