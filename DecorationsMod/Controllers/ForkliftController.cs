using System;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;

namespace DecorationsMod.Controllers
{
    public class ForkliftController : HandTarget, IHandTarget, IProtoEventListener
    {
        public void OnHandClick(GUIHand hand)
        {
            if (!enabled)
                return;

            if (Input.GetKey(KeyCode.E))
            {
                GameObject model = this.gameObject.FindChild("forklift");
                if (model == null)
                    return;

                BoxCollider collider = this.gameObject.GetComponent<BoxCollider>();
                if (model.transform.localScale.y > 10.0f)
                {
                    model.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    collider.size = new Vector3(0.05f, 0.064f, 0.04f);
                }
                else
                {
                    model.transform.localScale *= 1.25f;
                    collider.size *= 1.25f;
                }
            }
        }

        public void OnHandHover(GUIHand hand)
        {
            if (!enabled)
                return;
            
            var reticle = HandReticle.main;
            reticle.SetIcon(HandReticle.IconType.Hand, 1f);
#if BELOWZERO
            reticle.SetTextRaw(HandReticle.TextType.Hand, LanguageHelper.GetFriendlyWord("AdjustForkliftSize"));
#else
            reticle.SetInteractText("AdjustForkliftSize");
#endif
        }
        
        public void OnProtoSerialize(ProtobufSerializer serializer)
        {
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            if (id == null)
                if ((id = GetComponent<PrefabIdentifier>()) == null)
                    return;

            string saveFolder = FilesHelper.GetSaveFolderPath();
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            GameObject model = this.gameObject.FindChild("forklift");
            string size = Convert.ToString(model.transform.localScale.y, CultureInfo.InvariantCulture);
            BoxCollider collider = this.gameObject.GetComponent<BoxCollider>();
            size += Environment.NewLine + Convert.ToString(collider.size.y, CultureInfo.InvariantCulture);

            File.WriteAllText(FilesHelper.Combine(saveFolder, "forklift_" + id.Id + ".txt"), size, Encoding.UTF8);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            if (id == null)
                if ((id = GetComponent<PrefabIdentifier>()) == null)
                    return;

            string filePath = FilesHelper.Combine(FilesHelper.GetSaveFolderPath(), "forklift_" + id.Id + ".txt");
            if (File.Exists(filePath))
            {
                string tmpSize = File.ReadAllText(filePath, Encoding.UTF8).Replace(',', '.'); // Replace , with . for backward compatibility.
                if (tmpSize == null)
                    return;
                string[] sizes = tmpSize.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (sizes != null && sizes.Length == 2)
                {
                    GameObject model = this.gameObject.FindChild("forklift");
                    if (model != null)
                    {
                        float size = float.Parse(sizes[0], CultureInfo.InvariantCulture);
                        model.transform.localScale = new Vector3(size, size, size);
                    }
                    BoxCollider collider = this.gameObject.GetComponent<BoxCollider>();
                    if (collider != null)
                    {
                        float colliderSize = float.Parse(sizes[1], CultureInfo.InvariantCulture);
                        collider.size = new Vector3(colliderSize, colliderSize, colliderSize);
                    }
                }
            }
        }
    }
}
