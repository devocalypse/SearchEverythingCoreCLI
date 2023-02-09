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
                Console.Write($"SearchEverything\n©2020 Devocalypse\n\nUsage:\nSearchEverythingCoreCLI <keyword>\nSearchEverythingCoreCLI /<regex>/");
                return;
            }

            //check if everything is running
            if (Process.GetProcessesByName("Everything").Length < 1 && Process.GetProcessesByName("Everything64").Length < 1)
            {
                Console.WriteLine(
                    "Everything.exe is not started or has a different executable name.\nStart it yourself and retry.");
                return;
            }

            string searchString;

            //process arguments
            switch (args[0])
            {
                case "--help":
                    Console.Write($"SearchEverything\n©2022 Devocalypse\n\nUsage:\nnSearchEverythingCLI <keyword>");
                    return;

                default:
                    searchString = !args[0].StartsWith("Everything://", StringComparison.CurrentCultureIgnoreCase)
                        ? args[0]
                        : args[0][13..].TrimEnd('/');
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

            //check if dopus is installed
            var dopuspath = Dopus.GetDopusPathFromRegistry();
            if (string.IsNullOrEmpty(dopuspath))
            {
                Console.WriteLine("Dopus not found in registry. Switching to console output:");
                Console.WriteLine("-------------------------");
                foreach (var item in results)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.WriteLine($"Total: {results.Count} results");
                return;
            } else
            {
                //pass results to dopus
                Dopus.GenerateCollection(results);
            }

            
        }
    }
}