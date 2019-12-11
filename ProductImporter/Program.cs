using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ProductImporter
{
    class Program
    {
        public static string outputFormat { get; set; }
        static void Main(string[] args)
        {
            try
            {
                SetUpConfig();

                Console.WriteLine("Please enter path of file to import. Sytax: <import> <capterra/softwareadvice/..> <file full path>");
                //input ex1: "import softwareadvice feed-products/softwareadvice.json";
                //input ex2: "import capterra feed-products/capterra.yaml";// 
                //string userInput = "import capterra feed-products/capterra.yaml";
                string userInput = Console.ReadLine();
                //string outputFormat = "importing: Name: \"{0}\";  Categories: {1}; Twitter: {2}";
                ProductImporter productImporter = new ProductImporter(outputFormat);
                var feeds = productImporter.ImportProductDetails(userInput);
                foreach (var feed in feeds)
                {
                    Console.WriteLine(feed);// or feeds could be written in a text file. 
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Message: {ex.Message}");
            }
            
            Console.WriteLine("Press key to exit!");
            Console.ReadKey();
        }

        /// <summary>
        /// This method configures settings to read appsettings.json file which has been used to set final output message format.  
        /// </summary>
        private static void SetUpConfig()
        {
            var builder = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            outputFormat = configuration["outputFormat"];

        }
    }
}
