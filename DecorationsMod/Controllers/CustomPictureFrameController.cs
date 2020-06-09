using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace DecorationsMod.Controllers
{
    public static class PictureFrameEnumHelper
    {
        public static readonly MethodInfo SetStateMethod = typeof(PictureFrame).GetMethod("SetState", BindingFlags.NonPublic | BindingFlags.Instance);

        private static bool _initialized = false;
        private static object _noneEnumValue = 0;
        private static object _thumbnailEnumValue = 1;
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

        public static object ThumbnailEnumValue
        {
            get
            {
                if (!_initialized)
                    GetPictureFrameStateEnums();
                return _thumbnailEnumValue;
            }
            set
            {
                if (!_initialized)
                    GetPictureFrameStateEnums();
                _thumbnailEnumValue = value;
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
                    if (string.Compare(field.Name, "None", true, CultureInfo.InvariantCulture) == 0)
                        _noneEnumValue = field.GetRawConstantValue();
                    else if (string.Compare(field.Name, "Thumbnail", true, CultureInfo.InvariantCulture) == 0)
                        _thumbnailEnumValue = field.GetRawConstantValue();
                    else if (string.Compare(field.Name, "Full", true, CultureInfo.InvariantCulture) == 0)
                        _fullEnumValue = field.GetRawConstantValue();
                }
                _initialized = true;
            }
        }
    }

    public class PictureFramePatch
    {
        public static void OnHandHover_Postfix(PictureFrame __instance, HandTargetEventData eventData)
        {
            if (!__instance.enabled)
                return;
            if (__instance.gameObject.name.StartsWith("CustomPictureFrame(Clone)", true, CultureInfo.InvariantCulture))
            {
#if BELOWZERO
                HandReticle.main.SetTextRaw(HandReticle.TextType.Hand, LanguageHelper.GetFriendlyWord(ConfigSwitcher.UseCompactTooltips ? "CustomPictureFrameTooltipCompact" : "CustomPictureFrameTooltip"));
#else
                HandReticle.main.SetInteractText(ConfigSwitcher.UseCompactTooltips ? "CustomPictureFrameTooltipCompact" : "CustomPictureFrameTooltip");
#endif
            }
            // else, we are in regular PictureFrame
            return;
        }

        public static bool OnHandClick_Prefix(PictureFrame __instance, HandTargetEventData eventData)
        {
            if (!__instance.enabled)
                return true;
            if (__instance.gameObject.name.StartsWith("CustomPictureFrame(Clone)", true, CultureInfo.InvariantCulture))
            {

                // Minimum CustomPictureFrame size = normal size / minSizeRatio
                float minSizeRatio = 4.0f;

                CustomPictureFrameController cpfController = __instance.gameObject.GetComponent<CustomPictureFrameController>();
                GameObject frame = __instance.gameObject.FindChild("mesh");
                GameObject poster = __instance.gameObject.FindChild("poster_decorations(Clone)");
                GameObject posterModel = poster.FindChild("model");
                GameObject magnetModel = posterModel.FindChild("poster_kitty");
                MeshRenderer magnetRenderer = magnetModel.GetComponent<MeshRenderer>();
                GameObject bgBisModel = posterModel.FindChild("poster_background_bis");
                MeshRenderer bgBisRenderer = bgBisModel.GetComponent<MeshRenderer>();
                GameObject bgPivotModel = posterModel.FindChild("poster_background_pivot");
                MeshRenderer bgPivotRenderer = bgPivotModel.GetComponent<MeshRenderer>();

                if (Input.GetKey(KeyCode.R))
                {
                    GameObject pictureFrame = frame.FindChild("submarine_Picture_Frame");
                    MeshRenderer frameRenderer = pictureFrame.GetComponent<MeshRenderer>();

                    // Rotate frame
                    if (cpfController.Flipped)
                        frame.transform.localEulerAngles = new Vector3(frame.transform.localEulerAngles.x, frame.transform.localEulerAngles.y, frame.transform.localEulerAngles.z + 90f);
                    else
                        frame.transform.localEulerAngles = new Vector3(frame.transform.localEulerAngles.x, frame.transform.localEulerAngles.y, frame.transform.localEulerAngles.z - 90f);

                    // Adjust poster style
                    if (!frameRenderer.enabled)
                    {
                        bgBisRenderer.enabled = !bgBisRenderer.enabled;
                        bgPivotRenderer.enabled = !bgPivotRenderer.enabled;
                        // Adjust magnet pos
                        if (bgPivotRenderer.enabled)
                            magnetModel.transform.localPosition = new Vector3(0f, -0.0115f, 0f);
                        else
                            magnetModel.transform.localPosition = new Vector3(0f, 0f, 0f);
                    }
                    else
                    {
                        bgBisRenderer.enabled = false;
                        bgPivotRenderer.enabled = false;
                    }

                    // Toogle flip
                    cpfController.Flipped = !cpfController.Flipped;

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
                    PictureFrameEnumHelper.SetStateMethod.Invoke(__instance, new object[] { PictureFrameEnumHelper.ThumbnailEnumValue });
                    PictureFrameEnumHelper.SetStateMethod.Invoke(__instance, new object[] { PictureFrameEnumHelper.FullEnumValue });

                    return false;
                }
                else if (Input.GetKey(KeyCode.F))
                {
                    // Get objects.
                    GameObject pictureFrame = frame.FindChild("submarine_Picture_Frame");
                    GameObject frameButton = pictureFrame.FindChild("submarine_Picture_Frame_button");
                    MeshRenderer frameRenderer = pictureFrame.GetComponent<MeshRenderer>();
                    MeshRenderer buttonRenderer = frameButton.GetComponent<MeshRenderer>();
                    GameObject bgModel = posterModel.FindChild("poster_background");
                    MeshRenderer bgRenderer = bgModel.GetComponent<MeshRenderer>();

                    // Switch frame style.
                    if (frameRenderer.enabled) // If we're in picture frame mode, switch to poster mode.
                    {
                        // Disable picture frame
                        frameRenderer.enabled = false;
                        buttonRenderer.enabled = false;
                        // Enable poster magnet
                        magnetRenderer.enabled = true;
                        bgRenderer.enabled = false;
                        // Enable bg bis
                        bgBisRenderer.enabled = !cpfController.Flipped;
                        bgPivotRenderer.enabled = cpfController.Flipped;
                        // Adjust magnet pos.
                        if (cpfController.Flipped)
                            magnetModel.transform.localPosition = new Vector3(0f, -0.0115f, 0f);
                        else
                            magnetModel.transform.localPosition = new Vector3(0f, 0f, 0f);
                        // Adjust image pos.
                        __instance.imageRenderer.transform.localPosition = new Vector3(__instance.imageRenderer.transform.localPosition.x, __instance.imageRenderer.transform.localPosition.y, __instance.imageRenderer.transform.localPosition.z - 0.0045f);
                    }
                    else if (magnetRenderer.enabled) // Else if we're in poster mode, switch to plain image mode.
                    {
                        // Disable picture frame
                        frameRenderer.enabled = false;
                        buttonRenderer.enabled = false;
                        // Disable poster magnet and poster bg
                        magnetRenderer.enabled = false;
                        bgRenderer.enabled = false;
                        // Enable bg bis
                        bgBisRenderer.enabled = !cpfController.Flipped;
                        bgPivotRenderer.enabled = cpfController.Flipped;
                    }
                    else // Else if we're if plain image mode, switch to picture frame mode.
                    {
                        // Enable picture frame
                        frameRenderer.enabled = true;
                        buttonRenderer.enabled = true;
                        // Disable poster magnet and poster bg
                        magnetRenderer.enabled = false;
                        bgRenderer.enabled = false;
                        // Disable bg bis
                        bgBisRenderer.enabled = false;
                        bgPivotRenderer.enabled = false;
                        // Adjust image pos
                        __instance.imageRenderer.transform.localPosition = new Vector3(__instance.imageRenderer.transform.localPosition.x, __instance.imageRenderer.transform.localPosition.y, __instance.imageRenderer.transform.localPosition.z + 0.0045f);
                    }

                    return false;
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    // CustomPictureFrame scale ratio step
                    float scaleRatio = 1.1f;

                    if (frame.transform.localScale.x >= 3.0f)
                    {
                        // Set minimum size
                        frame.transform.localScale = new Vector3(cpfController.OriginFrameScale.x * (1.0f / minSizeRatio), cpfController.OriginFrameScale.y * (1.0f / minSizeRatio), cpfController.OriginFrameScale.z);
                        poster.transform.localPosition = new Vector3(cpfController.OriginPosterPosition.x * (1.0f / minSizeRatio), cpfController.OriginPosterPosition.y * (1.0f / minSizeRatio), cpfController.OriginPosterPosition.z);
                        posterModel.transform.localPosition = new Vector3(cpfController.OriginPosterModelPosition.x * (1.0f / minSizeRatio), cpfController.OriginPosterModelPosition.y * (1.0f / minSizeRatio), cpfController.OriginPosterModelPosition.z);
                        posterModel.transform.localScale = new Vector3(cpfController.OriginPosterModelScale.x * (1.0f / minSizeRatio), cpfController.OriginPosterModelScale.y * (1.0f / minSizeRatio), cpfController.OriginPosterModelScale.z);
                        __instance.imageRenderer.transform.localScale = new Vector3(cpfController.OriginImageScale.x * (1.0f / minSizeRatio), cpfController.OriginImageScale.y * (1.0f / minSizeRatio), cpfController.OriginImageScale.z * (1.0f / minSizeRatio));
                        magnetModel.transform.localScale = new Vector3(cpfController.OriginMagnetScale.x, (cpfController.OriginMagnetScale.y * (1.0f / minSizeRatio)) + 0.1f, cpfController.OriginMagnetScale.z);
                        frame.transform.localPosition = new Vector3(cpfController.OriginFramePosition.x, cpfController.OriginFramePosition.y, cpfController.OriginFramePosition.z + 0.0001f);

                        if (cpfController.Flipped)
                        {
#if DEBUG_CUSTOM_PICTURE_FRAME
                            Logger.Log("DEBUG: ENTERING FLIPPED");
                            Logger.Log("DEBUG: BG BIS IS " + (bgBisRenderer.enabled ? "ENABLED" : "DISABLED"));
                            Logger.Log("DEBUG: BG PIVOT IS " + (bgPivotRenderer.enabled ? "ENABLED" : "DISABLED"));
#endif
                            // Rotate image
                            __instance.imageRenderer.transform.localScale = new Vector3(__instance.imageRenderer.transform.localScale.y, __instance.imageRenderer.transform.localScale.x, __instance.imageRenderer.transform.localScale.z);

                        }
#if DEBUG_CUSTOM_PICTURE_FRAME
                        else
                        {
                            Logger.Log("DEBUG: ENTERING NOT FLIPPED");
                            Logger.Log("DEBUG: BG BIS IS " + (bgBisRenderer.enabled ? "ENABLED" : "DISABLED"));
                            Logger.Log("DEBUG: BG PIVOT IS " + (bgPivotRenderer.enabled ? "ENABLED" : "DISABLED"));
                        }
#endif
                    }
                    else
                    {
                        // Increase size
                        frame.transform.localScale = new Vector3(frame.transform.localScale.x * scaleRatio, frame.transform.localScale.y * scaleRatio, frame.transform.localScale.z); // *= scaleRatio;
                        poster.transform.localPosition = new Vector3(poster.transform.localPosition.x, poster.transform.localPosition.y * scaleRatio, poster.transform.localPosition.z);
                        posterModel.transform.localPosition = new Vector3(posterModel.transform.localPosition.x, posterModel.transform.localPosition.y * scaleRatio, posterModel.transform.localPosition.z);
                        posterModel.transform.localScale = new Vector3(posterModel.transform.localScale.x * scaleRatio, posterModel.transform.localScale.y * scaleRatio, posterModel.transform.localScale.z); // *= scaleRatio;
                        __instance.imageRenderer.transform.localScale = new Vector3(__instance.imageRenderer.transform.localScale.x * scaleRatio, __instance.imageRenderer.transform.localScale.y * scaleRatio, __instance.imageRenderer.transform.localScale.z * scaleRatio); //*= scaleRatio;
                        magnetModel.transform.localScale = new Vector3(magnetModel.transform.localScale.x, magnetModel.transform.localScale.y * scaleRatio, magnetModel.transform.localScale.z);
                        frame.transform.localPosition = new Vector3(frame.transform.localPosition.x, frame.transform.localPosition.y, frame.transform.localPosition.z * 1.0f);
                    }

                    // Refresh picture
                    PictureFrameEnumHelper.SetStateMethod.Invoke(__instance, new object[] { PictureFrameEnumHelper.ThumbnailEnumValue });
                    PictureFrameEnumHelper.SetStateMethod.Invoke(__instance, new object[] { PictureFrameEnumHelper.FullEnumValue });

                    return false;
                }
            }
            return true;
        }
    }
    
    public class CustomPictureFrameController : MonoBehaviour, IProtoEventListener
    {
        public Vector3 OriginFrameScale = Vector3.zero;
        public Vector3 OriginFramePosition = Vector3.zero;
        public Vector3 OriginFrameEulerAngles = Vector3.zero;
        public Vector3 OriginImageScale = Vector3.zero;
        public Vector3 OriginColliderSize = Vector3.zero;
        public Vector3 OriginConstructableBoundsExtents = Vector3.zero;
        public Vector3 OriginPosterPosition = Vector3.zero;
        public Vector3 OriginPosterModelPosition = Vector3.zero;
        public Vector3 OriginPosterModelScale = Vector3.zero;
        public Vector3 OriginMagnetScale = Vector3.zero;

        public bool Flipped = false;
        
        public void MySetState()
        {
            PictureFrameEnumHelper.SetStateMethod.Invoke(this.gameObject.GetComponent<PictureFrame>(), new object[] { PictureFrameEnumHelper.FullEnumValue });
        }

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
                GameObject frame = this.gameObject.FindChild("mesh");
                PictureFrame pf = this.gameObject.GetComponent<PictureFrame>();

                string tmpSize = File.ReadAllText(filePath).Replace(',', '.'); // Replace , with . for backward compatibility.
                string[] sizes = tmpSize.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (sizes.Length >= 10 || sizes.Length <= 12)
                {
                    // Restore frame angles
                    string[] eulerAngles = sizes[0].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (eulerAngles.Length == 3)
                    {
                        Vector3 savedEulerAngles = new Vector3(OriginFrameEulerAngles.x, OriginFrameEulerAngles.y, OriginFrameEulerAngles.z);
                        float.TryParse(eulerAngles[0], NumberStyles.Float, CultureInfo.InvariantCulture, out savedEulerAngles.x);
                        float.TryParse(eulerAngles[1], NumberStyles.Float, CultureInfo.InvariantCulture, out savedEulerAngles.y);
                        float.TryParse(eulerAngles[2], NumberStyles.Float, CultureInfo.InvariantCulture, out savedEulerAngles.z);
                        frame.transform.localEulerAngles = savedEulerAngles;
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
                        Vector3 savedImageRendererScale = new Vector3(OriginImageScale.x, OriginImageScale.y, OriginImageScale.z);
                        float.TryParse(imageRendererScale[0], NumberStyles.Float, CultureInfo.InvariantCulture, out savedImageRendererScale.x);
                        float.TryParse(imageRendererScale[1], NumberStyles.Float, CultureInfo.InvariantCulture, out savedImageRendererScale.y);
                        float.TryParse(imageRendererScale[2], NumberStyles.Float, CultureInfo.InvariantCulture, out savedImageRendererScale.z);
                        pf.imageRenderer.transform.localScale = savedImageRendererScale;
                    }

                    // Restore frame border visibility
                    GameObject pictureFrame = frame.FindChild("submarine_Picture_Frame");
                    MeshRenderer frameRenderer = pictureFrame.GetComponent<MeshRenderer>();
                    frameRenderer.enabled = ((string.Compare(sizes[3], "1", false, CultureInfo.InvariantCulture) == 0) ? true : false);
                    GameObject frameButton = pictureFrame.FindChild("submarine_Picture_Frame_button");
                    MeshRenderer buttonRenderer = frameButton.GetComponent<MeshRenderer>();
                    buttonRenderer.enabled = ((string.Compare(sizes[3], "1", false, CultureInfo.InvariantCulture) == 0) ? true : false);
                    GameObject poster = this.gameObject.FindChild("poster_decorations(Clone)");
                    GameObject posterModel = poster.FindChild("model");
                    GameObject magnetModel = posterModel.FindChild("poster_kitty");

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
                        Vector3 updateModelScale = new Vector3(OriginFrameScale.x, OriginFrameScale.y, OriginFrameScale.z);
                        float.TryParse(modelScale[0], NumberStyles.Float, CultureInfo.InvariantCulture, out updateModelScale.x);
                        float.TryParse(modelScale[1], NumberStyles.Float, CultureInfo.InvariantCulture, out updateModelScale.y);
                        float.TryParse(modelScale[2], NumberStyles.Float, CultureInfo.InvariantCulture, out updateModelScale.z);
                        frame.transform.localScale = updateModelScale;
                    }
                    string[] posterMagnetPosition = sizes[6].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (posterMagnetPosition.Length == 3)
                    {
                        Vector3 updatePosterMagnetPosition = new Vector3(OriginPosterPosition.x, OriginPosterPosition.y, OriginPosterPosition.z);
                        float.TryParse(posterMagnetPosition[0], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterMagnetPosition.x);
                        float.TryParse(posterMagnetPosition[1], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterMagnetPosition.y);
                        float.TryParse(posterMagnetPosition[2], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterMagnetPosition.z);
                        poster.transform.localPosition = updatePosterMagnetPosition;
                    }
                    string[] posterModelPosition = sizes[7].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (posterModelPosition.Length == 3)
                    {
                        Vector3 updatePosterModelPosition = new Vector3(OriginPosterModelPosition.x, OriginPosterModelPosition.y, OriginPosterModelPosition.z);
                        float.TryParse(posterModelPosition[0], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterModelPosition.x);
                        float.TryParse(posterModelPosition[1], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterModelPosition.y);
                        float.TryParse(posterModelPosition[2], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterModelPosition.z);
                        posterModel.transform.localPosition = updatePosterModelPosition;
                    }
                    string[] posterModelScale = sizes[8].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (posterModelScale.Length == 3)
                    {
                        Vector3 updatePosterModelScale = new Vector3(OriginPosterModelScale.x, OriginPosterModelScale.y, OriginPosterModelScale.z);
                        float.TryParse(posterModelScale[0], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterModelScale.x);
                        float.TryParse(posterModelScale[1], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterModelScale.y);
                        float.TryParse(posterModelScale[2], NumberStyles.Float, CultureInfo.InvariantCulture, out updatePosterModelScale.z);
                        posterModel.transform.localScale = updatePosterModelScale;
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
                    // Restore magnet scale
                    if (sizes.Length >= 11)
                    {
                        string[] posterMagnetScale = sizes[10].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (posterMagnetScale.Length == 3)
                        {
                            Vector3 updateMagnetScale = new Vector3(OriginMagnetScale.x, OriginMagnetScale.y, OriginMagnetScale.z);
                            float.TryParse(posterMagnetScale[0], NumberStyles.Float, CultureInfo.InvariantCulture, out updateMagnetScale.x);
                            float.TryParse(posterMagnetScale[1], NumberStyles.Float, CultureInfo.InvariantCulture, out updateMagnetScale.y);
                            float.TryParse(posterMagnetScale[2], NumberStyles.Float, CultureInfo.InvariantCulture, out updateMagnetScale.z);
                            magnetModel.transform.localScale = updateMagnetScale;
                        }
                    }
                    // Restore frame position
                    if (sizes.Length >= 12)
                    {
                        string[] framePosition = sizes[11].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (framePosition.Length == 3)
                        {
                            Vector3 updateFramePosition = new Vector3(OriginFramePosition.x, OriginFramePosition.y, OriginFramePosition.z + 0.0001f);
                            float.TryParse(framePosition[0], NumberStyles.Float, CultureInfo.InvariantCulture, out updateFramePosition.x);
                            float.TryParse(framePosition[1], NumberStyles.Float, CultureInfo.InvariantCulture, out updateFramePosition.y);
                            float.TryParse(framePosition[2], NumberStyles.Float, CultureInfo.InvariantCulture, out updateFramePosition.z);
                            frame.transform.localPosition = updateFramePosition;
                        }
                    }

                    // Restore flip toogle
                    this.Flipped = (pf.imageRenderer.transform.localScale.x > pf.imageRenderer.transform.localScale.y);

                    GameObject bgBisModel = posterModel.FindChild("poster_background_bis");
                    MeshRenderer bgBisRenderer = bgBisModel?.GetComponent<MeshRenderer>();
                    GameObject bgPivotModel = posterModel.FindChild("poster_background_pivot");
                    MeshRenderer bgPivotRenderer = bgPivotModel?.GetComponent<MeshRenderer>();

                    // Rotate poster background if needed
                    if (this.Flipped)
                    {
                        bgPivotRenderer.enabled = !(string.Compare(sizes[3], "1", false, CultureInfo.InvariantCulture) == 0);
                        bgBisRenderer.enabled = false;
                    }
                    else
                    {
                        bgPivotRenderer.enabled = false;
                        bgBisRenderer.enabled = !(string.Compare(sizes[3], "1", false, CultureInfo.InvariantCulture) == 0);
                    }

                    MeshRenderer magnetRenderer = magnetModel.GetComponent<MeshRenderer>();

                    // Adjust magnet position
                    if (bgPivotRenderer.enabled)
                        magnetModel.transform.localPosition = new Vector3(0f, -0.0115f, 0f);
                    else
                        magnetModel.transform.localPosition = Vector3.zero;

                    // Restore magnet visibility
                    magnetRenderer.enabled = (string.Compare(sizes[3], "2", false, CultureInfo.InvariantCulture) == 0);

                    // Refresh picture
                    PictureFrameEnumHelper.SetStateMethod.Invoke(pf, new object[] { PictureFrameEnumHelper.ThumbnailEnumValue });
                    this.Invoke("MySetState", 2f);
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
            GameObject frame = this.gameObject.FindChild("mesh");
            string saveData = Convert.ToString(frame.transform.localEulerAngles.x, CultureInfo.InvariantCulture) + "|" +
                              Convert.ToString(frame.transform.localEulerAngles.y, CultureInfo.InvariantCulture) + "|" +
                              Convert.ToString(frame.transform.localEulerAngles.z, CultureInfo.InvariantCulture) + Environment.NewLine;

            // Save collider size
            GameObject trigger = this.gameObject.FindChild("Trigger");
            BoxCollider collider = trigger.GetComponent<BoxCollider>();
            saveData += Convert.ToString(collider.size.x, CultureInfo.InvariantCulture) + "|" +
                        Convert.ToString(collider.size.y, CultureInfo.InvariantCulture) + "|" +
                        Convert.ToString(collider.size.z, CultureInfo.InvariantCulture) + Environment.NewLine;

            // Save picture scale
            PictureFrame pf = this.gameObject.GetComponent<PictureFrame>();
            saveData += Convert.ToString(pf.imageRenderer.transform.localScale.x, CultureInfo.InvariantCulture) + "|" +
                        Convert.ToString(pf.imageRenderer.transform.localScale.y, CultureInfo.InvariantCulture) + "|" +
                        Convert.ToString(pf.imageRenderer.transform.localScale.z, CultureInfo.InvariantCulture) + Environment.NewLine;

            // Save frame border visibility
            GameObject pictureFrame = frame.FindChild("submarine_Picture_Frame");
            GameObject poster = this.gameObject.FindChild("poster_decorations(Clone)");
            GameObject posterModel = poster.FindChild("model");
            GameObject magnetModel = posterModel.FindChild("poster_kitty");
            MeshRenderer frameRenderer = pictureFrame.GetComponent<MeshRenderer>();
            if (frameRenderer.enabled)
                saveData += "1" + Environment.NewLine;
            else
            {
                MeshRenderer magnetRenderer = magnetModel.GetComponent<MeshRenderer>();
                if (magnetRenderer.enabled)
                    saveData += "2" + Environment.NewLine;
                else
                    saveData += "0" + Environment.NewLine;
            }

            // Save constructable bounds extents
            ConstructableBounds constructableBounds = this.gameObject.GetComponent<ConstructableBounds>();
            saveData += Convert.ToString(constructableBounds.bounds.extents.x, CultureInfo.InvariantCulture) + "|" +
                        Convert.ToString(constructableBounds.bounds.extents.y, CultureInfo.InvariantCulture) + "|" +
                        Convert.ToString(constructableBounds.bounds.extents.z, CultureInfo.InvariantCulture) + Environment.NewLine;

            // Save current sizes
            saveData += Convert.ToString(frame.transform.localScale.x, CultureInfo.InvariantCulture) + "|" + Convert.ToString(frame.transform.localScale.y, CultureInfo.InvariantCulture) + "|" + Convert.ToString(frame.transform.localScale.z, CultureInfo.InvariantCulture) + Environment.NewLine;
            saveData += Convert.ToString(poster.transform.localPosition.x, CultureInfo.InvariantCulture) + "|" + Convert.ToString(poster.transform.localPosition.y, CultureInfo.InvariantCulture) + "|" + Convert.ToString(poster.transform.localPosition.z, CultureInfo.InvariantCulture) + Environment.NewLine;
            saveData += Convert.ToString(posterModel.transform.localPosition.x, CultureInfo.InvariantCulture) + "|" + Convert.ToString(posterModel.transform.localPosition.y, CultureInfo.InvariantCulture) + "|" + Convert.ToString(posterModel.transform.localPosition.z, CultureInfo.InvariantCulture) + Environment.NewLine;
            saveData += Convert.ToString(posterModel.transform.localScale.x, CultureInfo.InvariantCulture) + "|" + Convert.ToString(posterModel.transform.localScale.y, CultureInfo.InvariantCulture) + "|" + Convert.ToString(posterModel.transform.localScale.z, CultureInfo.InvariantCulture) + Environment.NewLine;
            saveData += Convert.ToString(pf.imageRenderer.transform.localPosition.x, CultureInfo.InvariantCulture) + "|" + Convert.ToString(pf.imageRenderer.transform.localPosition.y, CultureInfo.InvariantCulture) + "|" + Convert.ToString(pf.imageRenderer.transform.localPosition.z, CultureInfo.InvariantCulture) + Environment.NewLine;
            saveData += Convert.ToString(magnetModel.transform.localScale.x, CultureInfo.InvariantCulture) + "|" + Convert.ToString(magnetModel.transform.localScale.y, CultureInfo.InvariantCulture) + "|" + Convert.ToString(magnetModel.transform.localScale.z, CultureInfo.InvariantCulture) + Environment.NewLine;
            saveData += Convert.ToString(frame.transform.localPosition.x, CultureInfo.InvariantCulture) + "|" + Convert.ToString(frame.transform.localPosition.y, CultureInfo.InvariantCulture) + "|" + Convert.ToString(frame.transform.localPosition.z, CultureInfo.InvariantCulture) + Environment.NewLine;

            // Save state to file
            File.WriteAllText(Path.Combine(saveFolder, "custompictureframe_" + id.Id + ".txt"), saveData);
        }
    }
}
