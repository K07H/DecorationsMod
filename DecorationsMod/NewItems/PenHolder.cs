using SMLHelper.V2.Crafting;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class PenHolder : DecorationItem
    {
        public PenHolder()
        {
            this.ClassID = "PenHolder";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("docking_clerical_penholder");

#if BELOWZERO
            this.Sprite = AssetsHelper.Assets.LoadAsset<Sprite>("penholdericon");
#else
            this.Sprite = AssetsHelper.Assets.LoadAsset<Sprite>("penholdericon");
#endif

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("PenHolderName"),
                                                        LanguageHelper.GetFriendlyWord("PenHolderDescription"),
                                                        this.Sprite,
                                                        true);

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(TechType.Titanium, 1)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 1)
                    }),
            };
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Get model
                GameObject model = this.GameObject.FindChild("docking_clerical_penholder");

                // Scale
                model.transform.localScale *= 4f;
                // Translate
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.001f, model.transform.localPosition.z);
                // Rotate
                //model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y + -25.0f, model.transform.localEulerAngles.z);

                // Set tech tag
                var techTag = this.GameObject.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add prefab identifier
                var pi = this.GameObject.AddComponent<PrefabIdentifier>();
                pi.ClassId = this.ClassID;

                // Add collider
                var collider = this.GameObject.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.1f, 0.1f, 0.1f);
                collider.contactOffset = 0.01f;
                //collider.center = new Vector3(collider.center.x - 0.15f, collider.center.y + 0.1f, collider.center.z);
                collider.enabled = true;

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal = AssetsHelper.Assets.LoadAsset<Texture>("docking_clerical_01_normal");
                var renderers = this.GameObject.GetComponentsInChildren<Renderer>();
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
                PrefabsHelper.SetDefaultLargeWorldEntity(this.GameObject);

                // Add rigid body
                PrefabsHelper.SetDefaultRigidBody(this.GameObject);
                Rigidbody rb = this.GameObject.GetComponent<Rigidbody>();
                rb.mass = 0.4f;

                // Add sky applier
                PrefabsHelper.SetDefaultSkyApplier(this.GameObject, renderers);

                // We can pick this item
                PrefabsHelper.SetDefaultPickupable(this.GameObject);

                // We can place this item
                PrefabsHelper.SetDefaultPlaceTool(this.GameObject, collider);

                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                // Add the new TechType to Hand Equipment type.
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("penholdericon"));

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

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
