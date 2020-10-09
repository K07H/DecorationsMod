using DecorationsMod.Controllers;
using UnityEngine;

namespace DecorationsMod
{
    #region Abstract generic PlaceTool

    public abstract class GenericPlaceTool_PT : PlaceTool, IProtoEventListener
    {
        #region Properties

        [SerializeField]
        public bool HasBeenPlaced = false;

        [SerializeField]
        public string ModelName = "model";

        [SerializeField]
        public float ModelScale = 1.0f;

        [SerializeField]
        public float DefaultHeightAdd = 0.0f;
        [SerializeField]
        public float HeightMultiplier = 0.0f;

        [SerializeField]
        public float BrighterIllum = -1.0f;
        [SerializeField]
        public float BrighterColor = -1.0f;

        [SerializeField]
        public float RotateX = 0.0f;
        [SerializeField]
        public float RotateY = 0.0f;
        [SerializeField]
        public float RotateZ = 0.0f;

        [SerializeField]
        public float PositionX = 0.0f;
        [SerializeField]
        public float PositionY = 0.0f;
        [SerializeField]
        public float PositionZ = 0.0f;

        #endregion

        #region Constructors

        public GenericPlaceTool_PT(string modelName = "model", float modelScale = 1.0f, float defaultHeightAdd = 0.0f, float heightMultiplier = 0.0f, float brighterIllum = -1.0f, float brighterColor = -1.0f, float rotateX = 0.0f, float rotateY = 0.0f, float rotateZ = 0.0f)
        {
            this.ModelName = modelName;
            this.ModelScale = modelScale;
            this.DefaultHeightAdd = defaultHeightAdd;
            this.HeightMultiplier = heightMultiplier;
            this.BrighterIllum = brighterIllum;
            this.BrighterColor = brighterColor;
            this.RotateX = rotateX;
            this.RotateY = rotateY;
            this.RotateZ = rotateZ;
        }

        public GenericPlaceTool_PT(float posX = 0.0f, float posY = 0.0f, float posZ = 0.0f, string modelName = "model", float rotX = 0.0f, float rotY = 0.0f, float rotZ = 0.0f)
        {
            this.ModelName = modelName;
            this.PositionX = posX;
            this.PositionY = posY;
            this.PositionZ = posZ;
            this.RotateX = rotX;
            this.RotateY = rotY;
            this.RotateZ = rotZ;
        }

        #endregion

        #region Private methods

        private void SetBrighter()
        {
            Renderer[] rends = this.gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in rends)
                if (rend.material != null)
                {
                    Material nMat = new Material(rend.material) { shader = Shader.Find("MarmosetUBER") };
                    if (this.BrighterIllum > 0.0f)
                    {
                        Texture illumTex = nMat.GetTexture("_Illum");
                        if (illumTex == null)
                            illumTex = nMat.GetTexture("_MainTex");
                        if (illumTex != null)
                        {
                            nMat.EnableKeyword("MARMO_EMISSION");
                            nMat.SetTexture("_Illum", illumTex);
                            nMat.SetFloat("_EmissionLM", this.BrighterIllum);
                        }
                    }
                    if (this.BrighterColor > 0.0f)
                        nMat.color = new Color(Mathf.Clamp01(nMat.color.r + this.BrighterColor), Mathf.Clamp01(nMat.color.g + this.BrighterColor), Mathf.Clamp01(nMat.color.b + this.BrighterColor), nMat.color.a);
                    rend.material = nMat;
                }
        }

        private void ScaleAndTranslate()
        {
            // Scale + Translate
            GameObject model = this.gameObject.FindChild(this.ModelName);
            if (model != null)
            {
#if DEBUG_PLACE_TOOL
                Logger.Log("DEBUG: Material_PT: Current angle for [" + this.gameObject.name + "]=[" + model.transform.localEulerAngles.x.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" + model.transform.localEulerAngles.y.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" + model.transform.localEulerAngles.z.ToString(System.Globalization.CultureInfo.InvariantCulture) + "]");
#endif
                if (this.RotateX != 0.0f || this.RotateY != 0.0f || this.RotateZ != 0.0f)
                    model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x + this.RotateX, model.transform.localEulerAngles.y + this.RotateY, model.transform.localEulerAngles.z + this.RotateZ);
                model.transform.localScale *= this.ModelScale;
                if (this.DefaultHeightAdd != 0.0f || this.HeightMultiplier != 0.0f)
                {
                    float halfHeight = this.DefaultHeightAdd;
                    Renderer rend = model.GetComponentInChildren<Renderer>() ?? model.GetComponent<Renderer>();
                    if (rend != null)
                        halfHeight = rend.bounds.size.y * this.HeightMultiplier;
                    else
                    {
                        Collider col = model.GetComponentInChildren<Collider>() ?? this.gameObject.GetComponentInChildren<Collider>();
                        if (col != null)
                            halfHeight = col.bounds.size.y * this.HeightMultiplier;
                    }
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + halfHeight, model.transform.localPosition.z);
                }
                if (this.PositionX != 0.0f || this.PositionY != 0.0f || this.PositionZ != 0.0f)
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x + this.PositionX, model.transform.localPosition.y + this.PositionY, model.transform.localPosition.z + this.PositionZ);
            }
#if DEBUG_PLACE_TOOL
            else
            {
                Logger.Log("DEBUG: Material_PT: OnProtoDeserialize: Model \"" + this.ModelName + "\" not found!");
                Logger.PrintTransform(this.gameObject.transform);
            }
#endif
        }

        #endregion

        #region Override methods

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                if (this.BrighterIllum > 0.0f || this.BrighterColor > 0.0f)
                    SetBrighter();
                ScaleAndTranslate();
                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                if (this.BrighterIllum > 0.0f || this.BrighterColor > 0.0f)
                    SetBrighter();
                ScaleAndTranslate();
                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }

        #endregion
    }

    #endregion

    #region PlaceTools

    public class GenericPlaceTool : PlaceTool
    {
        #region Override methods

        public override void OnDraw(Player p)
        {
            var cpt = this.gameObject.GetComponent<CustomPlaceToolController>();
            if (cpt != null)
                cpt.Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            var cpt = this.gameObject.GetComponent<CustomPlaceToolController>();
            if (cpt != null)
                cpt.Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            var cpt = this.gameObject.GetComponent<CustomPlaceToolController>();
            if (cpt != null)
                cpt.Hide();
            base.OnPlace();
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        #endregion
    }

    public class NutrientBlock_PT : GenericPlaceTool_PT
    {
        public NutrientBlock_PT() : base(0.0f, 0.06f, 0.0f, "Nutrient_block") { }
    }

    public class Bleach_PT : GenericPlaceTool_PT
    {
        public Bleach_PT() : base(0.0f, 0.15f, 0.0f, "model") { }
    }

    public class Lubricant_PT : GenericPlaceTool_PT
    {
        public Lubricant_PT() : base(0.0f, 0.15f, 0.0f, "model") { }
    }

    public class DisinfectedWater_PT : GenericPlaceTool_PT
    {
        public DisinfectedWater_PT() : base(0.0f, 0.17f, 0.0f, "model") { }
    }

    public class FilteredWater_PT : GenericPlaceTool_PT
    {
        public FilteredWater_PT() : base(0.0f, 0.155f, 0.0f, "model") { }
    }

    public class StalkerTooth_PT : GenericPlaceTool_PT
    {
        public StalkerTooth_PT() : base(0.0f, -0.08f, 0.0f, "shark_tooth", 0.0f, 0.0f, -45.0f) { }
    }

    public class Egg14_PT : GenericPlaceTool_PT
    {
        public Egg14_PT() : base(0.0f, 0.05f, 0.0f, "Creatures_eggs_10") { }
    }

    public class WiringKit_PT : GenericPlaceTool_PT
    {
        public WiringKit_PT() : base(0.0f, 0.03f, 0.0f, "model") { }
    }

    public class AdvancedWiringKit_PT : GenericPlaceTool_PT
    {
        public AdvancedWiringKit_PT() : base(0.0f, 0.03f, 0.0f, "model") { }
    }

    public class ComputerChip_PT : GenericPlaceTool_PT
    {
        public ComputerChip_PT() : base(0.0f, 0.02f, 0.0f, "model") { }
    }

    public class OrangeKey_PT : GenericPlaceTool_PT
    {
        public OrangeKey_PT() : base(0.0f, 0.03f, 0.0f, "Model") { }
    }

    public class BlueKey_PT : GenericPlaceTool_PT
    {
        public BlueKey_PT() : base(0.0f, 0.03f, 0.0f, "Model") { }
    }

    public class PurpleKey_PT : GenericPlaceTool_PT
    {
        public PurpleKey_PT() : base(0.0f, 0.03f, 0.0f, "Model") { }
    }

    public class RedKey_PT : GenericPlaceTool_PT
    {
        public RedKey_PT() : base(0.0f, 0.0202f, 0.0f, "Model") { }
    }

    public class WhiteKey_PT : GenericPlaceTool_PT
    {
        public WhiteKey_PT() : base(0.0f, 0.0202f, 0.0f, "Model") { }
    }

    public class Battery_PT : GenericPlaceTool_PT
    {
        public Battery_PT() : base(0.0f, 0.069f, 0.0f, "model") { }
    }

    public class PowerCell_PT : GenericPlaceTool_PT
    {
        public PowerCell_PT() : base(0.0f, 0.138f, 0.0f, "engine_power_cell_01") { }
    }

    public class IonBattery_PT : GenericPlaceTool_PT
    {
        public IonBattery_PT() : base(0.0f, 0.069f, 0.0f, "model") { }
    }

    public class IonPowerCell_PT : GenericPlaceTool_PT
    {
        public IonPowerCell_PT() : base(0.0f, 0.138f, 0.0f, "engine_power_cell_ion") { }
    }

    public class Silicone_PT : GenericPlaceTool_PT
    {
        public Silicone_PT() : base("model", 0.6f, 0.1f, 0.5f) { }
    }

    public class Titanium_PT : GenericPlaceTool_PT
    {
        public Titanium_PT() : base("model", 0.6f, 0.1f, 0.35f) { }
    }

    public class Copper_PT : GenericPlaceTool_PT
    {
        public Copper_PT() : base("copper_small", 0.75f, 0.1f, 0.32f) { }
    }

    public class Sulphur_PT : GenericPlaceTool_PT
    {
        public Sulphur_PT() : base("Sulphur_small", 0.36f, 0.05f, 0.15f) { }
    }

    public class Diamond_PT : GenericPlaceTool_PT
    {
        public Diamond_PT() : base("Diamond_small", 0.5f) { }
    }

    public class Lithium_PT : GenericPlaceTool_PT
    {
        public Lithium_PT() : base("Lithium_small", 0.5f) { }
    }

    public class Magnetite_PT : GenericPlaceTool_PT
    {
        public Magnetite_PT() : base("Magnetite_small", 0.5f) { }
    }

    public class Nickel_PT : GenericPlaceTool_PT
    {
        public Nickel_PT() : base("Niсkel_ore_small", 0.38f) { }
    }

    public class Quartz_PT : GenericPlaceTool_PT
    {
        public Quartz_PT() : base("Quartz_small", 0.5f) { }
    }

    public class Lead_PT : GenericPlaceTool_PT
    {
        public Lead_PT() : base("Lead_small", 0.55f) { }
    }

    public class FiberMesh_PT : GenericPlaceTool_PT
    {
        public FiberMesh_PT() : base("model", 1.0f, 0.1f, 0.5f) { }
    }

    public class AramidFibers_PT : GenericPlaceTool_PT
    {
        public AramidFibers_PT() : base("Aramid_fibers", 1.0f, 0.1f, 0.5f) { }
    }

    public class SeaTreaderPoop_PT : GenericPlaceTool_PT
    {
        public SeaTreaderPoop_PT() : base("sea_treader_poop_01", 1.0f, 0.1f, 0.4f, 0.15f) { }
    }

    public class Silver_PT : GenericPlaceTool_PT
    {
        public Silver_PT() : base("Silver_ore_small", 0.5f, 0.1f, 0.16f) { }
    }

    public class Gold_PT : GenericPlaceTool_PT
    {
        public Gold_PT() : base("gold_small", 0.5f, 0.1f, 0.24f) { }
    }

    public class CrashPowder_PT : GenericPlaceTool_PT
    {
        public CrashPowder_PT() : base("Model", 0.7f) { }
    }

    public class BloodOil_PT : GenericPlaceTool_PT
    {
        public BloodOil_PT() : base("model", 0.38f, 0.1f, 0.37f, -1.0f, -1.0f, 30.0f, 300.0f, 30.0f) { }
    }

    public class UraniniteCrystal_PT : GenericPlaceTool_PT
    {
        public UraniniteCrystal_PT() : base("Uraninite_crystal_small", 0.5f, 0.1f, 0.06f) { }
    }

    public class Kyanite_PT : GenericPlaceTool_PT
    {
        public Kyanite_PT() : base("kyanite_small_03", 0.5f, 0.1f, 0.1f) { }
    }

    public class JeweledDiskPiece_PT : GenericPlaceTool_PT
    {
        public JeweledDiskPiece_PT() : base("Coral_reef_jeweled_disk_purple_01_01", 0.5f, 0.1f, 0.15f) { }
    }

    public class AluminumOxide_PT : GenericPlaceTool_PT
    {
        public AluminumOxide_PT() : base("Aluminium_Oxide_small", 0.5f, 0.1f, 0.1f) { }
    }

    public class PlasteelIngot_PT : GenericPlaceTool_PT
    {
        public PlasteelIngot_PT() : base("model", 1.0f, 0.0f, 0.0f, 2.0f, 0.4f) { }
    }

    public class TitaniumIngot_PT : GenericPlaceTool_PT
    {
        public TitaniumIngot_PT() : base("model", 0.75f, 0.1f, 0.5f, 10.0f, 0.4f) { }
    }

    public class CopperWire_PT : GenericPlaceTool_PT
    {
        public CopperWire_PT() : base("model", 0.5f, 0.1f, 0.5f, 0.15f, 0.4f) { }
    }

    public class Glass_PT : GenericPlaceTool_PT
    {
        public Glass_PT() : base("model", 1.0f, 0.1f, 0.3f, 5.0f) { }
    }

    public class EnameledGlass_PT : GenericPlaceTool_PT
    {
        public EnameledGlass_PT() : base("model", 1.0f, 0.1f, 0.3f, 5.0f) { }
    }

    public class CoralChunk_PT : GenericPlaceTool_PT
    {
        public CoralChunk_PT() : base("Coral_reef_shell_01", 0.4f, -0.1f, -0.75f, -1.0f, -1.0f, -90.0f, 0.0f, 0.0f) { }
    }

    #endregion

    #region Special PlaceTools

    public class EggSeaEmperor_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Scale
                GameObject model = this.gameObject.FindChild("Creatures_eggs_11");
                model.transform.localScale *= 4f;
                foreach (SphereCollider c in this.gameObject.GetAllComponentsInChildren<SphereCollider>())
                    c.radius *= 3.5f;

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Scale
                GameObject model = this.gameObject.FindChild("Creatures_eggs_11");
                model.transform.localScale *= 4f;

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }

    public class AlienArtefact1_PT : PlaceTool
    {
        public override void OnDraw(Player p)
        {
            var cpt = this.gameObject.GetComponent<CustomPlaceToolController>();
            if (cpt != null)
                cpt.Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            var cpt = this.gameObject.GetComponent<CustomPlaceToolController>();
            if (cpt != null)
                cpt.Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            var cpt = this.gameObject.GetComponent<CustomPlaceToolController>();
            if (cpt != null)
                cpt.Hide();
            base.OnPlace();

            Transform aim = Player.main.camRoot.GetAimingTransform();
            if (aim.eulerAngles.x > 65f)
                this.gameObject.transform.Translate(0f, 0.2f, 0f, aim);
            else if (aim.eulerAngles.x < 25f)
                this.gameObject.transform.Translate(0f, 0f, 0.2f, aim);
            else
                this.gameObject.transform.Translate(0f, 0.14f, 0.14f, aim);
        }
    }

    public class Salt_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        private void ScaleAndTranslate()
        {
            bool hide = false;
            foreach (Transform tr in this.gameObject.transform)
                if (tr.name.StartsWith("Mesh"))
                {
                    if (hide)
                        tr.GetComponent<Renderer>().enabled = false;
                    else
                    {
                        tr.localScale *= 0.5f;
                        float thirdHeight = 0.05f;
                        Renderer rend = tr.GetComponent<Renderer>();
                        if (rend != null)
                            thirdHeight = rend.bounds.size.y * 0.47f;
                        tr.localPosition = new Vector3(tr.localPosition.x, tr.localPosition.y + thirdHeight, tr.localPosition.z);
                        hide = true;
                    }
                }
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                ScaleAndTranslate();
                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                ScaleAndTranslate();
                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }

    #endregion

    #region To Remove

    /*
     * public class NutrientBlock_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
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
     */
    /*
     public class Bleach_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject bleachModel = this.gameObject.FindChild("model");
                bleachModel.transform.localPosition = new Vector3(bleachModel.transform.localPosition.x, bleachModel.transform.localPosition.y + 0.15f, bleachModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
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
     */
    /*
     public class Lubricant_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject lubricantModel = this.gameObject.FindChild("model");
                lubricantModel.transform.localPosition = new Vector3(lubricantModel.transform.localPosition.x, lubricantModel.transform.localPosition.y + 0.15f, lubricantModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
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
     */
    /*
     public class DisinfectedWater_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject disinfectedwaterModel = this.gameObject.FindChild("model");
                disinfectedwaterModel.transform.localPosition = new Vector3(disinfectedwaterModel.transform.localPosition.x, disinfectedwaterModel.transform.localPosition.y + 0.17f, disinfectedwaterModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
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
     */
    /*
     public class FilteredWater_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject filteredwaterModel = this.gameObject.FindChild("model");
                filteredwaterModel.transform.localPosition = new Vector3(filteredwaterModel.transform.localPosition.x, filteredwaterModel.transform.localPosition.y + 0.155f, filteredwaterModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
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
     */
    /*
     public class StalkerTooth_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
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
     */
    /*
     public class Egg14_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
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
     */
    /*
     public class WiringKit_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject wiringkitModel = this.gameObject.FindChild("model");
                wiringkitModel.transform.localPosition = new Vector3(wiringkitModel.transform.localPosition.x, wiringkitModel.transform.localPosition.y + 0.03f, wiringkitModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
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
     */
    /*
     public class AdvancedWiringKit_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject advancedwiringkitModel = this.gameObject.FindChild("model");
                advancedwiringkitModel.transform.localPosition = new Vector3(advancedwiringkitModel.transform.localPosition.x, advancedwiringkitModel.transform.localPosition.y + 0.03f, advancedwiringkitModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
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
     */
    /*
     public class ComputerChip_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject computerchipModel = this.gameObject.FindChild("model");
                computerchipModel.transform.localPosition = new Vector3(computerchipModel.transform.localPosition.x, computerchipModel.transform.localPosition.y + 0.02f, computerchipModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
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
     */
    /*
     public class Battery_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject model = this.gameObject.FindChild("model");
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.069f, model.transform.localPosition.z);

                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
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
     */
    /*
    public class PowerCell_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
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
     */
    /*
    public class IonBattery_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject model = this.gameObject.FindChild("model");
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.069f, model.transform.localPosition.z);

                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
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
    */
    /*
    public class IonPowerCell_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
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
    */
    /*
    public class OrangeKey_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
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

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
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

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
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

    public class RedKey_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject redKeyModel = this.gameObject.FindChild("Model");
                redKeyModel.transform.localPosition = new Vector3(redKeyModel.transform.localPosition.x, redKeyModel.transform.localPosition.y + 0.0202f, redKeyModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject redKeyModel = this.gameObject.FindChild("Model");
                redKeyModel.transform.localPosition = new Vector3(redKeyModel.transform.localPosition.x, redKeyModel.transform.localPosition.y + 0.0202f, redKeyModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }

    public class WhiteKey_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject whiteKeyModel = this.gameObject.FindChild("Model");
                whiteKeyModel.transform.localPosition = new Vector3(whiteKeyModel.transform.localPosition.x, whiteKeyModel.transform.localPosition.y + 0.0202f, whiteKeyModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject whiteKeyModel = this.gameObject.FindChild("Model");
                whiteKeyModel.transform.localPosition = new Vector3(whiteKeyModel.transform.localPosition.x, whiteKeyModel.transform.localPosition.y + 0.0202f, whiteKeyModel.transform.localPosition.z);

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class BloodOil_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        private void Adjust()
        {
            // Scale + Translate
            GameObject model = this.gameObject.FindChild("model");
            if (model != null)
            {
                model.transform.localEulerAngles = Vector3.zero;
                model.transform.localScale *= 0.45f;
                float halfHeight = 0.1f;
                Renderer rend = model.GetComponentInChildren<Renderer>();
                if (rend != null)
                    halfHeight = rend.bounds.size.y * 0.49f;
                else
                {
                    Collider col = model.GetComponentInChildren<Collider>();
                    if (col == null)
                        col = this.gameObject.GetComponentInChildren<Collider>();
                    if (col != null)
                        halfHeight = col.bounds.size.y * 0.49f;
                }
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + halfHeight, model.transform.localPosition.z);
            }
#if DEBUG
            else
            {
                Logger.Log("DEBUG: Material_PT: OnProtoDeserialize: BloodOil model not found!");
                Logger.PrintTransform(this.gameObject.transform);
            }
#endif
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                Adjust();
                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                Adjust();
                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class EggSeaDragon_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Scale
                GameObject model = this.gameObject.FindChild("engine_power_cell_ion");
                model.transform.localScale *= 2f;

                HasBeenPlaced = true;
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Scale
                GameObject model = this.gameObject.FindChild("engine_power_cell_ion");
                model.transform.localScale *= 2f;

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class CoralChunk_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        private void RotateScaleAndTranslate()
        {
            GameObject model = this.gameObject.FindChild("Coral_reef_shell_01");
            if (model != null)
            {
                model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x - 90.0f, model.transform.localEulerAngles.y, model.transform.localEulerAngles.z);
                model.transform.localScale *= 0.4f;
                float halfHeight = 0.1f;
                Renderer rend = model.GetComponentInChildren<Renderer>();
                if (rend != null)
                    halfHeight = rend.bounds.size.y * 0.75f;
                else
                {
                    Collider col = model.GetComponentInChildren<Collider>();
                    if (col != null)
                        halfHeight = col.bounds.size.y * 0.75f;
                }
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y - halfHeight, model.transform.localPosition.z);
            }
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                RotateScaleAndTranslate();
                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                RotateScaleAndTranslate();
                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class PlasteelIngot_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        private void SetBrighter()
        {
            Renderer[] rends = this.gameObject.GetComponentsInChildren<Renderer>();
            if (rends != null)
                foreach (Renderer rend in rends)
                    if (rend.material != null)
                    {
                        Material nMat = new Material(rend.material);
                        nMat.shader = Shader.Find("MarmosetUBER");
                        Texture mainTex = nMat.GetTexture("_MainTex");
                        if (mainTex != null)
                        {
                            nMat.EnableKeyword("MARMO_EMISSION");
                            nMat.SetTexture("_Illum", mainTex);
                            nMat.SetFloat("_EmissionLM", 0.2f);
                        }
                        nMat.color = new Color(Mathf.Clamp01(nMat.color.r + 0.4f), Mathf.Clamp01(nMat.color.g + 0.4f), Mathf.Clamp01(nMat.color.b + 0.4f), nMat.color.a);
                        rend.material = nMat;
                    }
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                SetBrighter();
                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                SetBrighter();
                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class TitaniumIngot_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        private void SetBrighter()
        {
            Renderer[] rends = this.gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in rends)
                if (rend.material != null)
                {
                    Material nMat = new Material(rend.material);
                    nMat.shader = Shader.Find("MarmosetUBER");
                    Texture mainTex = nMat.GetTexture("_MainTex");
                    if (mainTex != null)
                    {
                        nMat.EnableKeyword("MARMO_EMISSION");
                        nMat.SetTexture("_Illum", mainTex);
                        nMat.SetFloat("_EmissionLM", 10.0f);
                    }
                    nMat.color = new Color(Mathf.Clamp01(nMat.color.r + 0.4f), Mathf.Clamp01(nMat.color.g + 0.4f), Mathf.Clamp01(nMat.color.b + 0.4f), nMat.color.a);
                    rend.material = nMat;
                }
        }

        private void ScaleAndTranslate()
        {
            GameObject model = this.gameObject.FindChild("model");
            if (model != null && model.transform != null)
            {
                model.transform.localScale *= 0.75f;
                float thirdHeight = 0.1f;
                Renderer rend = model.GetComponentInChildren<Renderer>();
                if (rend != null)
                    thirdHeight = rend.bounds.size.y * 0.5f;
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + thirdHeight, model.transform.localPosition.z);
            }
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                SetBrighter();
                ScaleAndTranslate();
                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                SetBrighter();
                ScaleAndTranslate();
                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class CopperWire_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        private void SetBrighter()
        {
            Renderer[] rends = this.gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in rends)
                if (rend.material != null)
                {
                    Material nMat = new Material(rend.material);
                    nMat.shader = Shader.Find("MarmosetUBER");
                    Texture mainTex = nMat.GetTexture("_MainTex");
                    if (mainTex != null)
                    {
                        nMat.EnableKeyword("MARMO_EMISSION");
                        nMat.SetTexture("_Illum", mainTex);
                        nMat.SetFloat("_EmissionLM", 0.15f);
                    }
                    nMat.color = new Color(Mathf.Clamp01(nMat.color.r + 0.4f), Mathf.Clamp01(nMat.color.g + 0.4f), Mathf.Clamp01(nMat.color.b + 0.4f), nMat.color.a);
                    rend.material = nMat;
                }
        }

        private void ScaleAndTranslate()
        {
            GameObject model = this.gameObject.FindChild("model");
            if (model != null && model.transform != null)
            {
                model.transform.localScale *= 0.5f;
                float thirdHeight = 0.1f;
                Renderer rend = model.transform.GetComponent<Renderer>();
                if (rend != null)
                    thirdHeight = rend.bounds.size.y * 0.5f;
                else
                {
                    Collider col = this.gameObject.GetComponent<Collider>();
                    if (col != null)
                        thirdHeight = col.bounds.size.y * 0.5f;
                }
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + thirdHeight, model.transform.localPosition.z);
            }
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                SetBrighter();
                ScaleAndTranslate();
                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                SetBrighter();
                ScaleAndTranslate();
                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class Glass_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        private void SetBrighter()
        {
            Renderer[] rends = this.gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in rends)
                if (rend.material != null)
                {
                    Material nMat = new Material(rend.material);
                    Texture illumTex = nMat.GetTexture("_Illum");
                    if (illumTex == null)
                        illumTex = nMat.GetTexture("_MainTex");
                    else
                        illumTex = null;
                    nMat.EnableKeyword("MARMO_EMISSION");
                    if (illumTex != null)
                        nMat.SetTexture("_Illum", illumTex);
                    nMat.SetFloat("_EmissionLM", 5.0f);
                    rend.material = nMat;
                }
        }

        private void Translate()
        {
            GameObject model = this.gameObject.FindChild("model");
            if (model != null && model.transform != null)
            {
                float thirdHeight = 0.1f;
                Renderer rend = model.transform.GetComponent<Renderer>();
                if (rend != null)
                    thirdHeight = rend.bounds.size.y * 0.3f;
                else
                {
                    Collider col = this.gameObject.GetComponent<Collider>();
                    if (col != null)
                        thirdHeight = col.bounds.size.y * 0.3f;
                }
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + thirdHeight, model.transform.localPosition.z);
            }
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                SetBrighter();
                Translate();
                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                SetBrighter();
                Translate();
                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class Silicone_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Scale + Translate
                GameObject model = this.gameObject.FindChild("model");
                if (model != null)
                {
                    model.transform.localScale *= 0.6f;
                    float halfHeight = 0.1f;
                    Renderer rend = model.GetComponentInChildren<Renderer>();
                    if (rend != null)
                        halfHeight = rend.bounds.size.y * 0.5f;
                    else
                    {
                        Collider col = model.GetComponentInChildren<Collider>();
                        if (col != null)
                            halfHeight = col.bounds.size.y * 0.5f;
                    }
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + halfHeight, model.transform.localPosition.z);
                }
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: Silicone_PT: OnPlace: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif

                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Scale + Translate
                GameObject model = this.gameObject.FindChild("model");
                if (model != null)
                {
                    model.transform.localScale *= 0.6f;
                    float halfHeight = 0.1f;
                    Renderer rend = model.GetComponentInChildren<Renderer>();
                    if (rend != null)
                        halfHeight = rend.bounds.size.y * 0.5f;
                    else
                    {
                        Collider col = model.GetComponentInChildren<Collider>();
                        if (col != null)
                            halfHeight = col.bounds.size.y * 0.5f;
                    }
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + halfHeight, model.transform.localPosition.z);
                }
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: Silicone_PT: OnProtoDeserialize: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class SmallerSizeThirdUp_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Scale + Translate
                GameObject model = this.gameObject.FindChild("model");

                if (model != null)
                {
                    model.transform.localScale *= 0.75f;
                    float halfHeight = 0.1f;
                    Renderer rend = model.GetComponentInChildren<Renderer>();
                    if (rend != null)
                        halfHeight = rend.bounds.size.y * 0.35f;
                    else
                    {
                        Collider col = model.GetComponentInChildren<Collider>();
                        if (col != null)
                            halfHeight = col.bounds.size.y * 0.35f;
                    }
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + halfHeight, model.transform.localPosition.z);
                }
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: SmallerSizeThirdUp_PT: OnPlace: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif
                
                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Scale + Translate
                GameObject model = this.gameObject.FindChild("model");

                if (model != null)
                {
                    model.transform.localScale *= 0.75f;
                    float halfHeight = 0.1f;
                    Renderer rend = model.GetComponentInChildren<Renderer>();
                    if (rend != null)
                        halfHeight = rend.bounds.size.y * 0.35f;
                    else
                    {
                        Collider col = model.GetComponentInChildren<Collider>();
                        if (col != null)
                            halfHeight = col.bounds.size.y * 0.35f;
                    }
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + halfHeight, model.transform.localPosition.z);
                }
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: SmallerSizeThirdUp_PT: OnProtoDeserialize: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif
                
                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class SmallerSizeHalfUp_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Scale + Translate
                GameObject model = this.gameObject.FindChild("model");
                if (model == null)
                    model = this.gameObject.FindChild("copper_small");

                if (model != null)
                {
                    model.transform.localScale *= 0.75f;
                    float halfHeight = 0.1f;
                    Renderer rend = model.GetComponentInChildren<Renderer>();
                    if (rend != null)
                        halfHeight = rend.bounds.size.y * 0.5f;
                    else
                    {
                        Collider col = model.GetComponentInChildren<Collider>();
                        if (col != null)
                            halfHeight = col.bounds.size.y * 0.5f;
                    }
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + halfHeight, model.transform.localPosition.z);
                }
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: SmallerSizeHalfUp_PT: OnPlace: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif

                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Scale + Translate
                GameObject model = this.gameObject.FindChild("model");
                if (model == null)
                    model = this.gameObject.FindChild("copper_small");

                if (model != null)
                {
                    model.transform.localScale *= 0.75f;
                    float halfHeight = 0.1f;
                    Renderer rend = model.GetComponentInChildren<Renderer>();
                    if (rend != null)
                        halfHeight = rend.bounds.size.y * 0.5f;
                    else
                    {
                        Collider col = model.GetComponentInChildren<Collider>();
                        if (col != null)
                            halfHeight = col.bounds.size.y * 0.5f;
                    }
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + halfHeight, model.transform.localPosition.z);
                }
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: SmallerSizeHalfUp_PT: OnProtoDeserialize: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class ThirdSize_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Scale
                GameObject model = this.gameObject.FindChild("model");
                if (model == null)
                    model = this.gameObject.FindChild("Sulphur_small");

                if (model != null)
                    model.transform.localScale *= 0.35f;
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: ThirdSize_PT: OnPlace: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif

                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Scale
                GameObject model = this.gameObject.FindChild("model");
                if (model == null)
                    model = this.gameObject.FindChild("Sulphur_small");

                if (model != null)
                    model.transform.localPosition *= 0.35f;
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: ThirdSize_PT: OnProtoDeserialize: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class HalfSize_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Scale
                GameObject model = this.gameObject.FindChild("model");
                if (model == null)
                    model = this.gameObject.FindChild("Quartz_small");
                if (model == null)
                    model = this.gameObject.FindChild("Lithium_small");
                if (model == null)
                    model = this.gameObject.FindChild("Magnetite_small");
                if (model == null)
                    model = this.gameObject.FindChild("Diamond_small");
                if (model == null)
                    model = this.gameObject.FindChild("Niсkel_ore_small");

                if (model != null)
                    model.transform.localScale *= 0.5f;
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: HalfSize_PT: OnPlace: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif

                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Scale
                GameObject model = this.gameObject.FindChild("model");
                if (model == null)
                    model = this.gameObject.FindChild("Quartz_small");
                if (model == null)
                    model = this.gameObject.FindChild("Lithium_small");
                if (model == null)
                    model = this.gameObject.FindChild("Magnetite_small");
                if (model == null)
                    model = this.gameObject.FindChild("Diamond_small");
                if (model == null)
                    model = this.gameObject.FindChild("Niсkel_ore_small");

                if (model != null)
                    model.transform.localPosition *= 0.5f;
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: HalfSize_PT: OnProtoDeserialize: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class SmallerSize_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Scale
                GameObject model = this.gameObject.FindChild("model");
                if (model == null)
                    model = this.gameObject.FindChild("Lead_small");

                if (model != null)
                    model.transform.localScale *= 0.75f;
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: SmallerSize_PT: OnPlace: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif

                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Scale
                GameObject model = this.gameObject.FindChild("model");
                if (model == null)
                    model = this.gameObject.FindChild("Lead_small");

                if (model != null)
                    model.transform.localPosition *= 0.75f;
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: SmallerSize_PT: OnProtoDeserialize: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class HalfUp_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject model = this.gameObject.FindChild("model");
                if (model == null)
                    model = this.gameObject.FindChild("Aramid_fibers");

                if (model != null)
                {
                    float halfHeight = 0.1f;
                    Renderer rend = model.GetComponentInChildren<Renderer>();
                    if (rend != null)
                        halfHeight = rend.bounds.size.y * 0.5f;
                    else
                    {
                        Collider col = model.GetComponentInChildren<Collider>();
                        if (col != null)
                            halfHeight = col.bounds.size.y * 0.5f;
                    }
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + halfHeight, model.transform.localPosition.z);
                }
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: HalfUp_PT: OnPlace: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif
                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject model = this.gameObject.FindChild("model");
                if (model == null)
                    model = this.gameObject.FindChild("Aramid_fibers");

                if (model != null)
                {
                    float halfHeight = 0.1f;
                    Renderer rend = model.GetComponentInChildren<Renderer>();
                    if (rend != null)
                        halfHeight = rend.bounds.size.y * 0.5f;
                    else
                    {
                        Collider col = model.GetComponentInChildren<Collider>();
                        if (col != null)
                            halfHeight = col.bounds.size.y * 0.5f;
                    }
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + halfHeight, model.transform.localPosition.z);
                }
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: HalfUp_PT: OnProtoDeserialize: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class FourthUp_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Translate
                GameObject model = this.gameObject.FindChild("model");
                if (model == null)
                    model = this.gameObject.FindChild("sea_treader_poop_01");

                if (model != null)
                {
                    float halfHeight = 0.1f;
                    Renderer rend = model.GetComponentInChildren<Renderer>();
                    if (rend != null)
                        halfHeight = rend.bounds.size.y * 0.4f;
                    else
                    {
                        Collider col = model.GetComponentInChildren<Collider>();
                        if (col != null)
                            halfHeight = col.bounds.size.y * 0.4f;
                    }
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + halfHeight, model.transform.localPosition.z);
                }
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: FourthUp_PT: OnPlace: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif
                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Translate
                GameObject model = this.gameObject.FindChild("model");
                if (model == null)
                    model = this.gameObject.FindChild("sea_treader_poop_01");

                if (model != null)
                {
                    float halfHeight = 0.1f;
                    Renderer rend = model.GetComponentInChildren<Renderer>();
                    if (rend != null)
                        halfHeight = rend.bounds.size.y * 0.4f;
                    else
                    {
                        Collider col = model.GetComponentInChildren<Collider>();
                        if (col != null)
                            halfHeight = col.bounds.size.y * 0.4f;
                    }
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + halfHeight, model.transform.localPosition.z);
                }
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: FourthUp_PT: OnProtoDeserialize: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class HalfSizeHalfUp_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Scale + Translate
                GameObject model = this.gameObject.FindChild("model");
                if (model != null)
                {
                    model.transform.localScale *= 0.5f;
                    float halfHeight = 0.1f;
                    Renderer rend = model.GetComponentInChildren<Renderer>();
                    if (rend != null)
                        halfHeight = rend.bounds.size.y * 0.5f;
                    else
                    {
                        Collider col = model.GetComponentInChildren<Collider>();
                        if (col != null)
                            halfHeight = col.bounds.size.y * 0.5f;
                    }
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + halfHeight, model.transform.localPosition.z);
                }
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: HalfSizeHalfUp_PT: OnPlace: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif
                
                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Scale + Translate
                GameObject model = this.gameObject.FindChild("model");
                if (model != null)
                {
                    model.transform.localScale *= 0.5f;
                    float halfHeight = 0.1f;
                    Renderer rend = model.GetComponentInChildren<Renderer>();
                    if (rend != null)
                        halfHeight = rend.bounds.size.y * 0.5f;
                    else
                    {
                        Collider col = model.GetComponentInChildren<Collider>();
                        if (col != null)
                            halfHeight = col.bounds.size.y * 0.5f;
                    }
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + halfHeight, model.transform.localPosition.z);
                }
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: HalfSizeHalfUp_PT: OnProtoDeserialize: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif
                
                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class HalfSizeFourthUp_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Scale + Translate
                GameObject model = this.gameObject.FindChild("model");
                if (model != null)
                {
                    model.transform.localScale *= 0.5f;
                    float thirdHeight = 0.05f;
                    Renderer rend = model.GetComponentInChildren<Renderer>();
                    if (rend != null)
                        thirdHeight = rend.bounds.size.y * 0.4f;
                    else
                    {
                        Collider col = model.GetComponentInChildren<Collider>();
                        if (col != null)
                            thirdHeight = col.bounds.size.y * 0.4f;
                    }
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + thirdHeight, model.transform.localPosition.z);
                }
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: HalfSizeFourthUp_PT: OnPlace: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif

            HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Scale + Translate
                GameObject model = this.gameObject.FindChild("model");
                if (model != null)
                {
                    model.transform.localScale *= 0.5f;
                    float thirdHeight = 0.05f;
                    Renderer rend = model.GetComponentInChildren<Renderer>();
                    if (rend != null)
                        thirdHeight = rend.bounds.size.y * 0.4f;
                    else
                    {
                        Collider col = model.GetComponentInChildren<Collider>();
                        if (col != null)
                            thirdHeight = col.bounds.size.y * 0.4f;
                    }
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + thirdHeight, model.transform.localPosition.z);
                }
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: HalfSizeFourthUp_PT: OnProtoDeserialize: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */
    /*
    public class HalfSizeLittleUp_PT : PlaceTool, IProtoEventListener
    {
        [SerializeField]
        public bool HasBeenPlaced = false;

        public override void OnDraw(Player p)
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Show();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnHolster();
        }

        public override void OnPlace()
        {
            this.gameObject.GetComponent<CustomPlaceToolController>().Hide();
            base.OnPlace();

            if (!HasBeenPlaced)
            {
                // Scale + Translate
                GameObject model = this.gameObject.FindChild("model");
                if (model == null)
                    model = this.gameObject.FindChild("Uraninite_crystal_small");
                if (model == null)
                    model = this.gameObject.FindChild("kyanite_small_03");
                if (model == null)
                    model = this.gameObject.FindChild("Coral_reef_jeweled_disk_purple_01_01");
                if (model == null)
                    model = this.gameObject.FindChild("Aluminium_Oxide_small");

                if (model != null)
                {
                    model.transform.localScale *= 0.5f;
                    float thirdHeight = 0.05f;
                    Renderer rend = model.GetComponentInChildren<Renderer>();
                    if (rend != null)
                        thirdHeight = rend.bounds.size.y * 0.1f;
                    else
                    {
                        Collider col = model.GetComponentInChildren<Collider>();
                        if (col != null)
                            thirdHeight = col.bounds.size.y * 0.1f;
                    }
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + thirdHeight, model.transform.localPosition.z);
                }
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: HalfSizeLittleUp_PT: OnPlace: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif

                HasBeenPlaced = true;
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                // Scale + Translate
                GameObject model = this.gameObject.FindChild("model");
                if (model == null)
                    model = this.gameObject.FindChild("Uraninite_crystal_small");
                if (model == null)
                    model = this.gameObject.FindChild("kyanite_small_03");
                if (model == null)
                    model = this.gameObject.FindChild("Coral_reef_jeweled_disk_purple_01_01");
                if (model == null)
                    model = this.gameObject.FindChild("Aluminium_Oxide_small");

                if (model != null)
                {
                    model.transform.localScale *= 0.5f;
                    float thirdHeight = 0.05f;
                    Renderer rend = model.GetComponentInChildren<Renderer>();
                    if (rend != null)
                        thirdHeight = rend.bounds.size.y * 0.1f;
                    else
                    {
                        Collider col = model.GetComponentInChildren<Collider>();
                        if (col != null)
                            thirdHeight = col.bounds.size.y * 0.1f;
                    }
                    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + thirdHeight, model.transform.localPosition.z);
                }
#if DEBUG
                else
                {
                    Logger.Log("DEBUG: HalfSizeLittleUp_PT: OnProtoDeserialize: Model not found!");
                    Logger.PrintTransform(this.gameObject.transform);
                }
#endif

                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }
    }
    */

    #endregion
}
