using System;
using System.Globalization;
using System.IO;
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
            Logger.Log("DEBUG: Entering onProtoSerialize for gameobject name=[" + this.gameObject.name + "] id=[" + id.Id + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
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
                    Logger.Log("DEBUG: LinkedGrownPlant) plantController) Saving progress=[" + progress + "] for gameObject name=[" + this.gameObject.name + "]");
#endif
                }
                else if (treeController != null && treeController._progress >= 0.0f)
                {
                    progress = treeController._progress;
#if DEBUG_FLORA
                    Logger.Log("DEBUG: LinkedGrownPlant) treeController) Saving progress=[" + progress + "] for gameObject name=[" + this.gameObject.name + "]");
#endif
                }
                else if (plantMonoTransformController != null && plantMonoTransformController._progress >= 0.0f)
                {
                    progress = plantMonoTransformController._progress;
#if DEBUG_FLORA
                    Logger.Log("DEBUG: LinkedGrownPlant) plantMonoTransformController) Saving progress=[" + progress + "] for gameObject name=[" + this.gameObject.name + "]");
                }
                else
                    Logger.Log("DEBUG: LinkedGrownPlant) No controller found for gameObject name=[" + this.gameObject.name + "]");
#else
                }
#endif

                if (progress >= 0.0f)
                {
                    // Open save directory
                    string saveFolder = FilesHelper.GetSaveFolderPath();
                    if (!Directory.Exists(saveFolder))
                        Directory.CreateDirectory(saveFolder);

                    // Save custom flora state
                    File.WriteAllText(Path.Combine(saveFolder, "customflora_" + id.Id + ".txt"), Convert.ToString(progress, CultureInfo.InvariantCulture));
                }
            }
            else
            {
#if DEBUG_FLORA
                Logger.Log("DEBUG: Cannot find linkedGrownPlant. this.gameObject name=[" + this.gameObject.name + "] activeSelf=[" + this.gameObject.activeSelf + "] this.gameObject.activeInHierarchy=[" + this.gameObject.activeInHierarchy + "] transform x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
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
                        Logger.Log("DEBUG: GrownPlant) plantController) Saving progress=[" + progress + "] for gameObject name=[" + this.gameObject.name + "]");
#endif
                    }
                    else if (treeController != null && treeController._progress >= 0.0f)
                    {
                        progress = treeController._progress;
#if DEBUG_FLORA
                        Logger.Log("DEBUG: GrownPlant) treeController) Saving progress=[" + progress + "] for gameObject name=[" + this.gameObject.name + "]");
#endif
                    }
                    else if (plantMonoTransformController != null && plantMonoTransformController._progress >= 0.0f)
                    {
                        progress = plantMonoTransformController._progress;
#if DEBUG_FLORA
                        Logger.Log("DEBUG: GrownPlant) plantMonoTransformController) Saving progress=[" + progress + "] for gameObject name=[" + this.gameObject.name + "]");
                    }
                    else
                        Logger.Log("DEBUG: GrownPlant) No controller found for gameObject name=[" + this.gameObject.name + "]");
#else
                    }
#endif

                    if (progress >= 0.0f)
                    {
                        // Open save directory
                        string saveFolder = FilesHelper.GetSaveFolderPath();
                        if (!Directory.Exists(saveFolder))
                            Directory.CreateDirectory(saveFolder);

                        // Save custom flora state
                        File.WriteAllText(Path.Combine(saveFolder, "customflora_" + id.Id + ".txt"), Convert.ToString(progress, CultureInfo.InvariantCulture));
                    }
                }
#if DEBUG_FLORA
                else
                    Logger.Log("DEBUG: Cannot find grownPlant. gameObject name=[" + this.gameObject.name + "] activeSelf=[" + this.gameObject.activeSelf + "] this.gameObject.activeInHierarchy=[" + this.gameObject.activeInHierarchy + "] transform x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
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
            Logger.Log("DEBUG: Entering onProtoDeserialize for gameobject name=[" + this.gameObject.name + "] id=[" + id.Id + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
#endif

            string filePath = Path.Combine(FilesHelper.GetSaveFolderPath(), "customflora_" + id.Id + ".txt");
            if (File.Exists(filePath))
            {
#if DEBUG_FLORA
                Logger.Log("DEBUG: Saved file found for gameobject name=[" + this.gameObject.name + "]");
#endif
                string rawState = File.ReadAllText(filePath).Replace(',', '.'); // Replace , with . for backward compatibility.
                if (rawState == null)
                    return;
                string[] state = rawState.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (state != null && state.Length == 1)
                {
                    float passedProgress = 0.0f;
                    if (float.TryParse(state[0], NumberStyles.Float, CultureInfo.InvariantCulture, out passedProgress))
                    {
                        if (landPlantController != null)
                        {
                            landPlantController._passedProgress = passedProgress;
#if DEBUG_FLORA
                            Logger.Log("DEBUG: landPlantController) Setting passedProgress to [" + passedProgress + "] for gameobject name=[" + this.gameObject.name + "]");
#endif
                        }
                        else if (landTreeController != null)
                        {
                            landTreeController._passedProgress = passedProgress;
#if DEBUG_FLORA
                            Logger.Log("DEBUG: landTreeController) Setting passedProgress to [" + passedProgress + "] for gameobject name=[" + this.gameObject.name + "]");
#endif
                        }
                        else if (plantMonoTransformController != null)
                        {
                            plantMonoTransformController._passedProgress = passedProgress;
#if DEBUG_FLORA
                            Logger.Log("DEBUG: plantMonoTransformController) Setting passedProgress to [" + passedProgress + "] for gameobject name=[" + this.gameObject.name + "]");
                        }
                        else
                            Logger.Log("DEBUG: Controller not found for gameobject name=[" + this.gameObject.name + "] id=[" + id.Id + "]");
                    }
                    else
                        Logger.Log("DEBUG: Cannot parse passedProgress for gameobject name=[" + this.gameObject.name + "]");
                }
                else
                    Logger.Log("DEBUG: Cannot parse saved file for gameobject name=[" + this.gameObject.name + "]");
#else
                        }
                    }
                }
#endif
            }
        }
    }
}
