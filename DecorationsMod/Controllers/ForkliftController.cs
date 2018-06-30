using System;
using System.Globalization;
using System.IO;
using UnityEngine;
using UWE;

namespace DecorationsMod.Controllers
{
    public class ForkliftController : HandTarget, IHandTarget, IProtoEventListener
    {
        public void OnHandClick(GUIHand hand)
        {
            if (!enabled)
                return;

            GameObject model = this.gameObject.FindChild("forklift");
            if (model == null)
                return;
            
            BoxCollider collider = this.gameObject.GetComponent<BoxCollider>();
            if (model.transform.localScale.y > 35.0f)
            {
                model.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
                collider.size = new Vector3(0.2f, 0.2f, 0.2f);
            }
            else
            {
                model.transform.localScale *= 1.25f;
                collider.size *= 1.25f;
            }
        }

        public void OnHandHover(GUIHand hand)
        {
            if (!enabled)
                return;
            
            var reticle = HandReticle.main;
            reticle.SetIcon(HandReticle.IconType.Hand, 1f);
            reticle.SetInteractText("AdjustForkliftSize");
        }
        
        public void OnProtoSerialize(ProtobufSerializer serializer)
        {
            string saveFolder = FilesHelper.GetSaveFolderPath();
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            GameObject model = this.gameObject.FindChild("forklift");
            string size = Convert.ToString(model.transform.localScale.y);
            BoxCollider collider = this.gameObject.GetComponent<BoxCollider>();
            size += Environment.NewLine + Convert.ToString(collider.size.y);

            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            File.WriteAllText(Path.Combine(saveFolder, "forklift_" + id.Id + ".txt"), size);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            string filePath = Path.Combine(FilesHelper.GetSaveFolderPath(), "forklift_" + id.Id + ".txt");
            if (File.Exists(filePath))
            {
                string tmpSize = File.ReadAllText(filePath);
                string[] sizes = tmpSize.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (sizes.Length == 2)
                {
                    GameObject model = this.gameObject.FindChild("forklift");
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
