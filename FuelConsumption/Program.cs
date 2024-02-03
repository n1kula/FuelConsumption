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
            var producers = ReadProducers("producent.csv");

            var query = cars.Where(c => c.Producent == "Audi" && c.Year == 2018)
                            .OrderByDescending(c => c.MotorwayFuelConsumption)
                            .ThenBy(c => c.Producent)
                            .First();

            var queryAnnonymous = cars.Where(c => c.Producent == "Audi" && c.Year == 2018)
                            .OrderByDescending(c => c.MotorwayFuelConsumption)
                            .ThenBy(c => c.Producent)
                            .Select(c => new {c.Producent, c.Model, c.MotorwayFuelConsumption});

            var query2 = from car in cars
                         join producent in producers on car.Producent equals producent.Name
                         orderby car.MotorwayFuelConsumption descending, car.Producent ascending
                         select new
                         {
                             producent.Address,
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

            foreach (var data in query2.Take(10))
            {
                Console.WriteLine(data.Address + " " + data.Model + " " + data.MotorwayFuelConsumption);
            }

            //producent
            var producentQuery = cars.Select(c => c.Producent);

            foreach (var producent in producentQuery)
            {
                foreach (var letter in producent)
                {
                    //Console.WriteLine(letter);
                }
                //Console.WriteLine(producent);
            }

            var producentLettersQuery = cars.SelectMany(c => c.Producent);
            foreach (var letter in producentLettersQuery)
            {
                //Console.WriteLine(letter);
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

        private static List<Producent> ReadProducers(string path)
        {
            var query = File.ReadAllLines(path)
                            .Where(p => p.Length > 1)
                            .Select(p =>
                            {
                                var columns = p.Split(',');
                                return new Producent
                                {
                                    Name = columns[0],
                                    Address = columns[1],
                                    Year = int.Parse(columns[2]),
                                };
                            });
            return query.ToList();
        }
    }


}
