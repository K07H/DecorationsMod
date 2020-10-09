using System;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;

namespace DecorationsMod.Controllers
{
    public class WarperSpecimenController : HandTarget, IHandTarget, IProtoEventListener
    {
        private const float ScaleRatio = 1.1f;

        public void OnHandClick(GUIHand hand)
        {
            if (!enabled)
                return;

            if (Input.GetKey(KeyCode.E))
            {
                GameObject model = this.gameObject.FindChild("Model");
                if (model == null)
                    return;
                BoxCollider collider = this.gameObject.GetComponent<BoxCollider>();
                if (collider == null)
                    return;

                if (model.transform.localScale.y > 2.0f)
                {
#if DEBUG_WARPER_SPECIMEN
                    Logger.Log("DEBUG: Resetting WarperSpecimen size to [0.5;0.5;0.5] and collider to [0.004;0.004;0.019]");
#endif
                    model.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    collider.size = new Vector3(0.004f, 0.004f, 0.019f);
                }
                else
                {
#if DEBUG_WARPER_SPECIMEN
                    Logger.Log("DEBUG: Increasing WarperSpecimen size from [{0};{1};{2}].", model.transform.localScale.x.ToString("0.000", CultureInfo.InvariantCulture), model.transform.localScale.y.ToString("0.000", CultureInfo.InvariantCulture), model.transform.localScale.z.ToString("0.000", CultureInfo.InvariantCulture));
#endif
                    model.transform.localScale *= ScaleRatio;
#if DEBUG_WARPER_SPECIMEN
                    Logger.Log("DEBUG: Increasing WarperSpecimen size to [{0};{1};{2}].", model.transform.localScale.x.ToString("0.000", CultureInfo.InvariantCulture), model.transform.localScale.y.ToString("0.000", CultureInfo.InvariantCulture), model.transform.localScale.z.ToString("0.000", CultureInfo.InvariantCulture));
                    Logger.Log("DEBUG: Increasing WarperSpecimen collider from [{0};{1};{2}].", collider.size.x.ToString("0.000", CultureInfo.InvariantCulture), collider.size.y.ToString("0.000", CultureInfo.InvariantCulture), collider.size.z.ToString("0.000", CultureInfo.InvariantCulture));
#endif
                    collider.size *= ScaleRatio;
#if DEBUG_WARPER_SPECIMEN
                    Logger.Log("DEBUG: Increasing WarperSpecimen size from [{0};{1};{2}].", collider.size.x.ToString("0.000", CultureInfo.InvariantCulture), collider.size.y.ToString("0.000", CultureInfo.InvariantCulture), collider.size.z.ToString("0.000", CultureInfo.InvariantCulture));
#endif
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
            reticle.SetTextRaw(HandReticle.TextType.Hand, LanguageHelper.GetFriendlyWord("AdjustWarperSpecimenSize"));
#else
            reticle.SetInteractText("AdjustWarperSpecimenSize");
#endif
        }

        public void OnProtoSerialize(ProtobufSerializer serializer)
        {
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            if (id == null)
                if ((id = GetComponent<PrefabIdentifier>()) == null)
                    return;

            if (this.gameObject != null)
            {
                string saveFolder = FilesHelper.GetSaveFolderPath();
                if (!Directory.Exists(saveFolder))
                    Directory.CreateDirectory(saveFolder);

                GameObject model = this.gameObject.FindChild("Model");
                BoxCollider collider = this.gameObject.GetComponent<BoxCollider>();
                if (model != null && collider != null)
                {
                    string size = Convert.ToString(model.transform.localScale.x, CultureInfo.InvariantCulture) + "/" +
                        Convert.ToString(model.transform.localScale.y, CultureInfo.InvariantCulture) + "/" +
                        Convert.ToString(model.transform.localScale.z, CultureInfo.InvariantCulture);

                    size += Environment.NewLine +
                        Convert.ToString(collider.size.x, CultureInfo.InvariantCulture) + "/" +
                        Convert.ToString(collider.size.y, CultureInfo.InvariantCulture) + "/" +
                        Convert.ToString(collider.size.z, CultureInfo.InvariantCulture);

                    File.WriteAllText(FilesHelper.Combine(saveFolder, "warperspecimen_" + id.Id + ".txt"), size, Encoding.UTF8);
                }
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            if (id == null)
                if ((id = GetComponent<PrefabIdentifier>()) == null)
                    return;

            string filePath = FilesHelper.Combine(FilesHelper.GetSaveFolderPath(), "warperspecimen_" + id.Id + ".txt");
            if (File.Exists(filePath))
            {
                string tmpSize = File.ReadAllText(filePath, Encoding.UTF8).Replace(',', '.'); // Replace , with . for backward compatibility.
                if (tmpSize == null)
                    return;
                string[] sizes = tmpSize.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (sizes != null && sizes.Length == 2 && this.gameObject != null)
                {
                    string[] xyz;
                    GameObject model = this.gameObject.FindChild("Model");
                    if (model != null)
                    {
                        xyz = sizes[0].Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (xyz != null && xyz.Length == 3)
                        {
                            float sizeX = float.Parse(xyz[0], CultureInfo.InvariantCulture);
                            float sizeY = float.Parse(xyz[1], CultureInfo.InvariantCulture);
                            float sizeZ = float.Parse(xyz[2], CultureInfo.InvariantCulture);
                            model.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);
                        }
                    }
                    BoxCollider collider = this.gameObject.GetComponent<BoxCollider>();
                    if (collider != null)
                    {
                        xyz = sizes[1].Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (xyz != null && xyz.Length == 3)
                        {
                            float colliderSizeX = float.Parse(xyz[0], CultureInfo.InvariantCulture);
                            float colliderSizeY = float.Parse(xyz[1], CultureInfo.InvariantCulture);
                            float colliderSizeZ = float.Parse(xyz[2], CultureInfo.InvariantCulture);
                            collider.size = new Vector3(colliderSizeX, colliderSizeY, colliderSizeZ);
                        }
                    }
                }
            }
        }
    }
}

