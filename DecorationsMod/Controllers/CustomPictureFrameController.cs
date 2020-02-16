using Harmony;
using SMLHelper.V2.Utility;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace DecorationsMod.Controllers
{
    public static class PictureFrameEnumHelper
    {
        private static bool _initialized = false;
        private static object _noneEnumValue = 0;
        private static object _fullEnumValue = 2;

        public static object NoneEnumValue
        {
            get
            {
                if (!_initialized)
                    GetPictureFrameStateEnums();
                return _noneEnumValue;
            }
            set
            {
                if (!_initialized)
                    GetPictureFrameStateEnums();
                _noneEnumValue = value;
            }
        }

        public static object FullEnumValue
        {
            get
            {
                if (!_initialized)
                    GetPictureFrameStateEnums();
                return _fullEnumValue;
            }
            set
            {
                if (!_initialized)
                    GetPictureFrameStateEnums();
                _fullEnumValue = value;
            }
        }

        public static void GetPictureFrameStateEnums()
        {
            if (!_initialized)
            {
                // Retrieve picture state enum values
                Type StateEnum = typeof(PictureFrame).GetNestedType("State", BindingFlags.NonPublic | BindingFlags.Instance);
                FieldInfo[] fields = StateEnum.GetFields();
                foreach (var field in fields)
                {
                    if (field.Name.Equals("value__"))
                        continue;
                    if (field.Name.CompareTo("None") == 0)
                        _noneEnumValue = field.GetRawConstantValue();
                    if (field.Name.CompareTo("Full") == 0)
                        _fullEnumValue = field.GetRawConstantValue();
                }
                _initialized = true;
            }
        }
    }

    [HarmonyPatch(typeof(PictureFrame))]
    [HarmonyPatch("OnHandHover")]
    [HarmonyPatch("OnHandClick")]
    public class PictureFramePatch
    {
        public static void OnHandHover_Postfix(PictureFrame __instance, HandTargetEventData eventData)
        {
            if (!__instance.enabled)
                return;
            if (__instance.gameObject.name.StartsWith("CustomPictureFrame(Clone)"))
                HandReticle.main.SetInteractText(LanguageHelper.GetFriendlyWord("CustomPictureFrameTooltip"));
            // else, we are in regular PictureFrame
            return;
        }

        public static bool OnHandClick_Prefix(PictureFrame __instance, HandTargetEventData eventData)
        {
            if (!__instance.enabled)
                return true;
            if (__instance.gameObject.name.StartsWith("CustomPictureFrame(Clone)"))
            {
                if (Input.GetKey(KeyCode.R))
                {
                    CustomPictureFrameController cpfController = __instance.gameObject.GetComponent<CustomPictureFrameController>();
                    GameObject model = __instance.gameObject.FindChild("mesh");
                    GameObject posterMagnet = __instance.gameObject.FindChild("poster_kitty(Clone)");
                    GameObject posterMagnetModel = posterMagnet.FindChild("model");

                    // Restore size
                    model.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    posterMagnetModel.transform.localPosition = new Vector3(posterMagnetModel.transform.localPosition.x, 0.0110998f, posterMagnetModel.transform.localPosition.z);
                    posterMagnetModel.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    if (model.transform.localEulerAngles != cpfController.OriginEulerAngles)
                        __instance.imageRenderer.transform.localScale = new Vector3(0.685f, 1.275f, 0.25f);
                    else
                        __instance.imageRenderer.transform.localScale = new Vector3(1.275f, 0.685f, 0.25f);
                    __instance.imageRenderer.transform.localPosition = new Vector3(__instance.imageRenderer.transform.localPosition.x, __instance.imageRenderer.transform.localPosition.y, 0.0125f);

                    // Rotate model and adjust magnet position
                    if (model.transform.localEulerAngles != cpfController.OriginEulerAngles)
                    {
                        model.transform.localEulerAngles = cpfController.OriginEulerAngles;
                        posterMagnet.transform.localPosition = new Vector3(0.0f, -0.02f, -0.002f);
                    }
                    else
                    {
                        model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y, model.transform.localEulerAngles.z + 90.0f);
                        posterMagnet.transform.localPosition = new Vector3(0.0f, 0.27f, -0.002f);
                    }

                    // Rotate collider
                    GameObject trigger = __instance.gameObject.FindChild("Trigger");
                    BoxCollider collider = trigger.GetComponent<BoxCollider>();
                    collider.size = new Vector3(collider.size.y, collider.size.x, collider.size.z);

                    // Rotate image
                    __instance.imageRenderer.transform.localScale = new Vector3(__instance.imageRenderer.transform.localScale.y, __instance.imageRenderer.transform.localScale.x, __instance.imageRenderer.transform.localScale.z);

                    // Rotate constructable bounds extents
                    var constructableBounds = __instance.gameObject.GetComponent<ConstructableBounds>();
                    constructableBounds.bounds.extents = new Vector3(constructableBounds.bounds.extents.y, constructableBounds.bounds.extents.x, constructableBounds.bounds.extents.z);
                    
                    // Refresh picture
                    Type PictureFrameType = typeof(PictureFrame);
                    MethodInfo SetStateMethod = PictureFrameType.GetMethod("SetState", BindingFlags.NonPublic | BindingFlags.Instance);
                    SetStateMethod.Invoke(__instance, new object[] { PictureFrameEnumHelper.NoneEnumValue });
                    SetStateMethod.Invoke(__instance, new object[] { PictureFrameEnumHelper.FullEnumValue });

                    return false;
                }
                else if (Input.GetKey(KeyCode.F))
                {
                    // Hide/display frame border
                    GameObject model = __instance.gameObject.FindChild("mesh");
                    GameObject pictureFrame = model.FindChild("submarine_Picture_Frame");
                    GameObject frameButton = pictureFrame.FindChild("submarine_Picture_Frame_button");
                    MeshRenderer frameRenderer = pictureFrame.GetComponent<MeshRenderer>();
                    MeshRenderer buttonRenderer = frameButton.GetComponent<MeshRenderer>();
                    bool pictureFrameRenderer = frameRenderer.enabled;
                    if (pictureFrameRenderer)
                    {
                        // Disable picture frame borders
                        frameRenderer.enabled = false;
                        buttonRenderer.enabled = false;
                        // Enable poster magnet
                        GameObject posterMagnet = __instance.gameObject.FindChild("poster_kitty(Clone)");
                        GameObject magnetModel = posterMagnet.FindChild("model").FindChild("poster_kitty");
                        MeshRenderer magnetRenderer = magnetModel.GetComponent<MeshRenderer>();
                        magnetRenderer.enabled = true;
                    }
                    else
                    {
                        GameObject posterMagnet = __instance.gameObject.FindChild("poster_kitty(Clone)");
                        GameObject magnetModel = posterMagnet.FindChild("model").FindChild("poster_kitty");
                        MeshRenderer magnetRenderer = magnetModel.GetComponent<MeshRenderer>();
                        if (magnetRenderer.enabled)
                            magnetRenderer.enabled = false; // Disable poster magnet
                        else
                        {
                            frameRenderer.enabled = true; // Enable picture frame border
                            buttonRenderer.enabled = true; // Enable picture frame border
                        }
                    }
                    return false;
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    GameObject model = __instance.gameObject.FindChild("mesh");
                    GameObject posterMagnet = __instance.gameObject.FindChild("poster_kitty(Clone)");
                    GameObject posterMagnetModel = posterMagnet.FindChild("model");
                    CustomPictureFrameController cpfController = __instance.gameObject.GetComponent<CustomPictureFrameController>();

                    // CustomPictureFrame scale ratio step
                    float scaleRatio = 1.2f;

                    // Minimum CustomPictureFrame size = normal size / minSizeRatio
                    float minSizeRatio = 4.0f;

                    if (model.transform.localScale.x >= 3.0f)
                    {
                        // Set minimum size
                        model.transform.localScale = new Vector3((1.0f / minSizeRatio), (1.0f / minSizeRatio), (1.0f / minSizeRatio));
                        if (model.transform.localEulerAngles != cpfController.OriginEulerAngles)
                            posterMagnet.transform.localPosition = new Vector3(0.0f, (0.27f / minSizeRatio), -0.002f);
                        else
                            posterMagnet.transform.localPosition = new Vector3(0.0f, (-0.02f / minSizeRatio), -0.002f);
                        posterMagnetModel.transform.localPosition = new Vector3(posterMagnetModel.transform.localPosition.x, (0.0110998f / minSizeRatio), posterMagnetModel.transform.localPosition.z);
                        posterMagnetModel.transform.localScale = new Vector3((0.8f / minSizeRatio), (0.8f / minSizeRatio), (0.8f / minSizeRatio));
                        if (model.transform.localEulerAngles != cpfController.OriginEulerAngles)
                            __instance.imageRenderer.transform.localScale = new Vector3((0.685f / minSizeRatio), (1.275f / minSizeRatio), 0.25f);
                        else
                            __instance.imageRenderer.transform.localScale = new Vector3((1.275f / minSizeRatio), (0.685f / minSizeRatio), 0.25f);
                        __instance.imageRenderer.transform.localPosition = new Vector3(__instance.imageRenderer.transform.localPosition.x, __instance.imageRenderer.transform.localPosition.y, (0.0125f / minSizeRatio));
                    }
                    else
                    {
                        // Increase size
                        model.transform.localScale *= scaleRatio;
                        posterMagnet.transform.localPosition = new Vector3(posterMagnet.transform.localPosition.x, posterMagnet.transform.localPosition.y * scaleRatio, posterMagnet.transform.localPosition.z);
                        posterMagnetModel.transform.localPosition = new Vector3(posterMagnetModel.transform.localPosition.x, posterMagnetModel.transform.localPosition.y * scaleRatio, posterMagnetModel.transform.localPosition.z);
                        posterMagnetModel.transform.localScale *= scaleRatio;
                        __instance.imageRenderer.transform.localScale *= scaleRatio;
                        __instance.imageRenderer.transform.localPosition = new Vector3(__instance.imageRenderer.transform.localPosition.x, __instance.imageRenderer.transform.localPosition.y, __instance.imageRenderer.transform.localPosition.z * scaleRatio);
                    }
                    
                    // Refresh picture
                    Type PictureFrameType = typeof(PictureFrame);
                    FieldInfo current = PictureFrameType.GetField("current", BindingFlags.NonPublic | BindingFlags.Instance);
                    object currentEnumValue = current.GetValue(__instance);
                    MethodInfo SetStateMethod = PictureFrameType.GetMethod("SetState", BindingFlags.NonPublic | BindingFlags.Instance);
                    SetStateMethod.Invoke(__instance, new object[] { PictureFrameEnumHelper.NoneEnumValue });
                    currentEnumValue = current.GetValue(__instance);
                    SetStateMethod.Invoke(__instance, new object[] { PictureFrameEnumHelper.FullEnumValue });
                    currentEnumValue = current.GetValue(__instance);

                    return false;
                }
            }
            return true;
        }
    }
    
    public class CustomPictureFrameController : MonoBehaviour, IProtoEventListener
    {
        public Vector3 OriginEulerAngles = Vector3.zero;
        public Vector3 OriginColliderSize = Vector3.zero;
        public Vector3 OriginImageRendererScale = Vector3.zero;
        public Vector3 OriginConstructableBoundsExtents = Vector3.zero;
        
        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            // Retrieve save file
            PrefabIdentifier id = GetComponent<PrefabIdentifier>();
            if (id == null)
                if ((id = GetComponentInParent<PrefabIdentifier>()) == null)
                    return;
            
            string filePath = Path.Combine(FilesHelper.GetSaveFolderPath(), "custompictureframe_" + id.Id + ".txt");
            if (File.Exists(filePath))
            {
                GameObject model = this.gameObject.FindChild("mesh");
                PictureFrame pf = this.gameObject.GetComponent<PictureFrame>();

                string tmpSize = File.ReadAllText(filePath).Replace(',', '.'); // Replace , with . for backward compatibility.
                string[] sizes = tmpSize.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (sizes.Length == 10)
                {
                    // Restore model angles
                    string[] eulerAngles = sizes[0].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (eulerAngles.Length == 3)
                    {
                        Vector3 savedEulerAngles = new Vector3(OriginEulerAngles.x, OriginEulerAngles.y, OriginEulerAngles.z);
                        float.TryParse(eulerAngles[0], NumberStyles.Float, CultureInfo.InvariantCulture, out savedEulerAngles.x);
                        float.TryParse(eulerAngles[1], NumberStyles.Float, CultureInfo.InvariantCulture, out savedEulerAngles.y);
                        float.TryParse(eulerAngles[2], NumberStyles.Float, CultureInfo.InvariantCulture, out savedEulerAngles.z);
                        model.transform.localEulerAngles = savedEulerAngles;
                    }

                    // Restore collider size
                    string[] colliderSize = sizes[1].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (colliderSize.Length == 3)
                    {
                        Vector3 savedColliderSize = new Vector3(OriginColliderSize.x, OriginColliderSize.y, OriginColliderSize.z);
                        float.TryParse(colliderSize[0], NumberStyles.Float, CultureInfo.InvariantCulture, out savedColliderSize.x);
                        float.TryParse(colliderSize[1], NumberStyles.Float, CultureInfo.InvariantCulture, out savedColliderSize.y);
                        float.TryParse(colliderSize[2], NumberStyles.Float, CultureInfo.InvariantCulture, out savedColliderSize.z);
                        GameObject trigger = this.gameObject.FindChild("Trigger");
                        BoxCollider collider = trigger.GetComponent<BoxCollider>();
                        collider.size = savedColliderSize;
                    }

                    // Restore picture scale
                    string[] imageRendererScale = sizes[2].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (imageRendererScale.Length == 3)
                    {
                        Vector3 savedImageRendererScale = new Vector3(OriginImageRendererScale.x, OriginImageRendererScale.y, OriginImageRendererScale.z);
                        float.TryParse(imageRendererScale[0], NumberStyles.Float, CultureInfo.InvariantCulture, out savedImageRendererScale.x);
                        float.TryParse(imageRendererScale[1], NumberStyles.Float, CultureInfo.InvariantCulture, out savedImageRendererScale.y);
                        float.TryParse(imageRendererScale[2], NumberStyles.Float, CultureInfo.InvariantCulture, out savedImageRendererScale.z);
                        pf.imageRenderer.transform.localScale = savedImageRendererScale;
                    }

                    // Restore frame border visibility
                    GameObject pictureFrame = model.FindChild("submarine_Picture_Frame");
                    MeshRenderer frameRenderer = pictureFrame.GetComponent<MeshRenderer>();
                    frameRenderer.enabled = ((sizes[3].CompareTo("1") == 0) ? true : false);
                    GameObject frameButton = pictureFrame.FindChild("submarine_Picture_Frame_button");
                    MeshRenderer buttonRenderer = frameButton.GetComponent<MeshRenderer>();
                    buttonRenderer.enabled = ((sizes[3].CompareTo("1") == 0) ? true : false);
                    GameObject posterMagnet = this.gameObject.FindChild("poster_kitty(Clone)");
                    GameObject posterMagnetModel = posterMagnet.FindChild("model");
                    GameObject magnetModel = posterMagnetModel.FindChild("poster_kitty");
                    MeshRenderer magnetRenderer = magnetModel.GetComponent<MeshRenderer>();
                    magnetRenderer.enabled = ((sizes[3].CompareTo("2") == 0) ? true : false);

                    // Restore constructable bounds extents
                    string[] constructableBoundsExtents = sizes[4].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (constructableBoundsExtents.Length == 3)
                    {
                        Vector3 savedConstructableBoundsExtents = new Vector3(OriginConstructableBoundsExtents.x, OriginConstructableBoundsExtents.y, OriginConstructableBoundsExtents.z);
                        float.TryParse(constructableBoundsExtents[0], NumberStyles.Float, CultureInfo.InvariantCulture, out savedConstructableBoundsExtents.x);
                        float.TryParse(constructableBoundsExtents[1], NumberStyles.Float, CultureInfo.InvariantCulture, out savedConstructableBoundsExtents.y);
                        float.TryParse(constructableBoundsExtents[2], NumberStyles.Float, CultureInfo.InvariantCulture, out savedConstructableBoundsExtents.z);
                        ConstructableBounds constructableBounds = this.gameObject.GetComponent<ConstructableBounds>();
                        constructableBounds.bounds.extents = savedConstructableBoundsExtents;
                    }

                    // Restore picture frame sizes & positions
                    string[] modelScale = sizes[5].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (modelScale.Length == 3)
                    {
                        Vector3 updateModelScale = Vector3.zero;
                        float.TryParse(modelScale[0], NumberStyles.Float, CultureInfo.InvariantCulture, out updateModelScale.x);
                        float.TryParse(modelScale[1], NumberStyles.Float, CultureInfo.InvariantCulture, out updateModelScale.y);
                        float.TryParse(modelScale[2], NumberStyles.Float, CultureInfo.InvariantCulture, out updateModelScale.z);
                        model.transform.localScale = updateModelScale;
                    }
                    string[] posterMagnetPosition = sizes[6].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (posterMagnetPosition.Length == 3)
                    {
                        Vector3 updatePosterMagnetPosition = Vector3.zero;
                        float.TryParse(posterMagnetPosition[0], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterMagnetPosition.x);
                        float.TryParse(posterMagnetPosition[1], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterMagnetPosition.y);
                        float.TryParse(posterMagnetPosition[2], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterMagnetPosition.z);
                        posterMagnet.transform.localPosition = updatePosterMagnetPosition;
                    }
                    string[] posterMagnetModelPosition = sizes[7].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (posterMagnetModelPosition.Length == 3)
                    {
                        Vector3 updatePosterMagnetModelPosition = Vector3.zero;
                        float.TryParse(posterMagnetModelPosition[0], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterMagnetModelPosition.x);
                        float.TryParse(posterMagnetModelPosition[1], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterMagnetModelPosition.y);
                        float.TryParse(posterMagnetModelPosition[2], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterMagnetModelPosition.z);
                        posterMagnetModel.transform.localPosition = updatePosterMagnetModelPosition;
                    }
                    string[] posterMagnetModelScale = sizes[8].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (posterMagnetModelScale.Length == 3)
                    {
                        Vector3 updatePosterMagnetModelScale = Vector3.zero;
                        float.TryParse(posterMagnetModelScale[0], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterMagnetModelScale.x);
                        float.TryParse(posterMagnetModelScale[1], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterMagnetModelScale.y);
                        float.TryParse(posterMagnetModelScale[2], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterMagnetModelScale.z);
                        posterMagnetModel.transform.localScale = updatePosterMagnetModelScale;
                    }
                    string[] imageRendererPosition = sizes[9].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (imageRendererPosition.Length == 3)
                    {
                        Vector3 updateImageRendererPosition = Vector3.zero;
                        float.TryParse(imageRendererPosition[0], NumberStyles.Float, CultureInfo.InvariantCulture, out updateImageRendererPosition.x);
                        float.TryParse(imageRendererPosition[1], NumberStyles.Float, CultureInfo.InvariantCulture, out updateImageRendererPosition.y);
                        float.TryParse(imageRendererPosition[2], NumberStyles.Float, CultureInfo.InvariantCulture, out updateImageRendererPosition.z);
                        pf.imageRenderer.transform.localPosition = updateImageRendererPosition;
                    }
                    
                    // Refresh picture
                    Type PictureFrameType = typeof(PictureFrame);
                    MethodInfo SetStateMethod = PictureFrameType.GetMethod("SetState", BindingFlags.NonPublic | BindingFlags.Instance);
                    SetStateMethod.Invoke(pf, new object[] { PictureFrameEnumHelper.NoneEnumValue });
                    SetStateMethod.Invoke(pf, new object[] { PictureFrameEnumHelper.FullEnumValue });
                }
            }
        }

        public void OnProtoSerialize(ProtobufSerializer serializer)
        {
            // Retrieve prefab unique ID
            PrefabIdentifier id = GetComponent<PrefabIdentifier>();
            if (id == null)
                if ((id = GetComponentInParent<PrefabIdentifier>()) == null)
                    return;
            
            // Get saves folder and create it if it doesn't exist
            string saveFolder = FilesHelper.GetSaveFolderPath();
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            // Save model angles
            GameObject model = this.gameObject.FindChild("mesh");
            string saveData = model.transform.localEulerAngles.x + "|" + 
                              model.transform.localEulerAngles.y + "|" + 
                              model.transform.localEulerAngles.z + Environment.NewLine;

            // Save collider size
            GameObject trigger = this.gameObject.FindChild("Trigger");
            BoxCollider collider = trigger.GetComponent<BoxCollider>();
            saveData += collider.size.x + "|" + 
                        collider.size.y + "|" + 
                        collider.size.z + Environment.NewLine;

            // Save picture scale
            PictureFrame pf = this.gameObject.GetComponent<PictureFrame>();
            saveData += pf.imageRenderer.transform.localScale.x + "|" + 
                        pf.imageRenderer.transform.localScale.y + "|" + 
                        pf.imageRenderer.transform.localScale.z + Environment.NewLine;

            // Save frame border visibility
            GameObject pictureFrame = model.FindChild("submarine_Picture_Frame");
            GameObject posterMagnet = this.gameObject.FindChild("poster_kitty(Clone)");
            GameObject posterMagnetModel = posterMagnet.FindChild("model");
            MeshRenderer frameRenderer = pictureFrame.GetComponent<MeshRenderer>();
            if (frameRenderer.enabled)
                saveData += "1" + Environment.NewLine;
            else
            {
                GameObject magnetModel = posterMagnet.FindChild("model").FindChild("poster_kitty");
                MeshRenderer magnetRenderer = magnetModel.GetComponent<MeshRenderer>();
                if (magnetRenderer.enabled)
                    saveData += "2" + Environment.NewLine;
                else
                    saveData += "0" + Environment.NewLine;
            }

            // Save constructable bounds extents
            ConstructableBounds constructableBounds = this.gameObject.GetComponent<ConstructableBounds>();
            saveData += constructableBounds.bounds.extents.x + "|" +
                        constructableBounds.bounds.extents.y + "|" +
                        constructableBounds.bounds.extents.z + Environment.NewLine;

            // Save current sizes
            saveData += model.transform.localScale.x + "|" + model.transform.localScale.y + "|" + model.transform.localScale.z + Environment.NewLine;
            saveData += posterMagnet.transform.localPosition.x + "|" + posterMagnet.transform.localPosition.y + "|" + posterMagnet.transform.localPosition.z + Environment.NewLine;
            saveData += posterMagnetModel.transform.localPosition.x + "|" + posterMagnetModel.transform.localPosition.y + "|" + posterMagnetModel.transform.localPosition.z + Environment.NewLine;
            saveData += posterMagnetModel.transform.localScale.x + "|" + posterMagnetModel.transform.localScale.y + "|" + posterMagnetModel.transform.localScale.z + Environment.NewLine;
            saveData += pf.imageRenderer.transform.localPosition.x + "|" + pf.imageRenderer.transform.localPosition.y + "|" + pf.imageRenderer.transform.localPosition.z + Environment.NewLine;
            //pf.imageRenderer.transform.localScale

            // Save state to file
            File.WriteAllText(Path.Combine(saveFolder, "custompictureframe_" + id.Id + ".txt"), saveData);
        }
    }
}
