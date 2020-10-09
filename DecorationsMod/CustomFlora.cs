using System.Collections.Generic;

namespace DecorationsMod
{
    public interface ICustomFlora
    {
        CustomFlora Config { get; set; }
    }

    public class CustomFlora
    {
        public static readonly List<string> AllPlants = new List<string>(new string[58] {
            "Fern2",
            "Fern4",
            "JungleTree1",
            "JungleTree2",
            "LandPlant1",
            "LandPlant2",
            "LandPlant3",
            "LandPlant4",
            "LandPlant5",
            "LandTree1",
            "TropicalPlant1a",
            "TropicalPlant1b",
            "TropicalPlant2a",
            "TropicalPlant2b",
            "TropicalPlant3a",
            "TropicalPlant3b",
            "TropicalPlant6a",
            "TropicalPlant6b",
            "TropicalPlant7a",
            "TropicalPlant7b",
            "TropicalPlant10a",
            "TropicalPlant10b",
            "CoveTree1",
            "CoveTree2",
            "CrabClawKelp1",
            "CrabClawKelp2",
            "CrabClawKelp3",
            "FloatingStone1",
            "GreenReeds1",
            "GreenReeds6",
            "LostRiverPlant2",
            "LostRiverPlant4",
            "PlantMiddle11",
            "PyroCoral1",
            "PyroCoral2",
            "PyroCoral3",
            "BlueCoralTubes1",
            "BrownCoralTubes1",
            "BrownCoralTubes2",
            "BrownCoralTubes3",
            "SmallDeco3",
            "SmallDeco10",
            "SmallDeco11",
            "SmallDeco13",
            "SmallDeco14",
            "SmallDeco15Red",
            "SmallDeco17Purple",
            "BloodGrassDense",
            "BloodGrassRed",
            "RedGrass1",
            "RedGrass2",
            "RedGrass2Tall",
            "RedGrass3",
            "RedGrass3Tall",
            "MushroomTree1",
            "MushroomTree2",
            "MarbleMelonTiny",
            "MarbleMelonTinyFruit" // Not plant
        });

        // Default values
        public float GrowthDuration = 1200.0f;
        public float Health = 100.0f;
        public float Charge = 70.0f;
        public bool Knifeable = false;

        public bool Eatable = false;
        public float FoodValue = 3.0f;
        public float WaterValue = 6.0f;

        public bool Decomposes = false;
        public float KDecayRate = 0.02f;

        public bool Despawns = false;
        public float DespawnDelay = 300.0f;

        public CustomFlora(float growthDuration = 1200.0f,
                           float health = 100.0f,
                           bool knifeable = true,
                           float food = 3.0f,
                           float water = 6.0f,
                           bool decomposes = false,
                           float charge = 70.0f,
                           float kDecayRate = 0.02f,
                           bool despawns = false,
                           float despawnDelay = 300.0f,
                           bool eatable = false)
        {
            GrowthDuration = growthDuration;
            Health = health;
            Charge = charge;
            Knifeable = knifeable;

            Eatable = eatable;
            FoodValue = food;
            WaterValue = water;

            Decomposes = decomposes;
            KDecayRate = kDecayRate;

            Despawns = despawns;
            DespawnDelay = despawnDelay;
        }
    }
}
