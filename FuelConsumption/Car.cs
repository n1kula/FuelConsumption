﻿using System;

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
    }
}
