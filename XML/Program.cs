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
            var records = ReadFile("fuelConsumption.csv");
            var document = new XDocument();
            var cars = new XElement("Cars");
            foreach (var item in records)
            {
                var car = new XElement("Car");
                var producent = new XElement("Producent", item.Producent);
                var model = new XElement("Model", item.Model);
                var fuelCons = new XElement("FuelCons", item.MotorwayFuelConsumption);
                car.Add(producent);
                car.Add(model);
                car.Add(fuelCons);
                cars.Add(car);
            }
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
