using UnityEngine;

namespace DecorationsMod.Fixers
{
    public class AquariumFixer
    {
        public static void AddItem_Postfix(Aquarium __instance, InventoryItem item)
        {
            var pid = __instance?.gameObject?.GetComponent<PrefabIdentifier>();
#if DEBUG_AQUARIUM
            Logger.Log("DEBUG: Entering AddItem_Postfix for aquarium ClassID=[" + (!string.IsNullOrEmpty(pid?.ClassId) ? pid.ClassId : "?") + "]");
#endif
            // If current item is our custom aquarium (or if it's the regular aquarium and "FixAquariumLighting" is enabled).
            if (pid != null && !string.IsNullOrEmpty(pid.ClassId) &&
                (pid.ClassId == "AquariumSmall" || (ConfigSwitcher.FixAquariumLighting && pid.ClassId == "6d71afaa-09b6-44d3-ba2d-66644ffe6a99")))
            {
                GameObject model = __instance.gameObject.FindChild("model");
                if (model != null)
                {
                    PrefabsHelper.FixAquariumFishesSkyApplier(model.FindChild("Aquarium_animation").FindChild("root"));
                    PrefabsHelper.FixAquariumFishesSkyApplier(model.FindChild("Aquarium_animation2").FindChild("root"));
                }
            }
        }

        /*
        public static bool AddItem_Prefix(Aquarium __instance, InventoryItem item)
        {
            var pid = __instance?.gameObject?.GetComponent<PrefabIdentifier>();
#if DEBUG_AQUARIUM
            Logger.Log("DEBUG: Entering AddItem_Prefix for aquarium ClassID=[" + (!string.IsNullOrEmpty(pid?.ClassId) ? pid.ClassId : "?") + "]");
#endif
            // If current item is our custom aquarium (or if it's the regular aquarium and "FixAquariumLighting" is enabled).
            if (pid != null && !string.IsNullOrEmpty(pid.ClassId) && 
                (pid.ClassId == "AquariumSmall" || (ConfigSwitcher.FixAquariumLighting && pid.ClassId == "6d71afaa-09b6-44d3-ba2d-66644ffe6a99")))
            {
                GameObject gameObject = item.item.gameObject;
                AquariumFish component = gameObject.GetComponent<AquariumFish>();
                if (component == null)
                    return false;
                TechType techType = item.item.GetTechType();
                MethodInfo getFreeTrackMethod = typeof(Aquarium).GetMethod("GetFreeTrack", BindingFlags.NonPublic | BindingFlags.Instance);
                object freeTrack = getFreeTrackMethod.Invoke(__instance, null);
                //Aquarium.FishTrack freeTrack = this.GetFreeTrack();
                GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(component.model, Vector3.zero, Quaternion.identity);
                SkyApplier sa = gameObject2.GetComponent<SkyApplier>();
                if (sa == null)
                    sa = gameObject2.AddComponent<SkyApplier>();
                sa.anchorSky = Skies.Auto;
                sa.renderers = gameObject2.GetAllComponentsInChildren<Renderer>();
                sa.dynamic = true;
                sa.updaterIndex = 0;
                Type fishTrackType = typeof(Aquarium).GetNestedType("FishTrack", BindingFlags.NonPublic);
                FieldInfo trackField = fishTrackType.GetField("track");
                FieldInfo fishTypeField = fishTrackType.GetField("fishType");
                FieldInfo fishField = fishTrackType.GetField("fish");
                FieldInfo itemField = fishTrackType.GetField("item");
                FieldInfo infectedMixinField = fishTrackType.GetField("infectedMixin");
                GameObject track = (GameObject)trackField.GetValue(freeTrack);
                gameObject2.transform.SetParent(track.transform, false);
                //gameObject2.transform.SetParent(freeTrack.track.transform, false);
                gameObject2.transform.localScale *= 0.3f;
                MethodInfo setupRenderersMethod = typeof(Aquarium).GetMethod("SetupRenderers", BindingFlags.NonPublic | BindingFlags.Instance);
                setupRenderersMethod.Invoke(__instance, new object[] { gameObject2 });
                //this.SetupRenderers(gameObject2);
                AnimateByVelocity componentInChildren = gameObject2.GetComponentInChildren<AnimateByVelocity>();
                componentInChildren.rootGameObject = gameObject2;
                componentInChildren.animationMoveMaxSpeed = 0.5f;
                fishTypeField.SetValue(freeTrack, techType);
                //freeTrack.fishType = techType;
                fishField.SetValue(freeTrack, gameObject2);
                //freeTrack.fish = gameObject2;
                itemField.SetValue(freeTrack, gameObject);
                //freeTrack.item = gameObject;
                InfectedMixin component2 = gameObject.GetComponent<InfectedMixin>();
                if (component2 != null)
                {
                    InfectedMixin infectedMixin = gameObject2.AddComponent<InfectedMixin>();
                    MethodInfo getMarmosetRenderersMethod = typeof(Aquarium).GetMethod("GetMarmosetRenderers", BindingFlags.NonPublic | BindingFlags.Static);
                    List<Renderer> marmosetRenderers = (List<Renderer>)getMarmosetRenderersMethod.Invoke(null, new object[] { gameObject2 });
                    infectedMixin.renderers = marmosetRenderers.ToArray();
                    //infectedMixin.renderers = Aquarium.GetMarmosetRenderers(gameObject2).ToArray();
                    infectedMixin.SetInfectedAmount(component2.GetInfectedAmount());
                    infectedMixinField.SetValue(freeTrack, infectedMixin);
                    //freeTrack.infectedMixin = infectedMixin;
                }
                gameObject2.SetActive(true);
                MethodInfo updateInfectionSpreadingMethod = typeof(Aquarium).GetMethod("UpdateInfectionSpreading", BindingFlags.NonPublic | BindingFlags.Instance);
                updateInfectionSpreadingMethod.Invoke(__instance, null);
                //this.UpdateInfectionSpreading();
                return false;
            }
            return true;
        }
        */
    }
}
