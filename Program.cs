using System;
using System.ComponentModel;
using System.IO;
using Microsoft.BizTalk.Adapter.Wcf.Publishing.Description;
using Microsoft.BizTalk.Adapter.Wcf.Publishing;
namespace BtsWcfPublish
{
    class Program
    {
        static void Main(string[] args)
        {
            BackgroundWorker worker;

            bool validate = false;
            string filePath = string.Empty;

            if (args.Length == 0)
            {
                throw new ArgumentException(
                    "Application requires one argument: the path and file name of the WCF Service Description file.");
                return;
            }

            if (args.Length == 1)
            {
                if (args[0].Contains("-?") || args[0].Contains("/?") || args[0].ToLower().Contains("help"))
                {
                    Console.WriteLine();
                }
                else
                {
                    filePath = args[0];
                }
            }


            if (args.Length > 1)
            {
                foreach (string arg in args)
                {
                    if (arg.ToLower().Contains("-v") || arg.ToLower().Contains("/v") || arg.ToLower().Contains("validate"))
                    {
                        validate = true;
                    }
                    else
                    {
                        filePath = arg;
                    }
                }                
            }

            if (!File.Exists(filePath))
                throw new FileNotFoundException("WCF file could not be found at '{0}.'", filePath);

            if (validate)
            {
                var validator = new WcfServiceDescriptionValidator();
                if (!validator.Validate(WcfServiceDescription.LoadXml(filePath)))
                {
                    ConsoleColor color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Validation failed:");

                    foreach (ValidationError error in validator.ValidationErrors(WcfServiceDescription.LoadXml(filePath)))
                    {
                        Console.WriteLine(error.Text);
                    }

                    Console.ForegroundColor = color;
                    return;                    
                }
                else Console.WriteLine("validation successful.");


            }

            var desc = WcfServiceDescription.LoadXml(filePath);

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            var p = new Publisher();
            Console.WriteLine();
            
            var results = new Publisher { BackgroundWorker = worker }.Publish(desc);

            Console.WriteLine();
            Console.WriteLine("Message:\r\n" + results.Message + "\r\n");
            Console.WriteLine();
            Console.WriteLine("operation completed with {0} warnings and {1} errors.",results.Warnings,results.Errors);
            
            return;

        }

        static void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.Write(e.ProgressPercentage < 10 ? "Progress: 0{0}%" : "Progress: {0}%", e.ProgressPercentage);

            if (e.ProgressPercentage < 100) Console.CursorLeft = Console.CursorLeft - 13;
        }
    }
}
