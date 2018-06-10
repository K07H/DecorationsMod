using UnityEngine;

namespace DecorationsMod
{
    public class NutrientBlock_PT : PlaceTool
    {
        public override void OnPlace()
        {
            base.OnPlace();

            // Translate
            GameObject model = this.gameObject.FindChild("Nutrient_block");
            model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.06f, model.transform.localPosition.z);

        }
    }

    public class Bleach_PT : PlaceTool
    {
        public override void OnPlace()
        {
            base.OnPlace();

            // Translate
            GameObject bleachModel = this.gameObject.FindChild("model");
            bleachModel.transform.localPosition = new Vector3(bleachModel.transform.localPosition.x, bleachModel.transform.localPosition.y + 0.15f, bleachModel.transform.localPosition.z);
        }
    }

    public class Lubricant_PT : PlaceTool
    {
        public override void OnPlace()
        {
            base.OnPlace();

            // Translate
            GameObject lubricantModel = this.gameObject.FindChild("model");
            lubricantModel.transform.localPosition = new Vector3(lubricantModel.transform.localPosition.x, lubricantModel.transform.localPosition.y + 0.15f, lubricantModel.transform.localPosition.z);
        }
    }

    public class DisinfectedWater_PT : PlaceTool
    {
        public override void OnPlace()
        {
            base.OnPlace();

            // Translate
            GameObject disinfectedwaterModel = this.gameObject.FindChild("model");
            disinfectedwaterModel.transform.localPosition = new Vector3(disinfectedwaterModel.transform.localPosition.x, disinfectedwaterModel.transform.localPosition.y + 0.17f, disinfectedwaterModel.transform.localPosition.z);
        }
    }

    public class FilteredWater_PT : PlaceTool
    {
        public override void OnPlace()
        {
            base.OnPlace();

            // Translate
            GameObject filteredwaterModel = this.gameObject.FindChild("model");
            filteredwaterModel.transform.localPosition = new Vector3(filteredwaterModel.transform.localPosition.x, filteredwaterModel.transform.localPosition.y + 0.155f, filteredwaterModel.transform.localPosition.z);
        }
    }
    
    public class StalkerTooth_PT : PlaceTool
    {
        public override void OnPlace()
        {
            base.OnPlace();

            // Translate
            GameObject stalkertoothModel = this.gameObject.FindChild("shark_tooth");
            stalkertoothModel.transform.localEulerAngles = new Vector3(stalkertoothModel.transform.localEulerAngles.x, stalkertoothModel.transform.localEulerAngles.y, stalkertoothModel.transform.localEulerAngles.z + -45.0f);
            stalkertoothModel.transform.localPosition = new Vector3(stalkertoothModel.transform.localPosition.x, stalkertoothModel.transform.localPosition.y - 0.08f, stalkertoothModel.transform.localPosition.z);
        }
    }

    public class Egg14_PT : PlaceTool
    {
        public override void OnPlace()
        {
            base.OnPlace();

            // Translate
            GameObject egg14Model = this.gameObject.FindChild("Creatures_eggs_10");
            egg14Model.transform.localPosition = new Vector3(egg14Model.transform.localPosition.x, egg14Model.transform.localPosition.y + 0.05f, egg14Model.transform.localPosition.z);
        }
    }

    public class WiringKit_PT : PlaceTool
    {
        public override void OnPlace()
        {
            base.OnPlace();

            // Translate
            GameObject wiringkitModel = this.gameObject.FindChild("model");
            wiringkitModel.transform.localPosition = new Vector3(wiringkitModel.transform.localPosition.x, wiringkitModel.transform.localPosition.y + 0.03f, wiringkitModel.transform.localPosition.z);
        }
    }

    public class AdvancedWiringKit_PT : PlaceTool
    {
        public override void OnPlace()
        {
            base.OnPlace();

            // Translate
            GameObject advancedwiringkitModel = this.gameObject.FindChild("model");
            advancedwiringkitModel.transform.localPosition = new Vector3(advancedwiringkitModel.transform.localPosition.x, advancedwiringkitModel.transform.localPosition.y + 0.03f, advancedwiringkitModel.transform.localPosition.z);
        }
    }

    public class ComputerChip_PT : PlaceTool
    {
        public override void OnPlace()
        {
            base.OnPlace();

            // Translate
            GameObject computerchipModel = this.gameObject.FindChild("model");
            computerchipModel.transform.localPosition = new Vector3(computerchipModel.transform.localPosition.x, computerchipModel.transform.localPosition.y + 0.02f, computerchipModel.transform.localPosition.z);
        }
    }

    public class DetroyCube_PT : PlaceTool
    {
        public override void OnPlace()
        {
            base.OnPlace();

            // Translate
            GameObject cube = this.gameObject.FindChild("Cube");
            if (cube != null)
                GameObject.DestroyImmediate(cube);
        }
    }
}

