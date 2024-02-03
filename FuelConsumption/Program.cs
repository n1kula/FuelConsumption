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

            foreach (var car in cars)
            {
                Console.WriteLine(car.Producent + car.Model);
            }
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
