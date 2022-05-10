using System;

namespace DelegatesAndEvents
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // 1.
            var someList = new List<string>() { "1", "7,1", "33,3", "-22" };
            Console.WriteLine(someList.GetMax(elem => StrToFloat(elem)));

            // 2.
            using var watcher = new FileWatcher(@".\");
            watcher.FileFoundEventHandler += OnFileFound;
            watcher.EnableRaisingEvents = true;
            await watcher.Start(AskForStop);
        }

        // 1.
        private static float StrToFloat(string str)
        {
            if (float.TryParse(str, out float val))
            {
                return val;
            }
            else
            {
                return float.MinValue;
            }
        }

        // 2.
        private static bool AskForStop()
        {
            Console.Write("Stop watch files? (Press f to Pay Respects): ");
            ConsoleKeyInfo input = Console.ReadKey();
            Console.WriteLine();
            return input == new ConsoleKeyInfo('f', ConsoleKey.F, false, false, false);
        }

        private static void OnFileFound(object? sender, EventArgs e)
        {
            Console.WriteLine($"Found: {((FileArgs)e).FileName}");
        }
    }
}