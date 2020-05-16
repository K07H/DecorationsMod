using DecorationsMod.Fixers;
using System;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace DecorationsMod.Controllers
{
    public class CargoBoxController : HandTarget, IHandTarget, IProtoEventListener
    {
        private StorageContainer _storageContainer = null;
        
        public override void Awake()
        {
            base.Awake();
            StorageContainer sc;
            try { sc = this.gameObject.GetComponentInChildren<StorageContainer>(); }
            catch { sc = null; }
            if (sc != null)
                this._storageContainer = sc;
        }

        public void OnHandClick(GUIHand hand)
        {
            if (!base.enabled)
                return;

            if (Input.GetKey(KeyCode.E))
            {
                GameObject model = this.gameObject.FindChild("cargobox01_damaged");
                if (model == null)
                    if ((model = this.gameObject.FindChild("cargobox01a")) == null)
                        model = this.gameObject.FindChild("cargobox01b");
                if (model == null)
                    return;

                BoxCollider collider = this.gameObject.GetComponent<BoxCollider>();
                if (model.transform.localScale.y > 50.0f)
                {
                    model.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
                    collider.size = new Vector3(0.132f, 0.288f, 0.16f);
                }
                else
                {
                    model.transform.localScale *= 1.25f;
                    collider.size *= 1.248f;
                }
            }
            else
            {
                if (this._storageContainer == null)
                {
                    StorageContainer sc;
                    try { sc = this.gameObject.transform.parent.GetComponentInChildren<StorageContainer>(); }
                    catch { sc = null; }
                    if (sc != null)
                        this._storageContainer = sc;
                }
                if (this._storageContainer != null)
                    this._storageContainer.OnHandClick(hand);
            }
        }

        public void OnHandHover(GUIHand hand)
        {
            if (!base.enabled)
                return;
            
            HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
#if BELOWZERO
            HandReticle.main.SetTextRaw(HandReticle.TextType.Hand, LanguageHelper.GetFriendlyWord("AdjustCargoBoxSize"));
#else
            HandReticle.main.SetInteractText("AdjustCargoBoxSize");
#endif
        }

        public void OnProtoSerialize(ProtobufSerializer serializer)
        {
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            if (id == null)
                if ((id = GetComponent<PrefabIdentifier>()) == null)
                    if ((id = this.gameObject.GetComponent<PrefabIdentifier>()) == null)
                        return;
#if DEBUG_CARGO_CRATES
            Logger.Log("DEBUG: Serialize(): PrefabID=[" + id.Id + "]");
#endif

            string saveFolder = FilesHelper.GetSaveFolderPath();
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            GameObject model = this.gameObject.FindChild("cargobox01_damaged");
            if (model == null)
                if ((model = this.gameObject.FindChild("cargobox01a")) == null)
                    model = this.gameObject.FindChild("cargobox01b");
            if (model == null)
                return;
            
            string saveData = Convert.ToString(model.transform.localScale.y, CultureInfo.InvariantCulture);
            BoxCollider collider = this.gameObject.GetComponent<BoxCollider>();
            saveData += Environment.NewLine + Convert.ToString(collider.size.x, CultureInfo.InvariantCulture) + "|" + Convert.ToString(collider.size.y, CultureInfo.InvariantCulture) + "|" + Convert.ToString(collider.size.z, CultureInfo.InvariantCulture);

#if DEBUG_CARGO_CRATES
            Logger.Log("DEBUG: Serialize(): Saving cargo crates nbItems=[" + _storageContainer?.container?.count + "] size=[" + Convert.ToString(model.transform.localScale.y, CultureInfo.InvariantCulture) + "] collider x=[" + Convert.ToString(collider.size.x, CultureInfo.InvariantCulture) + "] y=[" + Convert.ToString(collider.size.y, CultureInfo.InvariantCulture) + "] z=[" + Convert.ToString(collider.size.z, CultureInfo.InvariantCulture) + "].");
#endif
            File.WriteAllText(Path.Combine(saveFolder, "cargocrate_" + id.Id + ".txt"), saveData);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            if (id == null)
                if ((id = GetComponent<PrefabIdentifier>()) == null)
                    if ((id = this.gameObject.GetComponent<PrefabIdentifier>()) == null)
                        return;
#if DEBUG_CARGO_CRATES
            Logger.Log("DEBUG: Deserialize(): PrefabID=[" + id.Id + "]");
#endif

            string filePath = Path.Combine(FilesHelper.GetSaveFolderPath(), "cargocrate_" + id.Id + ".txt");

            if (File.Exists(filePath))
            {

#if DEBUG_CARGO_CRATES
                Logger.Log("DEBUG: Deserialize() A");
#endif
                string tmpSize = File.ReadAllText(filePath).Replace(',', '.'); // Replace , with . for backward compatibility.
                string[] sizes = tmpSize.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (sizes.Length == 2)
                {
#if DEBUG_CARGO_CRATES
                    Logger.Log("DEBUG: Deserialize() B");
#endif
                    GameObject model = this.gameObject.FindChild("cargobox01_damaged");
                    if (model == null)
                        if ((model = this.gameObject.FindChild("cargobox01a")) == null)
                            model = this.gameObject.FindChild("cargobox01b");
                    if (model == null)
                        return;

                    BoxCollider collider = this.gameObject.GetComponent<BoxCollider>();

#if DEBUG_CARGO_CRATES
                    Logger.Log("DEBUG: Deserialize() C");
#endif
                    float size = float.Parse(sizes[0], CultureInfo.InvariantCulture);
                    model.transform.localScale = new Vector3(size, size, size);
                    string[] colliderSizes = sizes[1].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (colliderSizes.Length == 3)
                    {
#if DEBUG_CARGO_CRATES
                        Logger.Log("DEBUG: Deserialize() D");
#endif
                        float colliderSizeX = float.Parse(colliderSizes[0], CultureInfo.InvariantCulture);
                        float colliderSizeY = float.Parse(colliderSizes[1], CultureInfo.InvariantCulture);
                        float colliderSizeZ = float.Parse(colliderSizes[2], CultureInfo.InvariantCulture);
                        collider.size = new Vector3(colliderSizeX, colliderSizeY, colliderSizeZ);
                    }
                    else // Backward compatibility
                    {
                        float colliderSize;
                        if (float.TryParse(sizes[1], NumberStyles.Float, CultureInfo.InvariantCulture, out colliderSize))
                            collider.size = new Vector3(colliderSize * 0.4583f, colliderSize, colliderSize * 0.5555f);
#if DEBUG_CARGO_CRATES
                        Logger.Log("DEBUG: Deserialize() E");
#endif
                    }
                }
            }
        }
    }
}
