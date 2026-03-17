using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Helpers = Jtech.Common.Helpers;

namespace Jtech.Common.Helpers
{
    public static class Shell
    {
        public static string Execute(string fileName, string args = "", int exitTimeOut = 0)
        {
            try
            {
                //var escapedArgs = cmd.Replace("\"", "\\\"");
                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = fileName,//"ffmpeg",///bin/bash
                        Arguments = string.IsNullOrEmpty(args) ? "" : args,//$"\"{escapedArgs}\"",
                        //$"-c \"{escapedArgs}\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    },
                    EnableRaisingEvents = true
                };


                process.Start();
                using (StreamReader reader = process.StandardOutput)
                {
                    process.WaitForExit();
                    string result = reader.ReadToEnd();


                    return result;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string Bash(string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return result;
        }

     
        public static T? FFProbe<T>(string filePath)
        {
            try
            {
                string jsonProbe = Shell.Execute("ffprobe", $"-v quiet -print_format json -show_format -show_streams \"" + filePath + "\"");
                return Helpers.Json.DeserializeObject<T>(jsonProbe);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
     
    }
}
