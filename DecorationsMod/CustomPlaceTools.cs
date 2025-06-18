using DecorationsMod.Controllers;
using ProtoBuf;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using HarmonyLib;

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

        public GenericPlaceTool_PT(float posX = 0.0f, float posY = 0.0f, float posZ = 0.0f, string modelName = "model", float rotX = 0.0f, float rotY = 0.0f, float rotZ = 0.0f, float modelScale = 1.0f)
        {
            this.ModelName = modelName;
            this.PositionX = posX;
            this.PositionY = posY;
            this.PositionZ = posZ;
            this.RotateX = rotX;
            this.RotateY = rotY;
            this.RotateZ = rotZ;
            this.ModelScale = modelScale;
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

        private void RotateScaleTranslate()
        {
            GameObject model = this.gameObject.FindChild(this.ModelName);
            if (model != null)
            {
#if DEBUG_PLACE_TOOL
                Logger.Debug("DEBUG: Material_PT: Current angle for [" + this.gameObject.name + "]=[" + model.transform.localEulerAngles.x.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" + model.transform.localEulerAngles.y.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" + model.transform.localEulerAngles.z.ToString(System.Globalization.CultureInfo.InvariantCulture) + "]");
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
                Logger.Debug("DEBUG: Material_PT: Model \"" + this.ModelName + "\" not found!");
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

            PrefabIdentifier pid = this.gameObject.GetComponent<PrefabIdentifier>();
            if (pid == null)
                pid = this.gameObject.GetComponentInChildren<PrefabIdentifier>();
            if (pid == null)
                pid = this.gameObject.GetComponentInParent<PrefabIdentifier>();

#if DEBUG_PLACE_TOOL
            Logger.Debug($"DEBUG: OnPlace(): Id=[{pid?.Id ?? "NULL"}] HasBeenPlaced=[{HasBeenPlaced}]");
#endif

            if (!HasBeenPlaced || (pid != null && !IsInPlacedByPlayerList(pid.ClassId, pid.Id)))
            {
                //if (Player.main.IsInSub())
                //{
                if (this.BrighterIllum > 0.0f || this.BrighterColor > 0.0f)
                    SetBrighter();
                RotateScaleTranslate();
                if (pid != null)
                {
#if DEBUG_PLACE_TOOL
                    Logger.Debug($"DEBUG: OnPlace(): Added to list!");
#endif
                    AddToPlacedByPlayerList(pid.ClassId, pid.Id);
                }
                HasBeenPlaced = true;
                //}
            }
            if (this.gameObject.name != null)
                PrefabsHelper.FixPlaceToolSkyAppliers(this.gameObject);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            if (!HasBeenPlaced)
            {
                PrefabIdentifier pid = this.gameObject.GetComponent<PrefabIdentifier>();
                if (pid == null)
                    pid = this.gameObject.GetComponentInChildren<PrefabIdentifier>();
                if (pid == null)
                    pid = this.gameObject.GetComponentInParent<PrefabIdentifier>();
#if DEBUG_PLACE_TOOL
                Logger.Debug($"DEBUG: OnProtoDeserialize(): Id=[{pid?.Id ?? "NULL"}] HasBeenPlaced=[{HasBeenPlaced}]");
#endif

                if (pid != null && IsInPlacedByPlayerList(pid.ClassId, pid.Id))
                {
#if DEBUG_PLACE_TOOL
                    Logger.Debug($"DEBUG: OnProtoDeserialize(): Adjusting!");
#endif
                    if (this.BrighterIllum > 0.0f || this.BrighterColor > 0.0f)
                        SetBrighter();
                    RotateScaleTranslate();
                }
                HasBeenPlaced = true;
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer) { }

#endregion

        #region Static functions

        public static List<string> _placedByPlayer = new List<string>();

        public static bool IsInPlacedByPlayerList(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;
            return _placedByPlayer.Contains(id);
        }

        public static bool IsInPlacedByPlayerList(string classId, string prefabId)
        {
            if (string.IsNullOrEmpty(classId))
                return false;
            if (string.IsNullOrEmpty(prefabId))
                return false;
            return IsInPlacedByPlayerList($"{classId}+{prefabId}");
        }

        public static void AddToPlacedByPlayerList(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;
            if (!id.Contains("+"))
                return;
            if (_placedByPlayer.Contains(id))
                return;
            _placedByPlayer.Add(id);
        }

        public static void AddToPlacedByPlayerList(string classId, string prefabId)
        {
            if (string.IsNullOrEmpty(classId))
                return;
            if (string.IsNullOrEmpty(prefabId))
                return;
            AddToPlacedByPlayerList($"{classId}+{prefabId}");
        }

        public static bool SavePlacedByPlayerList()
        {
            string saveFolder = FilesHelper.GetSaveFolderPath();
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            try { File.WriteAllText(FilesHelper.Combine(saveFolder, "placedByPlayer.txt"), _placedByPlayer.Join(null, ", "), Encoding.UTF8); return true; }
            catch { return false; }
        }

        public static void LoadPlacedByPlayerList(string saveGame)
        {
            _placedByPlayer.Clear();
            string saveDir = FilesHelper.GetSaveFolderPath(saveGame);
            if (!string.IsNullOrEmpty(saveDir) && Directory.Exists(saveDir))
            {
                string filePath = Path.Combine(saveDir, "placedByPlayer.txt");
                if (File.Exists(filePath))
                {
                    string placedByPlayer = File.ReadAllText(filePath, Encoding.UTF8);
                    if (!string.IsNullOrEmpty(placedByPlayer))
                    {
                        string[] splitted = placedByPlayer.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                        if (splitted != null && splitted.Length > 0)
                            foreach (string id in splitted)
                                AddToPlacedByPlayerList(id);
                    }
                }
            }
        }

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

    public class Snack1_PT : GenericPlaceTool_PT
    {
        public Snack1_PT() : base(0.0f, 0.11f, 0.0f, "Snack_01") { }
    }

    public class Snack2_PT : GenericPlaceTool_PT
    {
        public Snack2_PT() : base(0.0f, 0.11f, 0.0f, "Snack_02") { }
    }

    public class Snack3_PT : GenericPlaceTool_PT
    {
        public Snack3_PT() : base(0.0f, 0.11f, 0.0f, "Snack_03") { }
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

    public class LithiumIonBattery_PT : GenericPlaceTool_PT
    {
        public LithiumIonBattery_PT() : base(0.0f, 0.069f, 0.0f, "model", 0.0f, 0.0f, 0.0f, 0.9f) { }
    }

    public class PowerCell_PT : GenericPlaceTool_PT
    {
        public PowerCell_PT() : base(0.0f, 0.138f, 0.0f, "engine_power_cell_01") { }
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
}
