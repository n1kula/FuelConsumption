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

            var query = cars.Where(c => c.Producent == "Audi" && c.Year == 2018)
                            .OrderByDescending(c => c.MotorwayFuelConsumption)
                            .ThenBy(c => c.Producent)
                            .First();

            var queryAnnonymous = cars.Where(c => c.Producent == "Audi" && c.Year == 2018)
                            .OrderByDescending(c => c.MotorwayFuelConsumption)
                            .ThenBy(c => c.Producent)
                            .Select(c => new {c.Producent, c.Model, c.MotorwayFuelConsumption});

            var query2 = from car in cars
                         orderby car.MotorwayFuelConsumption descending, car.Producent ascending
                         select new
                         {
                             car.Producent,
                             car.Model,
                             car.MotorwayFuelConsumption
                         };

            var any = cars.Any(c => c.Producent == "BMW");
            Console.WriteLine(any);

            var all = cars.All(c => c.Producent == "BMW");
            Console.WriteLine(all);

            var contains = cars.Contains<Car>(cars[5]);
            Console.WriteLine(contains);

            Console.WriteLine(query.Producent + " " + query.Model);

            foreach (var car in queryAnnonymous.Take(10))
            {
                Console.WriteLine(car.Producent + " " + car.Model + " " + car.MotorwayFuelConsumption);
            }
        }

        private static List<Car> ReadFileQuerySyntax(string path)
        {
            var query = File.ReadAllLines(path)
                            .Skip(1)
                            .Where(l => l.Length > 1)
                            .IntoCar();
                
                
                
                //from line in File.ReadAllLines(path).Skip(1)
                //           where line.Length > 1
                //          select Car.ParseCSV(line);

            return query.ToList();
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
