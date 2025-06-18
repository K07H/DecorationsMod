using System;
using System.Globalization;
using System.IO;
using System.Text;
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
            reticle.SetTextRaw(HandReticle.TextType.Hand, LanguageHelper.GetFriendlyWord("DisplayCoveTreeEggs"));
        }

        public void OnProtoSerialize(ProtobufSerializer serializer)
        {
#if DEBUG_COVE_TREE
            Logger.Debug("DEBUG: OnProtoSerialize covetree: Entry");
#endif
            // Retrieve prefab unique ID
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            if (id == null)
                if ((id = GetComponent<PrefabIdentifier>()) == null)
                    return;

#if DEBUG_COVE_TREE
            Logger.Debug("DEBUG: OnProtoSerialize covetree: Get save folder");
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
                Logger.Debug("DEBUG: OnProtoSerialize covetree: Found grown plant. Eggs active=[" + eggs.activeSelf + "]");
#endif
                File.WriteAllText(FilesHelper.Combine(saveFolder, "covetree_" + id.Id + ".txt"), eggs.activeSelf ? "1" : "0", Encoding.UTF8);
            }
#if DEBUG_COVE_TREE
            else
                Logger.Debug("DEBUG: OnProtoSerialize covetree: Cannot find grown plant");
#endif
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
#if DEBUG_COVE_TREE
            Logger.Debug("DEBUG: OnProtoDeserialize covetree: Entry");
#endif
            // Retrieve prefab unique ID
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            if (id == null)
                if ((id = GetComponent<PrefabIdentifier>()) == null)
                    return;

#if DEBUG_COVE_TREE
            Logger.Debug("DEBUG: OnProtoDeserialize covetree: Loading saved file");
#endif
            string filePath = FilesHelper.Combine(FilesHelper.GetSaveFolderPath(), "covetree_" + id.Id + ".txt");
            if (File.Exists(filePath))
            {
                string covetreedata = File.ReadAllText(filePath, Encoding.UTF8);
                if (covetreedata == null)
                    return;
                string[] covetreeparams = covetreedata.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (covetreeparams != null && covetreeparams.Length == 1)
                {
                    // Check if we need to display eggs
                    bool showEggs = (string.Compare(covetreeparams[0], "1", false, CultureInfo.InvariantCulture) == 0);
#if DEBUG_COVE_TREE
                    Logger.Debug("DEBUG: OnProtoDeserialize covetree: showEggs=[" + showEggs + "]");
#endif

                    // Get eggs game objects
                    GameObject model = this.gameObject.FindChild("lost_river_cove_tree_01");
                    GameObject eggs = model.FindChild("lost_river_cove_tree_01_eggs");
                    GameObject shells = model.FindChild("lost_river_cove_tree_01_eggs_shells");
                    
                    // Show/hide eggs
                    eggs.SetActive(showEggs);
                    shells.SetActive(showEggs);
#if DEBUG_COVE_TREE
                    Logger.Debug("DEBUG: OnProtoDeserialize covetree: showEggs value has been set");
#endif
                }

            }
        }
    }
}
