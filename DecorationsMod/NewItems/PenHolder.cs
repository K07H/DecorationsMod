using Nautilus.Crafting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class PenHolder : DecorationItem
    {
        public static Sprite _icon = AssetsHelper.Assets.LoadAsset<Sprite>("penholdericon");

        [SetsRequiredMembers]
        public PenHolder() : base("PenHolder", LanguageHelper.GetFriendlyWord("PenHolderName"), LanguageHelper.GetFriendlyWord("PenHolderDescription"), _icon)
        {
            this.ClassID = "PenHolder";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("docking_clerical_penholder");
            this.GameObject = new GameObject(this.ClassID);

            this.Sprite = _icon;
            this.TechType = this.Info.TechType;

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        private static GameObject _penHolder = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_penHolder == null)
                    _penHolder = AssetsHelper.Assets.LoadAsset<GameObject>("docking_clerical_penholder");

                // Get model
                GameObject model = _penHolder.FindChild("docking_clerical_penholder");

                // Scale
                model.transform.localScale *= 4f;
                // Translate
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.001f, model.transform.localPosition.z);
                // Rotate
                //model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y + -25.0f, model.transform.localEulerAngles.z);

                // Set tech tag
                var techTag = _penHolder.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add prefab identifier
                var pi = _penHolder.AddComponent<PrefabIdentifier>();
                pi.ClassId = this.ClassID;

                // Add collider
                var collider = _penHolder.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.1f, 0.1f, 0.1f);
                collider.contactOffset = 0.01f;
                //collider.center = new Vector3(collider.center.x - 0.15f, collider.center.y + 0.1f, collider.center.z);
                collider.enabled = true;

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal = AssetsHelper.Assets.LoadAsset<Texture>("docking_clerical_01_normal");
                var renderers = _penHolder.GetComponentsInChildren<Renderer>();
                foreach (Renderer rend in renderers)
                {
                    foreach (Material tmpMat in rend.materials)
                    {
                        tmpMat.shader = marmosetUber;
                        if (string.Compare(tmpMat.name, "docking_clerical_01 (Instance)", true, CultureInfo.InvariantCulture) == 0)
                        {
                            tmpMat.SetTexture("_BumpMap", normal);
                            tmpMat.EnableKeyword("MARMO_NORMALMAP");
                            tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                        }
                    }
                }

                // Add large world entity
                PrefabsHelper.SetDefaultLargeWorldEntity(_penHolder);

                // Add rigid body
                PrefabsHelper.SetDefaultRigidBody(_penHolder);
                Rigidbody rb = _penHolder.GetComponent<Rigidbody>();
                rb.mass = 0.4f;

                // Add sky applier
                PrefabsHelper.UpdateOrAddSkyApplier(_penHolder, null, renderers);

                // We can pick this item
                PrefabsHelper.SetDefaultPickupable(_penHolder);

                // We can place this item
                PrefabsHelper.SetDefaultPlaceTool(_penHolder, collider);

                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Add the new TechType to Hand Equipment type.
                Nautilus.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("penholdericon"));

                // Set the buildable prefab
                this.Register();

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_penHolder);

            prefab.name = this.ClassID;

            // Add fabricating animation
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.2f;
            fabricatingA.localMaxY = 0.6f;
            fabricatingA.posOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.scaleFactor = 1f;

            return prefab;
        }

    }
}
