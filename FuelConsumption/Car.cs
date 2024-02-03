using System;

namespace FuelConsumption
{
    public class Car
    { 
        public int Year { get; set; }
        public string Producent { get; set; }
        public string Model { get; set; }
        public string EngSize { get; set; }
        public int Cylinders { get; set; }
        public int CityFuelConsumption { get; set; }
        public int MotorwayFuelConsumption { get; set; }
        public int MixedFuelConsumption { get; set; }

        internal static Car ParseCSV(string line)
        {
            var columns = line.Split(',');

            return new Car
            {
                Year = int.Parse(columns[0]),
                Producent = columns[1],
                Model = columns[2],
                EngSize = columns[3],
                Cylinders = int.Parse(columns[4]),
                CityFuelConsumption = int.Parse(columns[5]),
                MotorwayFuelConsumption = int.Parse(columns[6]),
                MixedFuelConsumption = int.Parse(columns[7])
            };
        }
    }
}
