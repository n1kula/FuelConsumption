using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelConsumption
{
    public static class CarExtension
    {
        public static IEnumerable<Car> IntoCar(this IEnumerable<string> data)
        {
            foreach (var line in data)
            {
                var columns = line.SplitCarData();

                yield return new Car
                {
                    Year = int.Parse(columns.ElementAtOrDefault(0)),
                    Producent = columns.ElementAtOrDefault(1),
                    Model = columns.ElementAtOrDefault(2),
                    EngSize = columns.ElementAtOrDefault(3),
                    Cylinders = int.Parse(columns.ElementAtOrDefault(4)),
                    CityFuelConsumption = int.Parse(columns.ElementAtOrDefault(5)),
                    MotorwayFuelConsumption = int.Parse(columns.ElementAtOrDefault(6)),
                    MixedFuelConsumption = int.Parse(columns.ElementAtOrDefault(7))
                };
            }
        }

        private static IEnumerable<string> SplitCarData(this string carData)
        {
            return carData.Split(',');
        }
    }
}
