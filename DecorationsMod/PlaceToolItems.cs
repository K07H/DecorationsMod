using UnityEngine;
using SMLHelper.Patchers;
using System.Collections.Generic;

namespace DecorationsMod
{
    public static class PlaceToolItems
    {
        private static void MakeItemPlaceable(TechType techType, GameObject item, Collider collider = null)
        {
            // We can pick this item
            var pickupable = item.GetComponent<Pickupable>();
            if (pickupable == null)
                pickupable = item.AddComponent<Pickupable>();

            // We can place this item
            var placeTool = item.GetComponent<PlaceTool>();
            if (placeTool == null)
                placeTool = item.AddComponent<PlaceTool>();
            placeTool.allowedInBase = true;
            placeTool.allowedOnBase = true;
            placeTool.allowedOnCeiling = false;
            placeTool.allowedOnConstructable = true;
            placeTool.allowedOnGround = true;
            placeTool.allowedOnRigidBody = true;
            placeTool.allowedOnWalls = false;
            placeTool.allowedOutside = false;
            placeTool.rotationEnabled = true;
            placeTool.enabled = true;
            placeTool.hasAnimations = false;
            placeTool.hasBashAnimation = false;
            placeTool.hasFirstUseAnimation = false;
            // Associate pickupable
            placeTool.pickupable = pickupable;
            // Try get collider
            if (collider == null)
            {
                collider = item.GetComponent<Collider>();
                if (collider == null)
                    collider = item.GetComponentInChildren<Collider>();
            }
            // Associate collider
            if (collider != null)
                placeTool.mainCollider = collider;

            // Add TechType to the hand-equipments
            CraftDataPatcher.customEquipmentTypes.Add(techType, EquipmentType.Hand);
        }

        public static void MakeEggsPlaceable()
        {
            GameObject egg1 = Resources.Load<GameObject>("WorldEntities/Eggs/BonesharkEgg");
            MakeItemPlaceable(TechType.BonesharkEgg, egg1);
            MakeItemPlaceable(TechType.BonesharkEggUndiscovered, egg1);
            GameObject egg2 = Resources.Load<GameObject>("WorldEntities/Eggs/CrabsnakeEgg");
            MakeItemPlaceable(TechType.CrabsnakeEgg, egg2);
            MakeItemPlaceable(TechType.CrabsnakeEggUndiscovered, egg2);
            GameObject egg3 = Resources.Load<GameObject>("WorldEntities/Eggs/CrabSquidEgg");
            MakeItemPlaceable(TechType.CrabsquidEgg, egg3);
            MakeItemPlaceable(TechType.CrabsquidEggUndiscovered, egg3);
            GameObject egg4 = Resources.Load<GameObject>("WorldEntities/Eggs/CrashEgg");
            MakeItemPlaceable(TechType.CrashEgg, egg4);
            MakeItemPlaceable(TechType.CrashEggUndiscovered, egg4);
            GameObject egg5 = Resources.Load<GameObject>("WorldEntities/Eggs/CuteEgg");
            MakeItemPlaceable(TechType.CutefishEgg, egg5);
            MakeItemPlaceable(TechType.CutefishEggUndiscovered, egg5);
            GameObject egg6 = Resources.Load<GameObject>("WorldEntities/Eggs/GasopodEgg");
            MakeItemPlaceable(TechType.GasopodEgg, egg6);
            MakeItemPlaceable(TechType.GasopodEggUndiscovered, egg6);
            GameObject egg7 = Resources.Load<GameObject>("WorldEntities/Eggs/JellyrayEgg");
            MakeItemPlaceable(TechType.JellyrayEgg, egg7);
            MakeItemPlaceable(TechType.JellyrayEggUndiscovered, egg7);
            GameObject egg8 = Resources.Load<GameObject>("WorldEntities/Eggs/JumperEgg");
            MakeItemPlaceable(TechType.JumperEgg, egg8);
            MakeItemPlaceable(TechType.JumperEggUndiscovered, egg8);
            GameObject egg9 = Resources.Load<GameObject>("WorldEntities/Eggs/LavaLizardEgg");
            MakeItemPlaceable(TechType.LavaLizardEgg, egg9);
            MakeItemPlaceable(TechType.LavaLizardEggUndiscovered, egg9);
            GameObject egg10 = Resources.Load<GameObject>("WorldEntities/Eggs/MesmerEgg");
            MakeItemPlaceable(TechType.MesmerEgg, egg10);
            MakeItemPlaceable(TechType.MesmerEggUndiscovered, egg10);
            GameObject egg11 = Resources.Load<GameObject>("WorldEntities/Eggs/RabbitRayEgg");
            MakeItemPlaceable(TechType.RabbitrayEgg, egg11);
            MakeItemPlaceable(TechType.RabbitrayEggUndiscovered, egg11);
            GameObject egg12 = Resources.Load<GameObject>("WorldEntities/Eggs/ReefbackEgg");
            MakeItemPlaceable(TechType.ReefbackEgg, egg12);
            MakeItemPlaceable(TechType.ReefbackEggUndiscovered, egg12);
            GameObject egg13 = Resources.Load<GameObject>("WorldEntities/Eggs/SandsharkEgg");
            MakeItemPlaceable(TechType.SandsharkEgg, egg13);
            MakeItemPlaceable(TechType.SandsharkEggUndiscovered, egg13);
            GameObject egg14 = Resources.Load<GameObject>("WorldEntities/Eggs/ShockerEgg");
            GameObject egg14Model = egg14.FindChild("Creatures_eggs_10");
            egg14Model.transform.localPosition = new Vector3(egg14Model.transform.localPosition.x, egg14Model.transform.localPosition.y + 0.05f, egg14Model.transform.localPosition.z);
            MakeItemPlaceable(TechType.ShockerEgg, egg14);
            MakeItemPlaceable(TechType.ShockerEggUndiscovered, egg14);
            GameObject egg15 = Resources.Load<GameObject>("WorldEntities/Eggs/SpadefishEgg");
            MakeItemPlaceable(TechType.SpadefishEgg, egg15);
            MakeItemPlaceable(TechType.SpadefishEggUndiscovered, egg15);
            GameObject egg16 = Resources.Load<GameObject>("WorldEntities/Eggs/StalkerEgg");
            MakeItemPlaceable(TechType.StalkerEgg, egg16);
            MakeItemPlaceable(TechType.StalkerEggUndiscovered, egg16);
        }

        public static void MakeItemsPlaceable()
        {
            // Chimicals
            GameObject bleach = Resources.Load<GameObject>("WorldEntities/Natural/bleach");
            GameObject bleachModel = bleach.FindChild("model");
            bleachModel.transform.localPosition = new Vector3(bleachModel.transform.localPosition.x, bleachModel.transform.localPosition.y + 0.15f, bleachModel.transform.localPosition.z);
            MakeItemPlaceable(TechType.Bleach, bleach);
            GameObject lubricant = Resources.Load<GameObject>("WorldEntities/Natural/lubricant");
            GameObject lubricantModel = lubricant.FindChild("model");
            lubricantModel.transform.localPosition = new Vector3(lubricantModel.transform.localPosition.x, lubricantModel.transform.localPosition.y + 0.15f, lubricantModel.transform.localPosition.z);
            MakeItemPlaceable(TechType.Lubricant, lubricant);
            GameObject polyaniline = Resources.Load<GameObject>("WorldEntities/Natural/polyaniline");
            MakeItemPlaceable(TechType.Polyaniline, polyaniline);
            GameObject benzene = Resources.Load<GameObject>("WorldEntities/Natural/benzene");
            MakeItemPlaceable(TechType.Benzene, benzene);
            GameObject hydrochloricacid = Resources.Load<GameObject>("WorldEntities/Natural/hydrochloricacid");
            MakeItemPlaceable(TechType.HydrochloricAcid, hydrochloricacid);
            GameObject hatchingenzymes = Resources.Load<GameObject>("WorldEntities/Natural/HatchingEnzymes");
            MakeItemPlaceable(TechType.HatchingEnzymes, hatchingenzymes);

            // Food & water
            GameObject coffee = Resources.Load<GameObject>("WorldEntities/Food/Coffee");
            MakeItemPlaceable(TechType.Coffee, coffee);
            GameObject bigfilteredwater = Resources.Load<GameObject>("WorldEntities/Food/BigFilteredWater");
            MakeItemPlaceable(TechType.BigFilteredWater, bigfilteredwater);
            GameObject disinfectedwater = Resources.Load<GameObject>("WorldEntities/Food/DisinfectedWater");
            GameObject disinfectedwaterModel = disinfectedwater.FindChild("model");
            disinfectedwaterModel.transform.localPosition = new Vector3(disinfectedwaterModel.transform.localPosition.x, disinfectedwaterModel.transform.localPosition.y + 0.17f, disinfectedwaterModel.transform.localPosition.z);
            MakeItemPlaceable(TechType.DisinfectedWater, disinfectedwater);
            GameObject filteredwater = Resources.Load<GameObject>("WorldEntities/Food/FilteredWater");
            GameObject filteredwaterModel = filteredwater.FindChild("model");
            filteredwaterModel.transform.localPosition = new Vector3(filteredwaterModel.transform.localPosition.x, filteredwaterModel.transform.localPosition.y + 0.155f, filteredwaterModel.transform.localPosition.z);
            MakeItemPlaceable(TechType.FilteredWater, filteredwater);

            // Snacks
            GameObject snack1 = Resources.Load<GameObject>("WorldEntities/Food/Snack1");
            BoxCollider snack1Collider = snack1.AddComponent<BoxCollider>();
            snack1Collider.size = new Vector3(0.17f, 0.18f, 0.8f);
            MakeItemPlaceable(TechType.Snack1, snack1, snack1Collider);
            GameObject snack2 = Resources.Load<GameObject>("WorldEntities/Food/Snack2");
            BoxCollider snack2Collider = snack2.AddComponent<BoxCollider>();
            snack2Collider.size = new Vector3(0.17f, 0.18f, 0.8f);
            MakeItemPlaceable(TechType.Snack2, snack2, snack2Collider);
            GameObject snack3 = Resources.Load<GameObject>("WorldEntities/Food/Snack3");
            BoxCollider snack3Collider = snack3.AddComponent<BoxCollider>();
            snack3Collider.size = new Vector3(0.17f, 0.18f, 0.8f);
            MakeItemPlaceable(TechType.Snack3, snack3, snack3Collider);

            // Swap Snack2 and Snack3 techtypes (as tooltips do not match models)
            var snack2PrefabId = snack2.GetComponent<PrefabIdentifier>();
            var snack3PrefabId = snack3.GetComponent<PrefabIdentifier>();
            var snack2TechTag = snack2.GetComponent<TechTag>();
            var snack3TechTag = snack3.GetComponent<TechTag>();
            string tmpclassid = snack2PrefabId.ClassId;
            snack2PrefabId.ClassId = snack3PrefabId.ClassId;
            snack3PrefabId.ClassId = tmpclassid;
            string tmpname = snack2PrefabId.name;
            snack2PrefabId.name = snack3PrefabId.name;
            snack3PrefabId.name = tmpname;
            TechType tmpTechType = snack2TechTag.type;
            snack2TechTag.type = snack3TechTag.type;
            snack3TechTag.type = tmpTechType;
            
            // Electronics
            GameObject wiringkit = Resources.Load<GameObject>("WorldEntities/Natural/wiringkit");
            GameObject wiringkitModel = wiringkit.FindChild("model");
            wiringkitModel.transform.localPosition = new Vector3(wiringkitModel.transform.localPosition.x, wiringkitModel.transform.localPosition.y + 0.03f, wiringkitModel.transform.localPosition.z);
            MakeItemPlaceable(TechType.WiringKit, wiringkit);
            GameObject advancedwiringkit = Resources.Load<GameObject>("WorldEntities/Natural/advancedwiringkit");
            GameObject advancedwiringkitModel = advancedwiringkit.FindChild("model");
            advancedwiringkitModel.transform.localPosition = new Vector3(advancedwiringkitModel.transform.localPosition.x, advancedwiringkitModel.transform.localPosition.y + 0.03f, advancedwiringkitModel.transform.localPosition.z);
            MakeItemPlaceable(TechType.AdvancedWiringKit, advancedwiringkit);
            GameObject computerchip = Resources.Load<GameObject>("WorldEntities/Natural/computerchip");
            GameObject computerchipModel = computerchip.FindChild("model");
            computerchipModel.transform.localPosition = new Vector3(computerchipModel.transform.localPosition.x, computerchipModel.transform.localPosition.y + 0.02f, computerchipModel.transform.localPosition.z);
            MakeItemPlaceable(TechType.ComputerChip, computerchip);

            // Eggs
            MakeEggsPlaceable();

            // Others
            GameObject stalkertooth = Resources.Load<GameObject>("WorldEntities/Natural/stalkertooth");
            GameObject stalkertoothModel = stalkertooth.FindChild("shark_tooth");
            stalkertoothModel.transform.localEulerAngles = new Vector3(stalkertoothModel.transform.localEulerAngles.x, stalkertoothModel.transform.localEulerAngles.y, stalkertoothModel.transform.localEulerAngles.z + -45.0f);
            stalkertoothModel.transform.localPosition = new Vector3(stalkertoothModel.transform.localPosition.x, stalkertoothModel.transform.localPosition.y - 0.08f, stalkertoothModel.transform.localPosition.z);
            MakeItemPlaceable(TechType.StalkerTooth, stalkertooth);
            GameObject firstaidkit = Resources.Load<GameObject>("WorldEntities/Natural/firstaidkit");
            MakeItemPlaceable(TechType.FirstAidKit, firstaidkit);
        }
    }
}
