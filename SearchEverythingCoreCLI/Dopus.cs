using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Win32;

namespace SearchEverythingCoreCLI
{
    public static class Dopus
    {
        public static string DopusLocation { get; set; }

        private static bool LoadIntoDopus(string filelist)
        {
            if (File.Exists(filelist))
            {
                var ps = new Process
                {
                    StartInfo =
                    {
                        Arguments =
                            $"/col import /clear /create /nocheck Everything \"{filelist}\"",
                        FileName = DopusLocation,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                ps.Start();
                ps.WaitForExit();
                return ps.ExitCode <= 0;
            }
            return false;
        }

        private static void OpenResults()
        {
            var ps = new Process
            {
                StartInfo =
                {
                    Arguments = "/cmd go path=coll://Everything/",
                    FileName = DopusLocation,
                    CreateNoWindow = true
                }
            };

            ps.Start();
        }

        public static string GetDopusPathFromRegistry()
        {
            DopusLocation =
                (string)
                    Registry.GetValue(
                        @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\DOpus.exe", "Path", "");
            if(!string.IsNullOrEmpty(DopusLocation))
            {
                if (File.Exists(Path.Combine(DopusLocation, "dopusrt.exe")))
                    DopusLocation = Path.Combine(DopusLocation, "dopusrt.exe");
                return DopusLocation;
            } else
            {
                return null;
            }
            
        }

        public static OperationResult GenerateCollection(List<string> list, string name = "Everything")
        {
            if (string.IsNullOrEmpty(DopusLocation))
                return new OperationResult
                {
                    Error = "Directory Opus not found in system registry."
                };

            //magic
            if (list.Count > 0)
            {
                var exportFile = Path.GetTempPath() + Guid.NewGuid();
                try
                {
                    //write file to be loaded
                    if (File.Exists(exportFile))
                        File.Delete(exportFile);
                    var fs = new FileStream(exportFile, FileMode.CreateNew);
                    var sw = new StreamWriter(fs, Encoding.UTF8);
                    sw.Write(string.Join("\n", list.ToArray()));
                    sw.Close();
                    fs.Close();

                    //add it
                    if (!LoadIntoDopus(exportFile))
                        return new OperationResult
                        {
                            Error = "Could not import file list to Directory Opus.",
                        };
                    OpenResults();
                    return new OperationResult { Error = "Export successful" };
                }
                catch (IOException ex)
                {
                    return new OperationResult { Error = ex.Message };
                }
            }
            return new OperationResult
            {
                Error = "No results found. Nothing imported.",
                Header = "0 results"
            };
        }

        public class OperationResult
        {
            public string Search { get; set; }
            public string Error { get; set; }
            public string Header { get; set; }
        }
    }
}