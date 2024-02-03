using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FuelConsumption
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cars = ReadFile("fuelConsumption.csv");

            var query = cars.OrderByDescending(c => c.MotorwayFuelConsumption).ThenBy(c => c.Producent);

            var query2 = from car in cars
                         orderby car.MotorwayFuelConsumption descending, car.Producent ascending
                         select car;

            foreach (var car in query2.Take(10))
            {
                Console.WriteLine(car.Producent + " " + car.Model + " " + car.MotorwayFuelConsumption);
            }
        }

        private static List<Car> ReadFileQuerySyntax(string path)
        {
            var query = (from line in File.ReadAllLines(path).Skip(1)
                           where line.Length > 1
                           select Car.ParseCSV(line));

            return query.ToList();
        }

        private static List<Car> ReadFile(string path)
        {
            return File.ReadAllLines(path)
                        .Skip(1)
                        .Where(line => line.Length > 1)
                        .Select(Car.ParseCSV)
                        .ToList();
        }
    }
}
