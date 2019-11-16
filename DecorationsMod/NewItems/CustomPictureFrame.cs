using DecorationsMod.Controllers;
using Harmony;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class CustomPictureFrame : DecorationItem
    {
        private Vector3 originEulerAngles = Vector3.zero;
        private Vector3 originImageRendererScale = Vector3.zero;
        private Vector3 originColliderSize = Vector3.zero;
        private Vector3 originConstructableBoundsExtents = Vector3.zero;

        private GameObject posterMagnetObj = null;
        private Texture normal = null;
        private Texture illum = null;

        public CustomPictureFrame()
        {
            this.ClassID = "CustomPictureFrame";
            this.PrefabFileName = $"{DecorationItem.DefaultResourcePath}{this.ClassID}";

            this.GameObject = Resources.Load<GameObject>("Submarine/Build/PictureFrame");

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("CustomPictureFrameName"),
                                                        LanguageHelper.GetFriendlyWord("CustomPictureFrameDescription"),
                                                        true);

            this.IsHabitatBuilder = true;

            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[2]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.CopperWire, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Glass, 1)
                    }),
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                posterMagnetObj = AssetsHelper.Assets.LoadAsset<GameObject>("poster_kitty");
                normal = AssetsHelper.Assets.LoadAsset<Texture>("poster_magnet_normal");
                illum = AssetsHelper.Assets.LoadAsset<Texture>("poster_magnet_illum");

                // Add new TechType to the buildables
                SMLHelper.V2.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                SMLHelper.V2.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("revertpictureframe"));

                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);
                
                // Override OnHandHover
                var pictureFrameType = typeof(PictureFrame);
                var onHandHoverMethod = pictureFrameType.GetMethod("OnHandHover", BindingFlags.Public | BindingFlags.Instance);
                var postfix = typeof(PictureFramePatch).GetMethod("OnHandHover_Postfix", BindingFlags.Public | BindingFlags.Static);
                DecorationsMod.HarmonyInstance.Patch(onHandHoverMethod, null, new HarmonyMethod(postfix));
                
                // Override OnHandClick
                var onHandClickMethod = pictureFrameType.GetMethod("OnHandClick", BindingFlags.Public | BindingFlags.Instance);
                var prefix = typeof(PictureFramePatch).GetMethod("OnHandClick_Prefix", BindingFlags.Public | BindingFlags.Static);
                DecorationsMod.HarmonyInstance.Patch(onHandClickMethod, new HarmonyMethod(prefix), null);
                
                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject posterPrefab = GameObject.Instantiate(this.posterMagnetObj);

            // Update poster border shader, normal/emission maps, hide parts of the prefab
            GameObject posterModel = posterPrefab.FindChild("model").FindChild("poster_kitty");
            MeshRenderer posterRenderer = posterModel.GetComponent<MeshRenderer>();
            Shader marmosetUber = Shader.Find("MarmosetUBER");
            foreach (Material tmpMat in posterRenderer.materials)
            {
                tmpMat.shader = marmosetUber;
                if (tmpMat.name.CompareTo("poster_magnet (Instance)") == 0)
                {
                    tmpMat.SetTexture("_BumpMap", normal);
                    tmpMat.SetTexture("_Illum", illum);
                    tmpMat.EnableKeyword("MARMO_NORMALMAP"); // Enable normal map
                    tmpMat.EnableKeyword("MARMO_EMISSION"); // Enable emission map
                }
            }
            posterPrefab.transform.parent = prefab.transform;
            posterPrefab.transform.localPosition = new Vector3(0.0f, 0.27f, -0.002f);
            posterPrefab.transform.localScale = new Vector3(34.0f, 34.0f, 34.0f);
            posterPrefab.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            posterPrefab.SetActive(true);
            posterRenderer.enabled = false;

            // Get prefab sub models
            GameObject model = prefab.FindChild("mesh");
            GameObject screen = prefab.FindChild("Screen");
            GameObject trigger = prefab.FindChild("Trigger");

            // Update prefab name
            prefab.name = this.ClassID;

            // Modify tech tag
            var techTag = prefab.GetComponent<TechTag>();
            techTag.type = this.TechType;

            // Modify prefab identifier
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Rotate model
            originEulerAngles = model.transform.localEulerAngles;
            model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y, model.transform.localEulerAngles.z + 90.0f);
            
            // Update box collider
            BoxCollider collider = trigger.GetComponent<BoxCollider>();
            collider.size = new Vector3(collider.size.x - 0.15f, collider.size.y - 0.15f, collider.size.z);
            originColliderSize = collider.size;
            // Rotate collider
            collider.size = new Vector3(collider.size.y, collider.size.x, collider.size.z);
            
            // Update sky applier
            var skyapplier = model.GetComponent<SkyApplier>();
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            skyapplier.renderers = renderers;
            skyapplier.anchorSky = Skies.Auto;
            
            // Update contructable
            var constructible = prefab.GetComponent<Constructable>();
            constructible.techType = this.TechType;

            // Rotate PictureFrame
            PictureFrame pf = prefab.GetComponent<PictureFrame>();
            originImageRendererScale = pf.imageRenderer.transform.localScale;
            pf.imageRenderer.transform.localScale = new Vector3(pf.imageRenderer.transform.localScale.y, pf.imageRenderer.transform.localScale.x, pf.imageRenderer.transform.localScale.z);
            
            // Update constructable bounds
            var constructableBounds = prefab.GetComponent<ConstructableBounds>();
            constructableBounds.bounds.extents = new Vector3(constructableBounds.bounds.extents.x * 0.85f, constructableBounds.bounds.extents.y * 0.85f, constructableBounds.bounds.extents.z);
            originConstructableBoundsExtents = constructableBounds.bounds.extents;
            constructableBounds.bounds.extents = new Vector3(constructableBounds.bounds.extents.y, constructableBounds.bounds.extents.x, constructableBounds.bounds.extents.z);
            
            // Add CustomPictureFrame controller
            CustomPictureFrameController cpfController = prefab.AddComponent<CustomPictureFrameController>();
            cpfController.OriginEulerAngles = this.originEulerAngles;
            cpfController.OriginColliderSize = this.originColliderSize;
            cpfController.OriginImageRendererScale = this.originImageRendererScale;
            cpfController.OriginConstructableBoundsExtents = this.originConstructableBoundsExtents;
            
            return prefab;
        }
    }
}
