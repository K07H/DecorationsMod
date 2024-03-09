#if SUBNAUTICA_NAUTILUS
using System.Diagnostics.CodeAnalysis;
using Nautilus.Assets;
using Nautilus.Crafting;
using Nautilus.Handlers;
using static CraftData;
#else
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
#endif
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class PenHolder : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS

        [SetsRequiredMembers]
        public PenHolder() : base("PenHolder", "PenHolderName", "PenHolderDescription", "penholdericon")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public PenHolder()
        {
            this.ClassID = "PenHolder";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("docking_clerical_penholder");
            this.GameObject = new GameObject(this.ClassID);
#endif

#if SUBNAUTICA
            this.Sprite = AssetsHelper.Assets.LoadAsset<Sprite>("penholdericon");
#else
            this.Sprite = AssetsHelper.Assets.LoadAsset<Sprite>("penholdericon");
#endif

#if !SUBNAUTICA_NAUTILUS
            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("PenHolderName"),
                                                        LanguageHelper.GetFriendlyWord("PenHolderDescription"),
                                                        this.Sprite,
                                                        true);
#endif

#if SUBNAUTICA
#if SUBNAUTICA_NAUTILUS
            this.Recipe = new RecipeData()
#else
            this.Recipe = new TechData()
#endif
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(TechType.Titanium, 1)
                    }),
            };
#else
            this.Recipe = new RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(TechType.Titanium, 1)
                    }),
            };
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
                PrefabsHelper.SetDefaultSkyApplier(_penHolder, renderers);

                // We can pick this item
                PrefabsHelper.SetDefaultPickupable(_penHolder);

                // We can place this item
                PrefabsHelper.SetDefaultPlaceTool(_penHolder, collider);

                // Associate recipe to the new TechType
#if SUBNAUTICA_NAUTILUS
                CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);
#else
                CraftDataHandler.SetTechData(this.TechType, this.Recipe);
#endif

                // Add the new TechType to Hand Equipment type.
                CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

#if SUBNAUTICA_NAUTILUS
                // Set the buildable prefab
                this.Register();
#else
                // Set the custom sprite
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("penholdericon"));

                // Set the buildable prefab
                PrefabHandler.RegisterPrefab(this);
#endif

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
