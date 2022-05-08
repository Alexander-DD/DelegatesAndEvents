using System;

namespace DelegatesAndEvents
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1.
            var someList = new List<string>() { "1", "7,1", "33,3", "-22" };
            Console.WriteLine(someList.GetMax(elem => StrToFloat(elem)));

            // 2.
            using var watcher = new FileWatcher(@".\");

            watcher.FileFound += OnFileFound;
            watcher.EnableRaisingEvents = true;

            string? res;
            do
            {
                Console.WriteLine("Enter command:");
                Console.WriteLine("SW - stop watcher");
                Console.WriteLine("SE - stop events");
                Console.WriteLine("E - exit");

                res = Console.ReadLine();

                switch (res)
                {
                    case "SW":
                        watcher.Stop();
                        break;
                    case "SE":
                        Console.WriteLine("Events stopped, but watcher still working");
                        watcher.EnableRaisingEvents = false;
                        break;
                    case "E":
                        Console.WriteLine("Program closing..");
                        return;
                    default:
                        Console.WriteLine("*Unknown command*");
                        break;
                }
            }
            while (res is null || res == String.Empty || res != "E");
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
        private static void OnFileFound(object? sender, EventArgs e)
        {
            Console.WriteLine($"Found: {((FileArgs)e).FileName}");
        }
    }
}