using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecorationsMod
{
    public interface ICustomFlora
    {
        CustomFlora Config { get; set; }
    }

    public class CustomFlora
    {
        // Default values
        public float FoodValue = 3.0f;
        public float WaterValue = 6.0f;
        public bool Decomposes = false;
        public float GrowthDuration = 1200.0f;
        public float Health = 100.0f;
        public bool Knifeable = false;
        public float Charge = 70.0f;

        public CustomFlora(float growthDuration = 1200.0f,
                           float health = 100.0f,
                           bool knifeable = false,
                           float food = 3.0f,
                           float water = 6.0f,
                           bool decomposes = false,
                           float charge = 70.0f)
        {
            GrowthDuration = growthDuration;
            Health = health;
            Knifeable = knifeable;
            FoodValue = food;
            WaterValue = water;
            Decomposes = decomposes;
            Charge = charge;
        }
    }
}
