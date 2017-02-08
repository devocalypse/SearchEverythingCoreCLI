using System;
using System.Diagnostics;

namespace SearchEverythingCoreCLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //process args
            if (args.Length < 1)
            {
                Console.Write($"SearchEverything\n©2017 Devocalypse\n\nUsage:\nSearchEverythingCLI <keyword>\nSearchEverythingCLI /<regex>/");
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
            switch (args[0])
            {
                case "--help":
                    Console.Write($"SearchEverything\n©2016 Devocalypse\n\nUsage:\nnSearchEverythingCLI <keyword>");
                    return;

                default:
                    searchString = !args[0].StartsWith("Everything://", StringComparison.CurrentCultureIgnoreCase)
                        ? args[0]
                        : args[0].Substring(13).TrimEnd('/');
                    break;
            }

            //get results
            if (searchString.StartsWith("/") && searchString.EndsWith("/"))
            {
                searchString = searchString.Trim(Convert.ToChar("/"));
                Everything.Everything_SetRegex(true);
            }
            var results = Everything.Search(searchString);

            Console.WriteLine("Found {0} results.", results.Count);

            //pass results to dopus
            Dopus.GenerateCollection(results);
        }
    }
}