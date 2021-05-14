using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace CsvOperations
{
    class CSVOperation
    {
        public static void CSVReadOperations()
        {
            try
            {
                //Initialization
                string path = @"C:\Users\mahesh\source\repos\CsvOperations\CsvOperations\CSVData.csv";
                string export = @"C:\Users\mahesh\source\repos\CsvOperations\CsvOperations\CSVOutputData.csv";
                string exportjson = @"C:\Users\mahesh\source\repos\CsvOperations\CsvOperations\JsonFromCSVData.json";

                //FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
                var reader = new StreamReader(path);
                var csvread = new CsvReader(reader, CultureInfo.InvariantCulture);

                var personDel = csvread.GetRecords<personDetails>().ToList();
                foreach (personDetails p in personDel)
                {
                    Console.WriteLine(p.name + "\t" + p.email + "\t");
                }
                // using (var writer = new StreamWriter(export))
                // using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                //  {
                //   csvWriter.WriteRecords(personDel);
                // }

                JsonSerializer json = new JsonSerializer();
                using (var writer = new StreamWriter(exportjson))
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    json.Serialize(jsonWriter, personDel);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void ImplimentJsonToCSV()
        {
            try
            {

                //Initialization
                string ImportFilepath = @"C:\Users\mahesh\source\repos\CsvOperations\CsvOperations\JsonFromCSVData.json";
                //string ImportFilepath = @"C:\Users\mahesh\source\repos\CsvOperations\CsvOperations\exportList.json";
                string exportFilepath = @"C:\Users\mahesh\source\repos\CsvOperations\CsvOperations\csvUpdated.csv";

                IList<personDetails> addressData = JsonConvert.DeserializeObject<IList<personDetails>>(File.ReadAllText(ImportFilepath));

                Console.WriteLine("******Now reading from json file and write to csv file******");
                //writing csv file
                using (var writer = new StreamWriter(exportFilepath))
                using (var csvExport = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvExport.WriteRecords(addressData);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
    public class personDetails
    {
        //name,emil,phone,country
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string country { get; set; }

    }
}
