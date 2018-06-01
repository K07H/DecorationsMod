using SMLHelper.Patchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
            placeTool.allowedOnRigidBody = false;
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
            GameObject polyaniline = Resources.Load<GameObject>("WorldEntities/Natural/polyaniline");
            MakeItemPlaceable(TechType.Polyaniline, polyaniline);
            GameObject benzene = Resources.Load<GameObject>("WorldEntities/Natural/benzene");
            MakeItemPlaceable(TechType.Benzene, benzene);
            GameObject hydrochloricacid = Resources.Load<GameObject>("WorldEntities/Natural/hydrochloricacid");
            MakeItemPlaceable(TechType.HydrochloricAcid, hydrochloricacid);
            GameObject hatchingenzymes = Resources.Load<GameObject>("WorldEntities/Natural/HatchingEnzymes");
            MakeItemPlaceable(TechType.HatchingEnzymes, hatchingenzymes);
            GameObject reactorrod = Resources.Load<GameObject>("WorldEntities/Natural/reactorrod");
            MakeItemPlaceable(TechType.ReactorRod, reactorrod);
            GameObject depletedreactorrod = Resources.Load<GameObject>("WorldEntities/Natural/depletedreactorrod");
            MakeItemPlaceable(TechType.DepletedReactorRod, depletedreactorrod);
            GameObject coffee = Resources.Load<GameObject>("WorldEntities/Food/Coffee");
            MakeItemPlaceable(TechType.Coffee, coffee);
        }
    }
}
