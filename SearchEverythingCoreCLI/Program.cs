using System;
using System.Diagnostics;

namespace SearchEverythingCoreCLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //process args
            if (args.Length < 2)
            {
                Console.Write($"SearchEverything\n©2016 Devocalypse\n\nUsage:\n{args[0]} <keyword>)");
                return;
            }

            //check if everything is running
            if (Process.GetProcessesByName("Everything").Length < 1)
            {
                Console.WriteLine(
                    "Everything.exe is not started or has a different executable name.\nStart it yourself and retry.");
                return;
            }

            //check if dopus is installed
            var dopuspath = Dopus.GetDopusPathFromRegistry();
            if (string.IsNullOrEmpty(dopuspath))
            {
                Console.WriteLine("Dopus not found in registry. Abrorting");
                return;
            }

            string searchString;

            //process arguments
            switch (args[1])
            {
                case "--help":
                    Console.Write($"SearchEverything\n©2016 Devocalypse\n\nUsage:\n{args[0]} <keyword>");
                    return;

                default:
                    searchString = !args[1].StartsWith("Everything://", StringComparison.CurrentCultureIgnoreCase)
                        ? args[1]
                        : args[1].Substring(13).TrimEnd('/');
                    break;
            }

            //get results
            var results = Everything.Search(searchString);

            Console.WriteLine("Found {0} results.", results.Count);

            //pass results to dopus
            Dopus.GenerateCollection(results);
        }
    }
}