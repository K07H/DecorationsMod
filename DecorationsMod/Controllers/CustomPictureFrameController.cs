using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
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
        public bool RandomImage = false;
        public bool Slideshow = false;
        public List<string> SlideshowImages = new List<string>();

        public float lastSlideshowChange = Time.time;
        public float slideshowDelay = 15.0f; // Default slideshow delay (in seconds).
        public int slideshowIndex = 0;
        public int slideshowLastIndex = 0;

        private PictureFrame _pf = null;
        private readonly System.Random _r = new System.Random();

        public void MySetState()
        {
            PictureFrameEnumHelper.SetStateMethod.Invoke(this.gameObject.GetComponent<PictureFrame>(), new object[] { PictureFrameEnumHelper.FullEnumValue });
        }

        private string GetRandomImageFromFolder(string folder)
        {
            string[] files = Directory.GetFiles(folder);
            if (files != null)
            {
                List<string> images = new List<string>();
                foreach (string file in files)
                    if (Path.GetExtension(file) == ".jpg" || Path.GetExtension(file) == ".jpeg" || Path.GetExtension(file) == ".png" || Path.GetExtension(file) == ".gif")
                        images.Add(file);
                if (images.Count > 0)
                    return images.ElementAt(_r.Next(0, images.Count)).Replace('\\', '/');
            }
            return null;
        }

        private void InitRandomMode(string folderPath)
        {
            this._pf.fileName = GetRandomImageFromFolder(folderPath);
            if (this._pf.fileName != null)
                PictureFrameEnumHelper.SetStateMethod.Invoke(this._pf, new object[] { PictureFrameEnumHelper.FullEnumValue });
            else
                Logger.Log("INFO: Could not find a valid image inside custom picture frame images folder at \"" + folderPath + "\".");
        }

        private void InitSlideshowMode(string folderPath)
        {
            this.slideshowIndex = 0;
            string[] files;
            try { files = Directory.GetFiles(folderPath); }
            catch { files = null; }
            if (files != null)
            {
                this.SlideshowImages.Clear();
                foreach (string file in files)
                    if (Path.GetExtension(file) == ".jpg" || Path.GetExtension(file) == ".jpeg" || Path.GetExtension(file) == ".png" || Path.GetExtension(file) == ".gif")
                        this.SlideshowImages.Add(file);
                if (this.SlideshowImages.Count > 0)
                {
                    this.SlideshowImages.Sort();
                    if (this.RandomImage)
                    {
                        slideshowLastIndex = _r.Next(0, this.SlideshowImages.Count);
                        this._pf.fileName = this.SlideshowImages.ElementAt(slideshowLastIndex).Replace('\\', '/');
                    }
                    else
                        this._pf.fileName = this.SlideshowImages.First().Replace('\\', '/');
                    PictureFrameEnumHelper.SetStateMethod.Invoke(this._pf, new object[] { PictureFrameEnumHelper.FullEnumValue });
                }
                else
                    Logger.Log("INFO: Could not find any image inside custom picture frame images folder at \"" + folderPath + "\".");
            }
            else
                Logger.Log("INFO: Could not find any files inside custom picture frame images folder at \"" + folderPath + "\".");
            this.lastSlideshowChange = Time.time;
            this.Slideshow = true;
        }

        private void InitImagesFolder(PrefabIdentifier id, PictureFrame pf, bool initRandom = false, bool initSlideshow = false)
        {
            this._pf = pf;
            string rootFolderPath = Path.GetFullPath(Path.Combine(FilesHelper.GetSaveFolderPath(), "pictureframes")).Replace('\\', '/');
            if (!Directory.Exists(rootFolderPath))
            {
                try { Directory.CreateDirectory(rootFolderPath); }
                catch { Logger.Log("WARNING: Could not create directory for custom picture frames at \"" + rootFolderPath + "\"."); }
            }
            if (Directory.Exists(rootFolderPath))
            {
                string folderPath = rootFolderPath + "/" + id.Id;
                string configFilePath = folderPath + "/Config.txt";
                if (!Directory.Exists(folderPath))
                {
                    try { Directory.CreateDirectory(folderPath); }
                    catch { Logger.Log("WARNING: Could not create images directory for custom picture frame at \"" + folderPath + "\"."); }
                    if (Directory.Exists(folderPath))
                    {
                        try
                        {
                            Vector3 pos = pf.gameObject.transform.position;
                            string fileContent = string.Format(CultureInfo.InvariantCulture, "# Exact coordinates: {0} {1} {2}{3}# Rounded coordinates: {4} {5} {6}{3}# Value below defines the number of seconds to wait before switching to next image when slideshow mode is enabled.{3}Delay={7}",
                                pos.x.ToString(".0##", CultureInfo.InvariantCulture),
                                pos.y.ToString(".0##", CultureInfo.InvariantCulture),
                                pos.z.ToString(".0##", CultureInfo.InvariantCulture),
                                Environment.NewLine,
                                ((int)Math.Round(pos.x, 0)).ToString(),
                                ((int)Math.Round(pos.y, 0)).ToString(),
                                ((int)Math.Round(pos.z, 0)).ToString(),
                                ((int)Math.Round(this.slideshowDelay, 0)).ToString());
                            File.WriteAllText(configFilePath, fileContent, System.Text.Encoding.UTF8);
                        }
                        catch { Logger.Log("WARNING: Could not add config file into custom picture frame images folder."); }
                        try { Process.Start(folderPath); }
                        catch { Logger.Log("WARNING: Could not open custom picture frame images folder at \"" + folderPath + "\"."); }
                    }
                }
                if (Directory.Exists(folderPath))
                {
                    if (File.Exists(configFilePath))
                    {
                        string[] lines = File.ReadAllLines(configFilePath, System.Text.Encoding.UTF8);
                        if (lines != null)
                            foreach (string line in lines)
                                if (line.StartsWith("Delay="))
                                {
                                    if (line.Length > 6 && int.TryParse(line.Substring(6), NumberStyles.Integer, CultureInfo.InvariantCulture, out int delay) && delay > 0)
                                        this.slideshowDelay = delay;
                                    else
                                        Logger.Log("WARNING: The line \"" + line + "\" does not have a correct value (must be an integer greater than 0). Default value will be used.");
                                    break;
                                }
                    }
                    if (initRandom) // Initialize random mode
                        InitRandomMode(folderPath);
                    if (initSlideshow) // Initialize slideshow mode
                        InitSlideshowMode(folderPath);
                }
                else
                    Logger.Log("WARNING: Could not find custom picture frame images folder at \"" + folderPath + "\".");
            }
        }

        public void SetNextSlideshowImage()
        {
            PictureFrameEnumHelper.SetStateMethod.Invoke(this._pf, new object[] { PictureFrameEnumHelper.NoneEnumValue });
            if (this.SlideshowImages.Count <= 0)
            {
                this.slideshowIndex = 0;
                this._pf.fileName = null;
            }
            else if (this.SlideshowImages.Count == 1)
            {
                this.slideshowIndex = 0;
                this._pf.fileName = this.SlideshowImages.First().Replace('\\', '/');
                PictureFrameEnumHelper.SetStateMethod.Invoke(this._pf, new object[] { PictureFrameEnumHelper.FullEnumValue });
            }
            else if (this.SlideshowImages.Count > 1)
            {
                if (this.RandomImage)
                {
                    int rnd = this.slideshowLastIndex;
                    while (rnd == this.slideshowLastIndex && this.SlideshowImages.Count > 1)
                        rnd = _r.Next(0, this.SlideshowImages.Count);
                    this.slideshowLastIndex = rnd;
                    if (this.slideshowLastIndex < this.SlideshowImages.Count)
                        this._pf.fileName = this.SlideshowImages.ElementAt(this.slideshowLastIndex).Replace('\\', '/');
                }
                else
                {
                    this.slideshowIndex++;
                    if (this.slideshowIndex >= this.SlideshowImages.Count)
                        this.slideshowIndex = 0;
                    if (this.slideshowIndex < this.SlideshowImages.Count)
                        this._pf.fileName = this.SlideshowImages.ElementAt(this.slideshowIndex).Replace('\\', '/');
                }
                PictureFrameEnumHelper.SetStateMethod.Invoke(this._pf, new object[] { PictureFrameEnumHelper.FullEnumValue });
            }
            this.lastSlideshowChange = Time.time;
        }

        public void SetRandomImage(PrefabIdentifier id, PictureFrame pf) => InitImagesFolder(id, pf, true, false);

        public void StartSlideshow(PrefabIdentifier id, PictureFrame pf) => InitImagesFolder(id, pf, false, true);

        private void Update()
        {
            if (Slideshow && Time.time >= (lastSlideshowChange + slideshowDelay))
                if (enabled && _pf != null)
                    SetNextSlideshowImage();
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
                if (tmpSize == null)
                    return;
                string[] sizes = tmpSize.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (sizes != null && sizes.Length >= 10 && sizes.Length <= 14)
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
                    // Restore random image mode
                    if (sizes.Length >= 13)
                    {
                        this.RandomImage = (sizes[12] == "1");
                        if (this.RandomImage)
                        {
                            PictureFrameEnumHelper.SetStateMethod.Invoke(pf, new object[] { PictureFrameEnumHelper.NoneEnumValue });
                            pf.fileName = null;
                            this.SetRandomImage(id, pf);
                        }
                    }
                    // Restore slideshow mode
                    if (sizes.Length >= 14)
                    {
                        bool isSlideshowOn = (sizes[13] == "1");
                        if (!isSlideshowOn)
                            this.Slideshow = false;
                        else
                        {
                            PictureFrameEnumHelper.SetStateMethod.Invoke(pf, new object[] { PictureFrameEnumHelper.NoneEnumValue });
                            pf.fileName = null;
                            this.StartSlideshow(id, pf);
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

#if DEBUG_CUSTOM_PICTURE_FRAME
                    Logger.Log("DEBUG: Current image in picture frame: fileName=[" + (string.IsNullOrEmpty(pf.fileName) ? "?" : pf.fileName) + "]");
#endif
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

            // Save random image mode
            saveData += (this.RandomImage ? "1" : "0") + Environment.NewLine;

            // Save slideshow mode
            saveData += (this.Slideshow ? "1" : "0") + Environment.NewLine;

            // Save state to file
            File.WriteAllText(Path.Combine(saveFolder, "custompictureframe_" + id.Id + ".txt"), saveData);
        }
    }
}
