using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Gartner.DataContracts
{
    public class FileService : IDataService
    {
        private readonly ILogger<FileService> _logger;

        public FileService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FileService>();
        }

        public bool ParseData()
        {
            bool isComplete = true;
            try
            {
                Console.WriteLine("Please select a file option from the following list:");
                Console.WriteLine("\tc - Capterra.yaml");
                Console.WriteLine("\ts - Softwareadvice.json");
                Console.Write("Your Option? ");

                switch (Console.ReadLine())
                {
                    case "c":
                        ParseFile("Capterra.yaml", ref isComplete);
                        if (isComplete)
                            Console.Write("Capterra.yaml file parsing not successfull");
                        break;

                    case "s":
                        ParseFile("Softwareadvice.json", ref isComplete);
                        if (!isComplete)
                            Console.Write("Softwareadvice.json file parsing not successfull");
                        break;
                }
                Console.Write("Press any key to close the console app...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                isComplete = false;
                _logger.Log(LogLevel.Error, "Error Ocurred: " + ex.Message);
            }
            return isComplete;
        }

        public void ParseFile(string fileName, ref bool isComplete)
        {
            Console.WriteLine();
            Console.WriteLine($"importing data from {fileName}");

            try
            {
                string filepath = @"feed-products\" + fileName;
                string fileExtension = Path.GetExtension(filepath);

                using (StreamReader file = new StreamReader(filepath))
                {
                    string fileContents = file.ReadToEnd();
                    if (fileExtension == ".yaml")
                    {
                        var deserializer = new DeserializerBuilder()
                          .WithNamingConvention(CamelCaseNamingConvention.Instance)
                          .Build();

                        var arrYaml = deserializer.Deserialize<dynamic>(fileContents);
                        if (arrYaml != null)
                        {
                            foreach (var item in arrYaml)
                            {
                                if (item != null)
                                {
                                    Console.WriteLine($"importing: Name: {item["name"]}; Categories: {item["tags"]}; Twitter: {item["twitter"]}");
                                }
                            }
                        }
                    }
                    else if (fileExtension == ".json")
                    {
                        JObject parsed = JObject.Parse(fileContents);

                        foreach (var pair in parsed)
                        {
                            Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                        }
                    }
                    else
                    {
                        isComplete = false;
                        Console.WriteLine("File type not supported");
                    }
                }
            }
            catch (Exception ex)
            {
                isComplete = false;
                Console.WriteLine("Some exception occured " + ex.Message);
            }
        }
    }
}
