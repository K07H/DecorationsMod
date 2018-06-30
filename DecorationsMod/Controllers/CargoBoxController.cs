using System;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace DecorationsMod.Controllers
{
    public class CargoBoxController : HandTarget, IHandTarget, IProtoEventListener
    {
        public void OnHandClick(GUIHand hand)
        {
            if (!enabled)
                return;

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
                collider.size = new Vector3(0.16f, 0.288f, 0.16f);
            }
            else
            {
                model.transform.localScale *= 1.25f;
                collider.size *= 1.248f;
            }
        }

        public void OnHandHover(GUIHand hand)
        {
            if (!enabled)
                return;

            var reticle = HandReticle.main;
            reticle.SetIcon(HandReticle.IconType.Hand, 1f);
            reticle.SetInteractText("AdjustCargoBoxSize");
        }

        public void OnProtoSerialize(ProtobufSerializer serializer)
        {
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            if (id == null)
                return;

            string saveFolder = FilesHelper.GetSaveFolderPath();
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            GameObject model = this.gameObject.FindChild("cargobox01_damaged");
            if (model == null)
                if ((model = this.gameObject.FindChild("cargobox01a")) == null)
                    model = this.gameObject.FindChild("cargobox01b");
            if (model == null)
                return;

            string size = Convert.ToString(model.transform.localScale.y);
            BoxCollider collider = this.gameObject.GetComponent<BoxCollider>();
            size += Environment.NewLine + Convert.ToString(collider.size.y);
            
            File.WriteAllText(Path.Combine(saveFolder, "cargocrate_" + id.Id + ".txt"), size);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            if (id == null)
                return;

            string filePath = Path.Combine(FilesHelper.GetSaveFolderPath(), "cargocrate_" + id.Id + ".txt");
            if (File.Exists(filePath))
            {
                string tmpSize = File.ReadAllText(filePath);
                string[] sizes = tmpSize.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (sizes.Length == 2)
                {
                    GameObject model = this.gameObject.FindChild("cargobox01_damaged");
                    if (model == null)
                        if ((model = this.gameObject.FindChild("cargobox01a")) == null)
                            model = this.gameObject.FindChild("cargobox01b");
                    if (model == null)
                        return;
                    BoxCollider collider = this.gameObject.GetComponent<BoxCollider>();
                    
                    float size = float.Parse(sizes[0], CultureInfo.InvariantCulture.NumberFormat);
                    model.transform.localScale = new Vector3(size, size, size);
                    float colliderSize = float.Parse(sizes[1], CultureInfo.InvariantCulture.NumberFormat);
                    collider.size = new Vector3(colliderSize, colliderSize, colliderSize);
                }
            }
        }
    }
}
