using System;
using System.IO;
using UnityEngine;

namespace DecorationsMod.Controllers
{
    public class CoveTree1Controller : HandTarget, IHandTarget, IProtoEventListener
    {
        public void OnHandClick(GUIHand hand)
        {
            if (!enabled)
                return;

            // Get eggs game objects
            GameObject model = this.gameObject.FindChild("lost_river_cove_tree_01");
            GameObject eggs = model.FindChild("lost_river_cove_tree_01_eggs");
            GameObject shells = model.FindChild("lost_river_cove_tree_01_eggs_shells");
            
            // Show/hide eggs
            bool enable = !eggs.activeSelf;
            eggs.SetActive(enable);
            shells.SetActive(enable);
        }

        public void OnHandHover(GUIHand hand)
        {
            if (!enabled)
                return;

            var reticle = HandReticle.main;
            reticle.SetIcon(HandReticle.IconType.Hand, 1f);
#if BELOWZERO
            reticle.SetTextRaw(HandReticle.TextType.Hand, LanguageHelper.GetFriendlyWord("DisplayCoveTreeEggs"));
#else
            reticle.SetInteractText("DisplayCoveTreeEggs");
#endif
        }

        public void OnProtoSerialize(ProtobufSerializer serializer)
        {
#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: OnProtoSerialize covetree: Entry");
#endif
            // Retrieve prefab unique ID
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            if (id == null)
                if ((id = GetComponent<PrefabIdentifier>()) == null)
                    return;

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: OnProtoSerialize covetree: Get save folder");
#endif
            string saveFolder = FilesHelper.GetSaveFolderPath();
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            Plantable plant = this.gameObject.GetComponent<Plantable>();
            GrownPlant grownPlant = this.gameObject.GetComponent<GrownPlant>();
            if (grownPlant == null && plant != null && plant.linkedGrownPlant != null)
                grownPlant = plant.linkedGrownPlant;
            if (grownPlant != null)
            {
                GameObject eggs = grownPlant.gameObject.FindChild("lost_river_cove_tree_01").FindChild("lost_river_cove_tree_01_eggs");
#if DEBUG_COVE_TREE
                Logger.Log("DEBUG: OnProtoSerialize covetree: Found grown plant. Eggs active=[" + eggs.activeSelf + "]");
#endif
                File.WriteAllText(Path.Combine(saveFolder, "covetree_" + id.Id + ".txt"), (eggs.activeSelf ? "1" : "0"));
            }
#if DEBUG_COVE_TREE
            else
                Logger.Log("DEBUG: OnProtoSerialize covetree: Cannot find grown plant");
#endif
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: OnProtoDeserialize covetree: Entry");
#endif
            // Retrieve prefab unique ID
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            if (id == null)
                if ((id = GetComponent<PrefabIdentifier>()) == null)
                    return;

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: OnProtoDeserialize covetree: Loading saved file");
#endif
            string filePath = Path.Combine(FilesHelper.GetSaveFolderPath(), "covetree_" + id.Id + ".txt");
            if (File.Exists(filePath))
            {
                string covetreedata = File.ReadAllText(filePath);
                string[] covetreeparams = covetreedata.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (covetreeparams.Length == 1)
                {
                    // Check if we need to display eggs
                    bool showEggs = (covetreeparams[0].CompareTo("1") == 0);
#if DEBUG_COVE_TREE
                    Logger.Log("DEBUG: OnProtoDeserialize covetree: showEggs=[" + showEggs + "]");
#endif

                    // Get eggs game objects
                    GameObject model = this.gameObject.FindChild("lost_river_cove_tree_01");
                    GameObject eggs = model.FindChild("lost_river_cove_tree_01_eggs");
                    GameObject shells = model.FindChild("lost_river_cove_tree_01_eggs_shells");
                    
                    // Show/hide eggs
                    eggs.SetActive(showEggs);
                    shells.SetActive(showEggs);
#if DEBUG_COVE_TREE
                    Logger.Log("DEBUG: OnProtoDeserialize covetree: showEggs value has been set");
#endif
                }

            }
        }
    }
}
