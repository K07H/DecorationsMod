using UnityEngine;
using SMLHelper.Patchers;

namespace DecorationsFabricator
{
    public static class PlaceToolItems
    {
        private static void MakeItemPlaceable(TechType techType, GameObject item, Collider collider = null)
        {
            // We can pick this item
            var pickupable = item.GetComponent<Pickupable>();
            if (pickupable == null)
                pickupable = item.AddComponent<Pickupable>();
            pickupable.isPickupable = true;
            pickupable.randomizeRotationWhenDropped = true;

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
            placeTool.pickupable = pickupable;
            if (collider != null)
                placeTool.mainCollider = collider;

            // Add TechType to the hand-equipments
            CraftDataPatcher.customEquipmentTypes.Add(techType, EquipmentType.Hand);
        }

        public static void MakeItemsPlaceable()
        {
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

            GameObject reactorrod = Resources.Load<GameObject>("WorldEntities/Natural/reactorrod");
            GameObject reactorrodModel = reactorrod.FindChild("model");
            reactorrodModel.transform.localPosition = new Vector3(reactorrodModel.transform.localPosition.x, reactorrodModel.transform.localPosition.y + 0.3f, reactorrodModel.transform.localPosition.z);
            MakeItemPlaceable(TechType.ReactorRod, reactorrod);

            GameObject depletedreactorrod = Resources.Load<GameObject>("WorldEntities/Natural/depletedreactorrod");
            GameObject depletedreactorrodModel = depletedreactorrod.FindChild("model");
            depletedreactorrodModel.transform.localPosition = new Vector3(depletedreactorrodModel.transform.localPosition.x, depletedreactorrodModel.transform.localPosition.y + 0.3f, depletedreactorrodModel.transform.localPosition.z);
            MakeItemPlaceable(TechType.DepletedReactorRod, depletedreactorrod);

            GameObject stalkertooth = Resources.Load<GameObject>("WorldEntities/Natural/stalkertooth");
            GameObject stalkertoothModel = stalkertooth.FindChild("shark_tooth");
            stalkertoothModel.transform.localEulerAngles = new Vector3(stalkertoothModel.transform.localEulerAngles.x, stalkertoothModel.transform.localEulerAngles.y, stalkertoothModel.transform.localEulerAngles.z + -45.0f);
            stalkertoothModel.transform.localPosition = new Vector3(stalkertoothModel.transform.localPosition.x, stalkertoothModel.transform.localPosition.y - 0.08f, stalkertoothModel.transform.localPosition.z);
            MakeItemPlaceable(TechType.StalkerTooth, stalkertooth);
            GameObject firstaidkit = Resources.Load<GameObject>("WorldEntities/Natural/firstaidkit");
            MakeItemPlaceable(TechType.FirstAidKit, firstaidkit);

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
        }
    }
}
