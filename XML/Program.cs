using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;

namespace XML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateXML();
            QueryXML();
        }

        private static void QueryXML()
        {
            var document = XDocument.Load("data.xml");
            var query = from element in document.Element("Cars").Elements("Car")
                        where element.Attribute("Producent").Value == "Ferrari"
                        select element.Attribute("Model").Value;

            foreach (var model in query)
            {
                Console.WriteLine(model);
            }
        }

        private static void CreateXML()
        {
            var records = ReadFile("fuelConsumption.csv");
            var document = new XDocument();
            var cars = new XElement("Cars", from record in records
                                            select new XElement("Car",
                                                                 new XAttribute("Year", record.Year),
                                                                 new XAttribute("Producent", record.Producent),
                                                                 new XAttribute("Model", record.Model),
                                                                 new XAttribute("FuelCons", record.MotorwayFuelConsumption)
                                                                 ));

            document.Add(cars);
            document.Save("data.xml");
        }

        private static List<Car> ReadFile(string path)
        {
            return File.ReadAllLines(path)
                        .Skip(1)
                        .Where(line => line.Length > 1)
                        .IntoCar()
                        .ToList();
        }
    }
}
