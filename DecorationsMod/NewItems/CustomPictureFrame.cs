using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using HarmonyLib;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class CustomPictureFrame : DecorationItem
    {
        private GameObject posterMagnetObj = null;
        private Texture normal = null;
        private Texture illum = null;

        [SetsRequiredMembers]
        public CustomPictureFrame() : base("CustomPictureFrame", LanguageHelper.GetFriendlyWord("CustomPictureFrameName"), LanguageHelper.GetFriendlyWord("CustomPictureFrameDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("revertpictureframe"))
        {
            this.ClassID = "CustomPictureFrame";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.CustomizablePictureFrame = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

            this.IsHabitatBuilder = true;

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.CopperWire, 1),
                new Ingredient(TechType.Glass, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.CopperWire, 1),
                new Ingredient(TechType.Glass, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                posterMagnetObj = AssetsHelper.Assets.LoadAsset<GameObject>("poster_decorations");
                normal = AssetsHelper.Assets.LoadAsset<Texture>("poster_magnet_normal");
                illum = AssetsHelper.Assets.LoadAsset<Texture>("poster_magnet_illum");

                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Add new TechType to the buildables
                Nautilus.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                Nautilus.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType, TechType.PictureFrame);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("revertpictureframe"));

                // Patch with harmony
                MyHarmony.PatchPictureFrames();

                this.IsRegistered = true;
            }
        }

        private static GameObject _customPictureFrame = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: customPictureFrame.GetGameObject()");
#endif
            if (_customPictureFrame == null)
                _customPictureFrame = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/PictureFrame.prefab");

            // Instantiate prefabs
            GameObject prefab = GameObject.Instantiate(_customPictureFrame);
            GameObject posterPrefab = GameObject.Instantiate(this.posterMagnetObj);

            // Get objects
            GameObject posterRootModel = posterPrefab.FindChild("model");
            GameObject magnetModel = posterRootModel.FindChild("poster_kitty");
            MeshRenderer magnetRenderer = magnetModel.GetComponent<MeshRenderer>();
            GameObject posterBgModel = posterRootModel.FindChild("poster_background");
            MeshRenderer posterBgRenderer = posterBgModel.GetComponent<MeshRenderer>();
            GameObject posterBgBisModel = posterRootModel.FindChild("poster_background_bis");
            MeshRenderer posterBgBisRenderer = posterBgBisModel.GetComponent<MeshRenderer>();
            GameObject posterBgPivotModel = posterRootModel.FindChild("poster_background_pivot");
            MeshRenderer posterBgPivotRenderer = posterBgPivotModel.GetComponent<MeshRenderer>();
            Shader marmosetUber = Shader.Find("MarmosetUBER");

            // Update poster border shader, normal/emission maps, hide parts of the prefab
            if (magnetRenderer != null && magnetRenderer.materials != null)
            {
                foreach (Material tmpMat in magnetRenderer.materials)
                {
                    tmpMat.shader = marmosetUber;
                    if (string.Compare(tmpMat.name, "poster_magnet (Instance)", true, CultureInfo.InvariantCulture) == 0)
                    {
                        tmpMat.SetTexture("_BumpMap", normal);
                        tmpMat.SetTexture("_Illum", illum);
                        tmpMat.EnableKeyword("MARMO_NORMALMAP"); // Enable normal map
                        tmpMat.EnableKeyword("MARMO_EMISSION"); // Enable emission map
                        tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                    }
                }
            }
            if (posterBgBisRenderer != null && posterBgBisRenderer.materials != null)
                foreach (Material tmpMat in posterBgBisRenderer.materials)
                    tmpMat.shader = marmosetUber;
            if (posterBgPivotRenderer != null && posterBgPivotRenderer.materials != null)
                foreach (Material tmpMat in posterBgPivotRenderer.materials)
                    tmpMat.shader = marmosetUber;

            // Adjust poster bg position and scale
            posterBgPivotModel.transform.localPosition = new Vector3(posterBgPivotModel.transform.localPosition.x - 0.0101f, posterBgPivotModel.transform.localPosition.y - 0.00053f, posterBgPivotModel.transform.localPosition.z);
            posterBgPivotModel.transform.localScale = new Vector3(posterBgPivotModel.transform.localScale.x * 0.741f, posterBgPivotModel.transform.localScale.y, posterBgPivotModel.transform.localScale.z * 1.396f);
            posterBgBisModel.transform.localPosition = new Vector3(posterBgBisModel.transform.localPosition.x - 0.00005f, posterBgBisModel.transform.localPosition.y + 0.0006f, posterBgBisModel.transform.localPosition.z);
            posterBgBisModel.transform.localScale = new Vector3(posterBgBisModel.transform.localScale.x, posterBgBisModel.transform.localScale.y, posterBgBisModel.transform.localScale.z * 1.035f);

            // Set poster relative position
            posterPrefab.transform.parent = prefab.transform;
            posterPrefab.transform.localPosition = new Vector3(-0.002f, 0.289f, 0f);
            posterPrefab.transform.localScale = new Vector3(22.5f, 30.5f, 30.5f);
            posterPrefab.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            posterPrefab.SetActive(true);
            magnetRenderer.enabled = false;
            posterBgRenderer.enabled = false;
            posterBgBisRenderer.enabled = false;
            posterBgPivotRenderer.enabled = false;

            // Get prefab sub models
            GameObject model = prefab.FindChild("mesh");
            GameObject screen = prefab.FindChild("Screen");
            GameObject trigger = prefab.FindChild("Trigger");

            // Adjust position from wall (except for poster)
            foreach (Transform tr in prefab.transform)
                if (!tr.name.StartsWith("poster_decorations", true, CultureInfo.InvariantCulture))
                    tr.localPosition = new Vector3(tr.localPosition.x, tr.localPosition.y, tr.localPosition.z + 0.005f);

            // Update prefab name
            prefab.name = this.ClassID;

            // Modify tech tag
            var techTag = prefab.GetComponent<TechTag>();
            techTag.type = this.TechType;

            // Modify prefab identifier
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Rotate model
            model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y, model.transform.localEulerAngles.z + 90.0f);
            
            // Adjust frame position
            model = prefab.FindChild("mesh");
            model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y, model.transform.localPosition.z + 0.0055f);

            // Update box collider size
            BoxCollider collider = trigger.GetComponent<BoxCollider>();
            collider.size = new Vector3(collider.size.x - 0.5f, collider.size.y - 0.5f, collider.size.z);

            // Rotate box collider
            collider.size = new Vector3(collider.size.y, collider.size.x, collider.size.z);

            // Translate collider
            collider.center = new Vector3(collider.center.x + 0.15f, collider.center.y, collider.center.z);

            // Update sky appliers
            PrefabsHelper.ReplaceSkyApplier(prefab, "mesh");
            
            // Update contructable
            var constructible = prefab.GetComponent<Constructable>();
            constructible.techType = this.TechType;
            constructible.placeMinDistance = 0.4f;

            PictureFrame pf = prefab.GetComponent<PictureFrame>();

            // Rotate PictureFrame
            pf.imageRenderer.transform.localScale = new Vector3(pf.imageRenderer.transform.localScale.y, pf.imageRenderer.transform.localScale.x, pf.imageRenderer.transform.localScale.z);
            
            // Move PictureFrame
            pf.imageRenderer.transform.localPosition = new Vector3(pf.imageRenderer.transform.localPosition.x, pf.imageRenderer.transform.localPosition.y, pf.imageRenderer.transform.localPosition.z + 0.0045f);

            // Update constructable bounds
            var constructableBounds = prefab.GetComponent<ConstructableBounds>();
            constructableBounds.bounds.extents = new Vector3(constructableBounds.bounds.extents.x * 0.7f, constructableBounds.bounds.extents.y * 0.4f, constructableBounds.bounds.extents.z);
            
            // Rotate constructable bounds
            constructableBounds.bounds.extents = new Vector3(constructableBounds.bounds.extents.y, constructableBounds.bounds.extents.x, constructableBounds.bounds.extents.z);

            // Adjust frame scale
            foreach (Transform tr in prefab.transform)
            {
                if (tr.name.StartsWith("mesh", true, CultureInfo.InvariantCulture))
                {
                    tr.localScale = new Vector3(tr.localScale.x, tr.localScale.y, tr.localScale.z + 0.002f);
                    break;
                }
            }

            // Get origin frame scale and angles
            Vector3 originFrameScale = Vector3.zero;
            Vector3 originFramePosition = Vector3.zero;
            Vector3 originFrameEulerAngles = Vector3.zero;
            foreach (Transform tr in prefab.transform)
            {
                if (tr.name.StartsWith("mesh", true, CultureInfo.InvariantCulture))
                {
                    originFrameScale = tr.localScale;
                    originFramePosition = tr.localPosition;
                    originFrameEulerAngles = tr.localEulerAngles;
                    break;
                }
            }
            // Get origin poster position, origin poster model position and scale, and origin magnet scale
            Vector3 originPosterPosition = Vector3.zero;
            Vector3 originPosterModelPosition = Vector3.zero;
            Vector3 originPosterModelScale = Vector3.zero;
            Vector3 originMagnetScale = Vector3.zero;
            foreach (Transform tr in prefab.transform)
            {
                if (tr.name.StartsWith("poster_decorations", true, CultureInfo.InvariantCulture))
                {
                    originPosterPosition = tr.localPosition;
                    foreach (Transform ch in tr)
                    {
                        if (ch.name.StartsWith("model", true, CultureInfo.InvariantCulture))
                        {
                            originPosterModelPosition = ch.localPosition;
                            originPosterModelScale = ch.localScale;
                            foreach (Transform chb in ch)
                            {
                                if (chb.name.StartsWith("poster_kitty", true, CultureInfo.InvariantCulture))
                                {
                                    originMagnetScale = chb.localScale;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
            // Get origin image scale, origin collider size and origin constructable bounds extents
            Vector3 originImageScale = pf.imageRenderer.transform.localScale;
            Vector3 originColliderSize = collider.size;
            Vector3 originConstructableBoundsExtents = constructableBounds.bounds.extents;

            // Add CustomPictureFrame controller
            CustomPictureFrameController cpfController = prefab.AddComponent<CustomPictureFrameController>();
            // Store origin values inside CPF controller
            cpfController.OriginFrameScale = originFrameScale;
            cpfController.OriginFramePosition = originFramePosition;
            cpfController.OriginFrameEulerAngles = originFrameEulerAngles;
            cpfController.OriginImageScale = originImageScale;
            cpfController.OriginColliderSize = originColliderSize;
            cpfController.OriginConstructableBoundsExtents = originConstructableBoundsExtents;
            cpfController.OriginPosterPosition = originPosterPosition;
            cpfController.OriginPosterModelPosition = originPosterModelPosition;
            cpfController.OriginPosterModelScale = originPosterModelScale;
            cpfController.OriginMagnetScale = originMagnetScale;

            // Adjust PictureFrame rendering distance (displays full image quality if player is closer than 20m)
            pf.distance = 20.0f;

            return prefab;
        }
    }
}
