using UnityEngine;

namespace DecorationsMod
{
    public class NutrientBlock_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnPlace()
        {
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject model = this.gameObject.FindChild("Nutrient_block");
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.06f, model.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject model = this.gameObject.FindChild("Nutrient_block");
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.06f, model.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }

    public class Bleach_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnPlace()
        {
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject bleachModel = this.gameObject.FindChild("model");
                bleachModel.transform.localPosition = new Vector3(bleachModel.transform.localPosition.x, bleachModel.transform.localPosition.y + 0.15f, bleachModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject bleachModel = this.gameObject.FindChild("model");
                bleachModel.transform.localPosition = new Vector3(bleachModel.transform.localPosition.x, bleachModel.transform.localPosition.y + 0.15f, bleachModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }

    public class Lubricant_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnPlace()
        {
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject lubricantModel = this.gameObject.FindChild("model");
                lubricantModel.transform.localPosition = new Vector3(lubricantModel.transform.localPosition.x, lubricantModel.transform.localPosition.y + 0.15f, lubricantModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject lubricantModel = this.gameObject.FindChild("model");
                lubricantModel.transform.localPosition = new Vector3(lubricantModel.transform.localPosition.x, lubricantModel.transform.localPosition.y + 0.15f, lubricantModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }

    public class DisinfectedWater_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnPlace()
        {
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject disinfectedwaterModel = this.gameObject.FindChild("model");
                disinfectedwaterModel.transform.localPosition = new Vector3(disinfectedwaterModel.transform.localPosition.x, disinfectedwaterModel.transform.localPosition.y + 0.17f, disinfectedwaterModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject disinfectedwaterModel = this.gameObject.FindChild("model");
                disinfectedwaterModel.transform.localPosition = new Vector3(disinfectedwaterModel.transform.localPosition.x, disinfectedwaterModel.transform.localPosition.y + 0.17f, disinfectedwaterModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }

    public class FilteredWater_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnPlace()
        {
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject filteredwaterModel = this.gameObject.FindChild("model");
                filteredwaterModel.transform.localPosition = new Vector3(filteredwaterModel.transform.localPosition.x, filteredwaterModel.transform.localPosition.y + 0.155f, filteredwaterModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject filteredwaterModel = this.gameObject.FindChild("model");
                filteredwaterModel.transform.localPosition = new Vector3(filteredwaterModel.transform.localPosition.x, filteredwaterModel.transform.localPosition.y + 0.155f, filteredwaterModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    
    public class StalkerTooth_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnPlace()
        {
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject stalkertoothModel = this.gameObject.FindChild("shark_tooth");
                stalkertoothModel.transform.localEulerAngles = new Vector3(stalkertoothModel.transform.localEulerAngles.x, stalkertoothModel.transform.localEulerAngles.y, stalkertoothModel.transform.localEulerAngles.z + -45.0f);
                stalkertoothModel.transform.localPosition = new Vector3(stalkertoothModel.transform.localPosition.x, stalkertoothModel.transform.localPosition.y - 0.08f, stalkertoothModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject stalkertoothModel = this.gameObject.FindChild("shark_tooth");
                stalkertoothModel.transform.localEulerAngles = new Vector3(stalkertoothModel.transform.localEulerAngles.x, stalkertoothModel.transform.localEulerAngles.y, stalkertoothModel.transform.localEulerAngles.z + -45.0f);
                stalkertoothModel.transform.localPosition = new Vector3(stalkertoothModel.transform.localPosition.x, stalkertoothModel.transform.localPosition.y - 0.08f, stalkertoothModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }

    public class Egg14_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnPlace()
        {
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject egg14Model = this.gameObject.FindChild("Creatures_eggs_10");
                egg14Model.transform.localPosition = new Vector3(egg14Model.transform.localPosition.x, egg14Model.transform.localPosition.y + 0.05f, egg14Model.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject egg14Model = this.gameObject.FindChild("Creatures_eggs_10");
                egg14Model.transform.localPosition = new Vector3(egg14Model.transform.localPosition.x, egg14Model.transform.localPosition.y + 0.05f, egg14Model.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }

    public class WiringKit_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnPlace()
        {
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject wiringkitModel = this.gameObject.FindChild("model");
                wiringkitModel.transform.localPosition = new Vector3(wiringkitModel.transform.localPosition.x, wiringkitModel.transform.localPosition.y + 0.03f, wiringkitModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject wiringkitModel = this.gameObject.FindChild("model");
                wiringkitModel.transform.localPosition = new Vector3(wiringkitModel.transform.localPosition.x, wiringkitModel.transform.localPosition.y + 0.03f, wiringkitModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }

    public class AdvancedWiringKit_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnPlace()
        {
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject advancedwiringkitModel = this.gameObject.FindChild("model");
                advancedwiringkitModel.transform.localPosition = new Vector3(advancedwiringkitModel.transform.localPosition.x, advancedwiringkitModel.transform.localPosition.y + 0.03f, advancedwiringkitModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject advancedwiringkitModel = this.gameObject.FindChild("model");
                advancedwiringkitModel.transform.localPosition = new Vector3(advancedwiringkitModel.transform.localPosition.x, advancedwiringkitModel.transform.localPosition.y + 0.03f, advancedwiringkitModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }

    public class ComputerChip_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnPlace()
        {
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject computerchipModel = this.gameObject.FindChild("model");
                computerchipModel.transform.localPosition = new Vector3(computerchipModel.transform.localPosition.x, computerchipModel.transform.localPosition.y + 0.02f, computerchipModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject computerchipModel = this.gameObject.FindChild("model");
                computerchipModel.transform.localPosition = new Vector3(computerchipModel.transform.localPosition.x, computerchipModel.transform.localPosition.y + 0.02f, computerchipModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    
    public class OrangeKey_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnPlace()
        {
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject orangeKeyModel = this.gameObject.FindChild("Model");
                orangeKeyModel.transform.localPosition = new Vector3(orangeKeyModel.transform.localPosition.x, orangeKeyModel.transform.localPosition.y + 0.03f, orangeKeyModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject orangeKeyModel = this.gameObject.FindChild("Model");
                orangeKeyModel.transform.localPosition = new Vector3(orangeKeyModel.transform.localPosition.x, orangeKeyModel.transform.localPosition.y + 0.03f, orangeKeyModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }

    public class BlueKey_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnPlace()
        {
            base.OnPlace();
            
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject blueKeyModel = this.gameObject.FindChild("Model");
                blueKeyModel.transform.localPosition = new Vector3(blueKeyModel.transform.localPosition.x, blueKeyModel.transform.localPosition.y + 0.03f, blueKeyModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject blueKeyModel = this.gameObject.FindChild("Model");
                blueKeyModel.transform.localPosition = new Vector3(blueKeyModel.transform.localPosition.x, blueKeyModel.transform.localPosition.y + 0.03f, blueKeyModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }

    public class PurpleKey_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnPlace()
        {
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject purpleKeyModel = this.gameObject.FindChild("Model");
                purpleKeyModel.transform.localPosition = new Vector3(purpleKeyModel.transform.localPosition.x, purpleKeyModel.transform.localPosition.y + 0.03f, purpleKeyModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject purpleKeyModel = this.gameObject.FindChild("Model");
                purpleKeyModel.transform.localPosition = new Vector3(purpleKeyModel.transform.localPosition.x, purpleKeyModel.transform.localPosition.y + 0.03f, purpleKeyModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }

    public class Battery_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnPlace()
        {
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject model = this.gameObject.FindChild("model");
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.069f, model.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject model = this.gameObject.FindChild("model");
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.069f, model.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }

    public class PowerCell_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnPlace()
        {
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject model = this.gameObject.FindChild("engine_power_cell_01");
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.138f, model.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject model = this.gameObject.FindChild("engine_power_cell_01");
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.138f, model.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }

    public class IonBattery_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnPlace()
        {
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject model = this.gameObject.FindChild("model");
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.069f, model.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject model = this.gameObject.FindChild("model");
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.069f, model.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }

    public class IonPowerCell_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnPlace()
        {
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject model = this.gameObject.FindChild("engine_power_cell_ion");
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.138f, model.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject model = this.gameObject.FindChild("engine_power_cell_ion");
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.138f, model.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
}

