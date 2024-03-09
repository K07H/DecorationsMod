using System;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;

namespace DecorationsMod.Controllers
{
    public class CustomFloraSerializer : MonoBehaviour, IProtoEventListener
    {
        public void OnProtoSerialize(ProtobufSerializer serializer)
        {
            PrefabIdentifier id = this.gameObject.GetComponent<PrefabIdentifier>();
            if (id == null)
                if ((id = GetComponent<PrefabIdentifier>()) == null)
                    if ((id = GetComponentInParent<PrefabIdentifier>()) == null)
                        return;

#if DEBUG_FLORA
            Logger.Debug("Entering onProtoSerialize for gameobject name=[" + this.gameObject.name + "] id=[" + id.Id + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
#endif

            Plantable plant = this.gameObject.GetComponent<Plantable>();
            if (plant.linkedGrownPlant != null)
            {
                PlantGenericController plantController = plant.linkedGrownPlant.gameObject.GetComponent<PlantGenericController>();
                LandTree1Controller treeController = plant.linkedGrownPlant.gameObject.GetComponent<LandTree1Controller>();
                PlantMonoTransformController plantMonoTransformController = plant.linkedGrownPlant.GetComponent<PlantMonoTransformController>();

                float progress = -1.0f;
                if (plantController != null && plantController._progress >= 0.0f)
                {
                    progress = plantController._progress;
#if DEBUG_FLORA
                    Logger.Debug("LinkedGrownPlant) plantController) Saving progress=[" + progress + "] for gameObject name=[" + this.gameObject.name + "]");
#endif
                }
                else if (treeController != null && treeController._progress >= 0.0f)
                {
                    progress = treeController._progress;
#if DEBUG_FLORA
                    Logger.Debug("LinkedGrownPlant) treeController) Saving progress=[" + progress + "] for gameObject name=[" + this.gameObject.name + "]");
#endif
                }
                else if (plantMonoTransformController != null && plantMonoTransformController._progress >= 0.0f)
                {
                    progress = plantMonoTransformController._progress;
#if DEBUG_FLORA
                    Logger.Debug("LinkedGrownPlant) plantMonoTransformController) Saving progress=[" + progress + "] for gameObject name=[" + this.gameObject.name + "]");
                }
                else
                    Logger.Debug("LinkedGrownPlant) No controller found for gameObject name=[" + this.gameObject.name + "]");
#else
                }
#endif

                if (progress >= 0.0f)
                {
                    // Open save directory
                    string saveFolder = FilesHelper.GetSaveFolderPath();
                    if (!saveFolder.Contains("/test/"))
                    {
                        if (!Directory.Exists(saveFolder))
                            Directory.CreateDirectory(saveFolder);

                        // Save custom flora state
                        File.WriteAllText(FilesHelper.Combine(saveFolder, "customflora_" + id.Id + ".txt"), Convert.ToString(progress, CultureInfo.InvariantCulture), Encoding.UTF8);
                    }
                    else
                        Logger.Warning("Cannot save custom flora state: Save game folder path \"" + saveFolder + "\" is incorrect.");
                }
            }
            else
            {
#if DEBUG_FLORA
                Logger.Debug("Cannot find linkedGrownPlant. this.gameObject name=[" + this.gameObject.name + "] activeSelf=[" + this.gameObject.activeSelf + "] this.gameObject.activeInHierarchy=[" + this.gameObject.activeInHierarchy + "] transform x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
#endif
                GrownPlant grownPlant = GetComponent<GrownPlant>();
                if (grownPlant != null)
                {
                    PlantGenericController plantController = grownPlant.gameObject.GetComponent<PlantGenericController>();
                    LandTree1Controller treeController = grownPlant.gameObject.GetComponent<LandTree1Controller>();
                    PlantMonoTransformController plantMonoTransformController = grownPlant.gameObject.GetComponent<PlantMonoTransformController>();

                    float progress = -1.0f;
                    if (plantController != null && plantController._progress >= 0.0f)
                    {
                        progress = plantController._progress;
#if DEBUG_FLORA
                        Logger.Debug("GrownPlant) plantController) Saving progress=[" + progress + "] for gameObject name=[" + this.gameObject.name + "]");
#endif
                    }
                    else if (treeController != null && treeController._progress >= 0.0f)
                    {
                        progress = treeController._progress;
#if DEBUG_FLORA
                        Logger.Debug("GrownPlant) treeController) Saving progress=[" + progress + "] for gameObject name=[" + this.gameObject.name + "]");
#endif
                    }
                    else if (plantMonoTransformController != null && plantMonoTransformController._progress >= 0.0f)
                    {
                        progress = plantMonoTransformController._progress;
#if DEBUG_FLORA
                        Logger.Debug("GrownPlant) plantMonoTransformController) Saving progress=[" + progress + "] for gameObject name=[" + this.gameObject.name + "]");
                    }
                    else
                        Logger.Debug("GrownPlant) No controller found for gameObject name=[" + this.gameObject.name + "]");
#else
                    }
#endif

                    if (progress >= 0.0f)
                    {
                        // Open save directory
                        string saveFolder = FilesHelper.GetSaveFolderPath();
                        if (!saveFolder.Contains("/test/"))
                        {
                            if (!Directory.Exists(saveFolder))
                                Directory.CreateDirectory(saveFolder);

                            // Save custom flora state
                            File.WriteAllText(FilesHelper.Combine(saveFolder, "customflora_" + id.Id + ".txt"), Convert.ToString(progress, CultureInfo.InvariantCulture), Encoding.UTF8);
                        }
                        else
                            Logger.Warning("Cannot save custom flora state: Save game folder path \"" + saveFolder + "\" is incorrect.");
                    }
                }
#if DEBUG_FLORA
                else
                    Logger.Debug("Cannot find grownPlant. gameObject name=[" + this.gameObject.name + "] activeSelf=[" + this.gameObject.activeSelf + "] this.gameObject.activeInHierarchy=[" + this.gameObject.activeInHierarchy + "] transform x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
#endif
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            PlantGenericController landPlantController = this.gameObject.GetComponent<PlantGenericController>();
            LandTree1Controller landTreeController = this.gameObject.GetComponent<LandTree1Controller>();
            PlantMonoTransformController plantMonoTransformController = this.gameObject.GetComponent<PlantMonoTransformController>();
            PrefabIdentifier id = this.gameObject.GetComponent<PrefabIdentifier>();
            if (id == null)
                if ((id = GetComponent<PrefabIdentifier>()) == null)
                    if ((id = GetComponentInParent<PrefabIdentifier>()) == null)
                        return;

#if DEBUG_FLORA
            Logger.Debug("Entering onProtoDeserialize for gameobject name=[" + this.gameObject.name + "] id=[" + id.Id + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
#endif

            string filePath = FilesHelper.Combine(FilesHelper.GetSaveFolderPath(), "customflora_" + id.Id + ".txt");
            if (File.Exists(filePath))
            {
#if DEBUG_FLORA
                Logger.Debug("Saved file found for gameobject name=[" + this.gameObject.name + "]");
#endif
                string rawState = File.ReadAllText(filePath, Encoding.UTF8).Replace(',', '.'); // Replace , with . for backward compatibility.
                if (rawState == null)
                    return;
                string[] state = rawState.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (state != null && state.Length == 1)
                {
                    float passedProgress = 0.0f;
                    if (float.TryParse(state[0], NumberStyles.Float, CultureInfo.InvariantCulture, out passedProgress))
                    {
                        if (landPlantController != null)
                        {
                            landPlantController._passedProgress = passedProgress;
#if DEBUG_FLORA
                            Logger.Debug("landPlantController) Setting passedProgress to [" + passedProgress + "] for gameobject name=[" + this.gameObject.name + "]");
#endif
                        }
                        else if (landTreeController != null)
                        {
                            landTreeController._passedProgress = passedProgress;
#if DEBUG_FLORA
                            Logger.Debug("landTreeController) Setting passedProgress to [" + passedProgress + "] for gameobject name=[" + this.gameObject.name + "]");
#endif
                        }
                        else if (plantMonoTransformController != null)
                        {
                            plantMonoTransformController._passedProgress = passedProgress;
#if DEBUG_FLORA
                            Logger.Debug("plantMonoTransformController) Setting passedProgress to [" + passedProgress + "] for gameobject name=[" + this.gameObject.name + "]");
                        }
                        else
                            Logger.Debug("Controller not found for gameobject name=[" + this.gameObject.name + "] id=[" + id.Id + "]");
                    }
                    else
                        Logger.Debug("Cannot parse passedProgress for gameobject name=[" + this.gameObject.name + "]");
                }
                else
                    Logger.Debug("Cannot parse saved file for gameobject name=[" + this.gameObject.name + "]");
#else
                        }
                    }
                }
#endif
            }
        }
    }
}
