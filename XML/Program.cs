using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Data.Entity;

namespace XML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarDB>());
            InsetData();
            GetData();
        }

        private static void InsetData()
        {
            var cars = ReadFile("fuelConsumption.csv");
            var db = new CarDB();

            if (!db.Cars.Any())
            {
                foreach (var car in cars)
                {
                    db.Cars.Add(car);
                }
                db.SaveChanges();
            }
        }

        private static void GetData()
        {
            var db = new CarDB();
            db.Database.Log = Console.WriteLine;
            var cars = from car in db.Cars
                        orderby car.MixedFuelConsumption descending, car.Model ascending
                        select car;

            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Model} : {car.MixedFuelConsumption}");
            }
        }

        private static void FerrariFromXML()
        {
            var document = XDocument.Load("data.xml");
            var query = from element in document.Element("Cars").Elements("Car") 
                        where element.Attribute("Producent").Value == "Ferrari"
                        select new
                        {
                            model = element.Attribute("Model").Value,
                            producent = element.Attribute("Producent").Value,
                        };

            foreach (var car in query)
            {
                Console.WriteLine(car.model + " " + car.producent);
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
