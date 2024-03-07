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
using DecorationsMod.Controllers;
using HarmonyLib;
using mset;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace DecorationsMod.NewItems
{
    public class AquariumSmall : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public AquariumSmall() : base("AquariumSmall", "AquariumSmallName", "AquariumSmallDescription", "tubularaquarium3icon")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public AquariumSmall() // Feeds abstract class
        {
            this.ClassID = "AquariumSmall";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("AquariumSmallName"),
                                                        LanguageHelper.GetFriendlyWord("AquariumSmallDescription"),
                                                        true);
#endif

            this.IsHabitatBuilder = true;

#if SUBNAUTICA && !SUBNAUTICA_NAUTILUS
            this.Recipe = new TechData()
#else
            this.Recipe = new RecipeData()
#endif
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[2]
                    {
                        new Ingredient(TechType.Titanium, 1),
                        new Ingredient(TechType.Glass, 1)
                    }),
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Add new TechType to the buildables
                CraftDataHandler.AddBuildable(this.TechType);
                CraftDataHandler.AddToGroup(TechGroup.InteriorModules, TechCategory.InteriorModule, this.TechType, TechType.Aquarium);

                // Set the buildable prefab
#if SUBNAUTICA_NAUTILUS
                this.Register();
#else
                PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("tubularaquarium3icon"));
#endif

                // Associate recipe to the new TechType
#if SUBNAUTICA_NAUTILUS
                CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);
#else
                CraftDataHandler.SetTechData(this.TechType, this.Recipe);
#endif

                this.IsRegistered = true;
            }
        }

        private static GameObject _aquariumSmall = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_AQUARIUM
            Logger.Debug("A0");
#endif
            if (_aquariumSmall == null)
                _aquariumSmall = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/Aquarium.prefab");

#if SUBNAUTICA
            GameObject greenCoral = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Coral_reef/coral_reef_small_deco_13.prefab");
            GameObject blueCoral = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Coral_reef/Coral_reef_blue_coral_tubes.prefab");
            GameObject brownCoral = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Coral_reef/coral_reef_brown_coral_tubes_02_01.prefab");
            GameObject brownCoral1 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Coral_reef/coral_reef_brown_coral_tubes_02_03.prefab");
#else
            GameObject greenCoral = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Flora/Shared/coral_reef_small_deco_13.prefab");
            GameObject blueCoral = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Flora/Shared/Coral_reef_blue_coral_tubes.prefab");
            GameObject brownCoral = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Flora/Shared/coral_reef_brown_coral_tubes_02_01.prefab");
            GameObject brownCoral1 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Flora/Shared/coral_reef_brown_coral_tubes_02_03.prefab");
#endif

            // Instantiate root prefab
            GameObject prefab = GameObject.Instantiate(_aquariumSmall);
            prefab.name = this.ClassID;

            // Get sub objects
            GameObject model = prefab.FindChild("model");
            GameObject bubbles = prefab.FindChild("Bubbles");
            GameObject xBubbles = bubbles.FindChild("xBubbles");
            GameObject storage = prefab.FindChild("StorageRoot");
            GameObject collider = prefab.FindChild("Collider");
            GameObject coral = model.FindChild("Coral");
            GameObject coral1 = coral.FindChild("coral_reef_grass_11");
            GameObject coral2 = coral.FindChild("coral_reef_grass_10");
            GameObject coral3 = coral.FindChild("coral_reef_grass_09");
            GameObject coral4 = coral.FindChild("coral_reef_grass_07");
            GameObject coral5 = coral.FindChild("coral_reef_plant_middle_03");
            GameObject coral6 = coral.FindChild("coral_reef_small_deco_15_red");
            GameObject coral7 = coral.FindChild("coral_reef_grass_03_04");
            GameObject coral8 = coral.FindChild("coral_reef_small_deco_14");
            GameObject coral9 = coral.FindChild("coral_reef_grass_03_03");
            GameObject coral10 = coral.FindChild("coral_reef_grass_03_02");
            GameObject coral11 = coral.FindChild("Coral_reef_purple_fan");
            GameObject aquariumAnim2 = model.FindChild("Aquarium_animation2");
            GameObject aquariumAnim2Root = aquariumAnim2.FindChild("root");
            GameObject aquariumAnim2Geo = aquariumAnim2.FindChild("Aquarium_geo");
            GameObject aquariumAnim2GeoAquarium = aquariumAnim2Geo.FindChild("Aquarium");
            GameObject aquariumAnim2GeoAquariumGlass = aquariumAnim2Geo.FindChild("Aquarium_glass");
            GameObject aquariumAnim2Fish1 = aquariumAnim2Root.FindChild("fish1");
            GameObject aquariumAnim2Fish2 = aquariumAnim2Root.FindChild("fish2");
            GameObject aquariumAnim2Fish3 = aquariumAnim2Root.FindChild("fish3");
            GameObject aquariumAnim2Fish4 = aquariumAnim2Root.FindChild("fish4");
            GameObject aquariumAnim2Attach4 = aquariumAnim2Fish4.FindChild("fish_attach4");
            GameObject aquariumAnim1 = model.FindChild("Aquarium_animation");
            GameObject aquariumAnim1Root = aquariumAnim1.FindChild("root");
            GameObject aquariumAnim1Geo = aquariumAnim1.FindChild("Aquarium_geo");
            GameObject aquariumAnim1GeoAquarium = aquariumAnim1Geo.FindChild("Aquarium");
            GameObject aquariumAnim1GeoAquariumGlass = aquariumAnim1Geo.FindChild("Aquarium_glass");
            GameObject aquariumAnim1Fish1 = aquariumAnim1Root.FindChild("fish1");
            GameObject aquariumAnim1Fish2 = aquariumAnim1Root.FindChild("fish2");
            GameObject aquariumAnim1Fish3 = aquariumAnim1Root.FindChild("fish3");
            GameObject aquariumAnim1Fish4 = aquariumAnim1Root.FindChild("fish4");

#if DEBUG_AQUARIUM
            Logger.Debug("A1");
#endif
            // Setup green coral
            if (greenCoral != null)
            {
                GameObject iGreenCoral = GameObject.Instantiate(greenCoral);
                iGreenCoral.name = "SmallDeco13";
                iGreenCoral.transform.parent = coral.transform;
                iGreenCoral.transform.localScale *= 0.179f;
                iGreenCoral.transform.localPosition = new Vector3(iGreenCoral.transform.localPosition.x, iGreenCoral.transform.localPosition.y + 0.17f, iGreenCoral.transform.localPosition.z - 0.635f);
                GameObject.DestroyImmediate(iGreenCoral.GetComponent<LargeWorldEntity>());
                GameObject.DestroyImmediate(iGreenCoral.GetComponent<PrefabIdentifier>());
            }

#if DEBUG_AQUARIUM
            Logger.Debug("A2");
#endif
            // Setup blue coral
            if (blueCoral != null)
            {
                coral9.GetComponent<MeshRenderer>().enabled = false;

                GameObject iBlueCoral = GameObject.Instantiate(blueCoral);
                iBlueCoral.name = "BlueCoralTubes1";
                iBlueCoral.transform.parent = coral.transform;
                iBlueCoral.transform.localPosition = Vector3.zero;
                iBlueCoral.transform.localScale *= 0.31f;
                iBlueCoral.transform.localPosition = new Vector3(iBlueCoral.transform.localPosition.x + 0.22f, iBlueCoral.transform.localPosition.y + 0.16f, iBlueCoral.transform.localPosition.z - 0.77f);
                var lwe = iBlueCoral.GetComponent<LargeWorldEntity>();
                if (lwe != null)
                    GameObject.DestroyImmediate(lwe);
                var lmi = iBlueCoral.GetComponent<LiveMixin>();
                if (lmi != null)
                    GameObject.DestroyImmediate(lmi);
                var pid = iBlueCoral.GetComponent<PrefabIdentifier>();
                if (pid != null)
                    GameObject.DestroyImmediate(pid);

                var blueCoralModel = iBlueCoral.FindChild("Coral_reef_blue_coral_tubes");
                var blueCoralAnim = blueCoralModel.GetComponent<Animator>();
                var blueCoralBase = blueCoralModel.FindChild("base1");
                var blueCoralGeos = blueCoralModel.FindChild("geos");

                LODGroup lodBlueCoral = iBlueCoral.GetComponent<LODGroup>();
                lodBlueCoral.ForceLOD(0);
                LOD[] lods = lodBlueCoral.GetLODs();
                lodBlueCoral.SetLODs(new LOD[] { lods[0] });
                lodBlueCoral.ForceLOD(0);
                lodBlueCoral.size = 3.0f;
            }

#if DEBUG_AQUARIUM
            Logger.Debug("A3");
#endif
            // Setup brown coral 2
            if (brownCoral != null)
            {
                GameObject iBrownCoral = GameObject.Instantiate(brownCoral);
                iBrownCoral.name = "BrownCoralTubes2";
                iBrownCoral.transform.parent = coral.transform;
                iBrownCoral.transform.localPosition = Vector3.zero;
                iBrownCoral.transform.localScale *= 0.3f;
                iBrownCoral.transform.localPosition = new Vector3(iBrownCoral.transform.localPosition.x + 0.249f, iBrownCoral.transform.localPosition.y + 0.17f, iBrownCoral.transform.localPosition.z - 0.26f);
                iBrownCoral.transform.localEulerAngles = new Vector3(iBrownCoral.transform.localEulerAngles.x, iBrownCoral.transform.localEulerAngles.y + 15f, iBrownCoral.transform.localEulerAngles.z);
                var lwe = iBrownCoral.GetComponent<LargeWorldEntity>();
                if (lwe != null)
                    GameObject.DestroyImmediate(lwe);
                var rbo = iBrownCoral.GetComponent<Rigidbody>();
                if (rbo != null)
                    GameObject.DestroyImmediate(rbo);
                var lmi = iBrownCoral.GetComponent<LiveMixin>();
                if (lmi != null)
                    GameObject.DestroyImmediate(lmi);
                var pid = iBrownCoral.GetComponent<PrefabIdentifier>();
                if (pid != null)
                    GameObject.DestroyImmediate(pid);
                var bco = iBrownCoral.GetComponentInChildren<BoxCollider>();
                if (bco != null)
                    GameObject.DestroyImmediate(bco);
                // Scale models
                iBrownCoral.FindChild("coral_reef_brown_coral_tubes_02_01").transform.localScale *= 0.4f;
                iBrownCoral.FindChild("coral_reef_brown_coral_tubes_02_01_LOD3").transform.localScale *= 0.4f;
                var lodBrownCoral = iBrownCoral.GetComponent<LODGroup>();
                lodBrownCoral.ForceLOD(0);
                LOD[] lods = lodBrownCoral.GetLODs();
                lodBrownCoral.SetLODs(new LOD[] { lods[0] });
                lodBrownCoral.ForceLOD(0);
                lodBrownCoral.size = 3.0f;
            }

#if DEBUG_AQUARIUM
            Logger.Debug("A4");
#endif
            // Setup brown coral 2 bis
            if (brownCoral != null)
            {
                GameObject iBrownCoralB = GameObject.Instantiate(brownCoral);
                iBrownCoralB.name = "BrownCoralTubes2";
                iBrownCoralB.transform.parent = coral.transform;
                iBrownCoralB.transform.localPosition = Vector3.zero;
                iBrownCoralB.transform.localScale *= 0.3f;
                iBrownCoralB.transform.localPosition = new Vector3(iBrownCoralB.transform.localPosition.x - 0.18f, iBrownCoralB.transform.localPosition.y + 0.055f, iBrownCoralB.transform.localPosition.z - 0.115f);
                iBrownCoralB.transform.localEulerAngles = new Vector3(iBrownCoralB.transform.localEulerAngles.x, iBrownCoralB.transform.localEulerAngles.y - 105.0f, iBrownCoralB.transform.localEulerAngles.z + 15.0f);
                var lwe = iBrownCoralB.GetComponent<LargeWorldEntity>();
                if (lwe != null)
                    GameObject.DestroyImmediate(lwe);
                var rbo = iBrownCoralB.GetComponent<Rigidbody>();
                if (rbo != null)
                    GameObject.DestroyImmediate(rbo);
                var lmi = iBrownCoralB.GetComponent<LiveMixin>();
                if (lmi != null)
                    GameObject.DestroyImmediate(lmi);
                var pid = iBrownCoralB.GetComponent<PrefabIdentifier>();
                if (pid != null)
                    GameObject.DestroyImmediate(pid);
                var bco = iBrownCoralB.GetComponentInChildren<BoxCollider>();
                if (bco != null)
                    GameObject.DestroyImmediate(bco);
                // Scale models
                iBrownCoralB.FindChild("coral_reef_brown_coral_tubes_02_01").transform.localScale *= 0.4f;
                iBrownCoralB.FindChild("coral_reef_brown_coral_tubes_02_01_LOD3").transform.localScale *= 0.4f;

                var lodBrownCoral = iBrownCoralB.GetComponent<LODGroup>();
                lodBrownCoral.ForceLOD(0);
                LOD[] lods = lodBrownCoral.GetLODs();
                lodBrownCoral.SetLODs(new LOD[] { lods[0] });
                lodBrownCoral.ForceLOD(0);
                lodBrownCoral.size = 3.0f;

                iBrownCoralB.transform.localScale *= 1.4f;
            }

#if DEBUG_AQUARIUM
            Logger.Debug("A5");
#endif
            // Setup brown coral 1
            if (brownCoral1 != null)
            {
                GameObject iBrownCoral1 = GameObject.Instantiate(brownCoral1);
                iBrownCoral1.name = "BrownCoralTubes2";
                iBrownCoral1.transform.parent = coral.transform;
                iBrownCoral1.transform.localPosition = Vector3.zero;
                iBrownCoral1.transform.localScale *= 0.4f;
                iBrownCoral1.transform.localPosition = new Vector3(iBrownCoral1.transform.localPosition.x - 0.08f, iBrownCoral1.transform.localPosition.y + 0.055f, iBrownCoral1.transform.localPosition.z - 0.1144f);
                iBrownCoral1.transform.localEulerAngles = new Vector3(iBrownCoral1.transform.localEulerAngles.x, iBrownCoral1.transform.localEulerAngles.y - 30.0f, iBrownCoral1.transform.localEulerAngles.z);
                var lwe = iBrownCoral1.GetComponent<LargeWorldEntity>();
                if (lwe != null)
                    GameObject.DestroyImmediate(lwe);
                var rbo = iBrownCoral1.GetComponent<Rigidbody>();
                if (rbo != null)
                    GameObject.DestroyImmediate(rbo);
                var lmi = iBrownCoral1.GetComponent<LiveMixin>();
                if (lmi != null)
                    GameObject.DestroyImmediate(lmi);
                var pid = iBrownCoral1.GetComponent<PrefabIdentifier>();
                if (pid != null)
                    GameObject.DestroyImmediate(pid);
                var bco = iBrownCoral1.GetComponentInChildren<BoxCollider>();
                if (bco != null)
                    GameObject.DestroyImmediate(bco);
                // Scale models
                iBrownCoral1.FindChild("coral_reef_brown_coral_tubes_02_03").transform.localScale *= 0.4f;
                iBrownCoral1.FindChild("coral_reef_brown_coral_tubes_02_03_LOD3").transform.localScale *= 0.4f;
                // Adjust LOD
                var lodBrownCoral = iBrownCoral1.GetComponent<LODGroup>();
                if (lodBrownCoral != null)
                {
                    lodBrownCoral.ForceLOD(0);
                    LOD[] lods = lodBrownCoral.GetLODs();
                    lodBrownCoral.SetLODs(new LOD[] { lods[0] });
                    lodBrownCoral.ForceLOD(0);
                    lodBrownCoral.size = 3.0f;
                }
            }

#if DEBUG_AQUARIUM
            Logger.Debug("A6");
#endif
            // Adjust corals
            coral1.SetActive(false); // petite fougere jaune (gauche)
            coral2.SetActive(false); // grande fougere jaune 1 (droite)
            coral3.SetActive(false); // grande fougere jaune 2 (droite)
            coral4.SetActive(false);  // grande fougere jaune 3 (droite)
            coral5.SetActive(false); // grande plante rose (centre gauche)
            coral6.SetActive(false); // corail rouge foncé a petit points blanc (droite)
            coral7.SetActive(false); // plante rose (droite)
            coral8.SetActive(false); // corail bleu foncé a pointes en spirales et bout rouge (gauche)
            coral9.SetActive(false); // plante rose (centre gauche)
            coral10.SetActive(true); // plante rose (centre gauche)
            coral11.SetActive(true); // algue en courone bleu/violet foncée (centre gauche)
            coral.transform.localScale *= 0.6f;
            coral10.transform.localScale *= 0.5f;
            coral10.transform.localPosition = new Vector3(coral10.transform.localPosition.x + 0.10f, coral10.transform.localPosition.y - 0.17f, coral10.transform.localPosition.z - 0.43f);
            coral11.transform.localPosition = new Vector3(coral11.transform.localPosition.x - 0.2f, coral11.transform.localPosition.y - 0.23f, coral11.transform.localPosition.z);

#if DEBUG_AQUARIUM
            Logger.Debug("A7");
#endif
            // Adjust aquarium
            aquariumAnim2Geo.transform.localScale = new Vector3(aquariumAnim2Geo.transform.localScale.x * (1.0f / 0.239f), aquariumAnim2Geo.transform.localScale.y * (1.0f / 0.24f), aquariumAnim2Geo.transform.localScale.z * (1.0f / 0.239f));
            aquariumAnim1Geo.transform.localScale = new Vector3(aquariumAnim1Geo.transform.localScale.x * (1.0f / 0.239f), aquariumAnim1Geo.transform.localScale.y * (1.0f / 0.24f), aquariumAnim1Geo.transform.localScale.z * (1.0f / 0.239f));
            aquariumAnim2Geo.transform.localPosition = new Vector3(aquariumAnim2Geo.transform.localPosition.x, aquariumAnim2Geo.transform.localPosition.y - (0.20f * (1.0f / 0.24f)), aquariumAnim2Geo.transform.localPosition.z);
            aquariumAnim1Geo.transform.localPosition = new Vector3(aquariumAnim1Geo.transform.localPosition.x, aquariumAnim1Geo.transform.localPosition.y - (0.20f * (1.0f / 0.24f)), aquariumAnim1Geo.transform.localPosition.z);

            aquariumAnim2Geo.transform.localScale = new Vector3(aquariumAnim2Geo.transform.localScale.x * 0.50f, aquariumAnim2Geo.transform.localScale.y * 0.285f, aquariumAnim2Geo.transform.localScale.z * 0.16f);
            aquariumAnim1Geo.transform.localScale = new Vector3(aquariumAnim1Geo.transform.localScale.x * 0.50f, aquariumAnim1Geo.transform.localScale.y * 0.285f, aquariumAnim1Geo.transform.localScale.z * 0.16f);
            aquariumAnim2Geo.transform.localPosition = new Vector3(aquariumAnim2Geo.transform.localPosition.x, aquariumAnim2Geo.transform.localPosition.y, aquariumAnim2Geo.transform.localPosition.z + (0.145f * (1.0f / 0.239f)));
            aquariumAnim1Geo.transform.localPosition = new Vector3(aquariumAnim1Geo.transform.localPosition.x, aquariumAnim1Geo.transform.localPosition.y, aquariumAnim1Geo.transform.localPosition.z + (0.145f * (1.0f / 0.239f)));

            aquariumAnim2GeoAquarium.transform.localPosition = new Vector3(aquariumAnim2GeoAquarium.transform.localPosition.x, aquariumAnim2GeoAquarium.transform.localPosition.y, aquariumAnim2GeoAquarium.transform.localPosition.z - ((0.145f * 2.0f) * (1.0f / 0.16f)));
            aquariumAnim1GeoAquarium.transform.localPosition = new Vector3(aquariumAnim1GeoAquarium.transform.localPosition.x, aquariumAnim1GeoAquarium.transform.localPosition.y, aquariumAnim1GeoAquarium.transform.localPosition.z - ((0.145f * 2.0f) * (1.0f / 0.16f)));

#if DEBUG_AQUARIUM
            Logger.Debug("A8");
#endif
            // Adjust fish
            aquariumAnim2Attach4.transform.localScale = new Vector3(aquariumAnim2Attach4.transform.localScale.x * 2.2f, aquariumAnim2Attach4.transform.localScale.y * 2.2f, aquariumAnim2Attach4.transform.localScale.z * 2.2f);

#if DEBUG_AQUARIUM
            Logger.Debug("A9");
#endif
            // Adjust animators
            Animator anim = aquariumAnim1.GetComponent<Animator>();
            anim.transform.localScale = new Vector3(anim.transform.localScale.x * 0.239f, anim.transform.localScale.y * 0.24f, anim.transform.localScale.z * 0.239f);
            anim.transform.localPosition = new Vector3(anim.transform.localPosition.x + 0.145f, anim.transform.localPosition.y + 0.20f, anim.transform.localPosition.z);
            Animator anim2 = aquariumAnim2.GetComponent<Animator>();
            anim2.transform.localScale = new Vector3(anim2.transform.localScale.x * 0.239f, anim2.transform.localScale.y * 0.24f, anim2.transform.localScale.z * 0.239f);
            anim2.transform.localPosition = new Vector3(anim2.transform.localPosition.x + 0.145f, anim2.transform.localPosition.y + 0.20f, anim2.transform.localPosition.z);

#if DEBUG_AQUARIUM
            Logger.Debug("A10");
#endif
            // Adjust bubbles
            bubbles.transform.localScale = new Vector3(bubbles.transform.localScale.x * 0.07f, bubbles.transform.localScale.y * 0.07f, bubbles.transform.localScale.z * 0.07f);
            xBubbles.transform.localScale = new Vector3(xBubbles.transform.localScale.x * 0.07f, xBubbles.transform.localScale.y * 0.07f, xBubbles.transform.localScale.z * 0.07f);
            xBubbles.transform.localPosition = new Vector3(xBubbles.transform.localPosition.x + 4.0f, xBubbles.transform.localPosition.y + 1.0f, xBubbles.transform.localPosition.z);
            var ps = xBubbles.GetComponent<ParticleSystem>();
            var psr = xBubbles.GetComponent<ParticleSystemRenderer>();
#pragma warning disable CS0618
            ps.startSize *= 0.5f;
            ps.startLifetime *= 0.18f;
            ps.startSpeed = 0f;
            ps.emissionRate = 6f;
            ps.maxParticles = 5;
            ps.playbackSpeed *= 0.4f;
#pragma warning restore CS0618
            psr.lengthScale *= 0.35f;
            psr.maxParticleSize *= 0.35f;
            foreach (Transform tr in xBubbles.transform)
            {
                tr.localPosition = new Vector3(0.5f, 0.0f, 0.0f);
                psr = tr.GetComponent<ParticleSystemRenderer>();
                if (tr.name.StartsWith("xDots", true, CultureInfo.InvariantCulture))
                {
                    ps = tr.GetComponent<ParticleSystem>();
#pragma warning disable CS0618
                    ps.startSize *= 0.3f;
                    ps.startLifetime *= 0.11f;
                    ps.startSpeed = 0.3f;
                    ps.emissionRate = 3f;
                    ps.maxParticles = 6;
                    ps.playbackSpeed *= 0.5f;
#pragma warning restore CS0618
                    psr.lengthScale *= 0.11f;
                    psr.maxParticleSize *= 0.11f;
                }
                else if (tr.name.StartsWith("xLateralBubbles", true, CultureInfo.InvariantCulture))
                    psr.enabled = false;
            }

#if DEBUG_AQUARIUM
            Logger.Debug("A11");
#endif
            // Adjust prefab identifier
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Adjust tech tag
            var techTag = prefab.GetComponent<TechTag>();
            techTag.type = this.TechType;

#if DEBUG_AQUARIUM
            Logger.Debug("A12");
#endif
            // Adjust contructable
            Constructable constructable = prefab.GetComponent<Constructable>();
            constructable.allowedInBase = true;
            constructable.allowedInSub = true;
            constructable.allowedOutside = true;
            constructable.allowedOnCeiling = false;
            constructable.allowedOnGround = true;
            constructable.allowedOnConstructables = true;
            constructable.allowedUnderwater = true;
            constructable.controlModelState = true;
            constructable.deconstructionAllowed = true;
            constructable.rotationEnabled = true;
            //constructable.model = model;
            constructable.techType = this.TechType;
            constructable.placeMinDistance = 0.5f;
            constructable.enabled = true;

#if DEBUG_AQUARIUM
            Logger.Debug("A13");
#endif
            // Adjust constructable bounds
            ConstructableBounds bounds = prefab.GetComponent<ConstructableBounds>();
            bounds.bounds = new OrientedBounds(new Vector3((bounds.bounds.position.x * 0.16f) + (0.145f * 2.0f), bounds.bounds.position.y * 0.285f, bounds.bounds.position.z * 0.5f), bounds.bounds.rotation, new Vector3(bounds.bounds.extents.x * 0.16f, bounds.bounds.extents.y * 0.285f, bounds.bounds.extents.z * 0.5f));

            // Adjust collider
            BoxCollider c = collider.GetComponent<BoxCollider>();
            c.size = new Vector3(c.size.x * 0.16f, c.size.y * 0.285f, c.size.z * 0.5f);
            c.center = new Vector3((c.center.x * 0.16f) + (0.145f * 2.0f), c.center.y * 0.285f, c.center.z * 0.5f);

            // Adjust aquarium
            var aquarium = prefab.GetComponent<Aquarium>();
            aquarium.storageContainer.width = 1;
            aquarium.storageContainer.height = 1;
            aquarium.trackObjects = new GameObject[] { aquariumAnim2Attach4 };

#if DEBUG_AQUARIUM
            Logger.Debug("A14");
#endif
            // Adjust rendering
#if SUBNAUTICA
            var sas = prefab.GetComponents<SkyApplier>();
            List<Renderer> rendsA = new List<Renderer>();
            List<Renderer> rendsB = new List<Renderer>();
#else
            var sas = prefab.GetComponentsInChildren<SkyApplier>();
#endif
            int i = 0;
            foreach (SkyApplier sa in sas)
            {
                int j = 0;
                foreach (Renderer rend in sa.renderers)
                {
#if DEBUG_AQUARIUM
                    Logger.Debug("SkyApp " + i.ToString() + ": rend " + j.ToString() + " name=[" + rend.name + "]");
#endif
                    if (string.Compare(rend.name, "Aquarium", true, CultureInfo.InvariantCulture) != 0)
                    {
                        if (ConfigSwitcher.GlowingAquariumGlass && string.Compare(rend.name, "Aquarium_glass", true, CultureInfo.InvariantCulture) == 0)
                        {
                            foreach (Material mat in rend.materials)
                            {
                                mat.EnableKeyword("MARMO_EMISSION");
                                mat.EnableKeyword("MARMO_GLOW");
                                mat.SetFloat("_SpecInt", 16f);
                                mat.SetFloat("_EnableGlow", 1);
                                mat.SetColor("_GlowColor", new Color(0.79f, 0.9785799f, 1.0f, 0.08f));
                                mat.SetFloat("_GlowStrength", 0.18f);
                            }
                        }
#if SUBNAUTICA
                        if (i == 0)
                            rendsA.Add(rend);
                        else
                            rendsB.Add(rend);
#endif
                    }
                    j++;
                }
                i++;
            }
#if DEBUG_AQUARIUM
            Logger.Debug("A15");
#endif
#if SUBNAUTICA
            sas[0].renderers = rendsA.ToArray();
            sas[1].renderers = rendsB.ToArray();
#endif
            Renderer[] tmpRends = prefab.GetAllComponentsInChildren<Renderer>();
            List<Renderer> rends = new List<Renderer>();
            foreach (Renderer tmpRend in tmpRends)
                if (string.Compare(tmpRend.name, "Aquarium", true, CultureInfo.InvariantCulture) == 0)
                    rends.Add(tmpRend);

#if DEBUG_AQUARIUM
            Logger.Debug("A16");
#endif
            var fixedSa = prefab.AddComponent<SkyApplier>();
            fixedSa.anchorSky = Skies.Auto;
            fixedSa.dynamic = true;
            fixedSa.renderers = rends.ToArray();
            fixedSa.updaterIndex = 0;

#if DEBUG_AQUARIUM
            Logger.Debug("A17");
#endif
            // Adjust bubbles LOD
            var lodBubbles = bubbles.GetComponent<LODGroup>();
            lodBubbles.size *= (1.0f / 0.07f);

#if DEBUG_AQUARIUM
            Logger.Debug("A18");
#endif
            // Remove unwanted elements
            GameObject.DestroyImmediate(aquariumAnim2Fish1);
            GameObject.DestroyImmediate(aquariumAnim2Fish2);
            GameObject.DestroyImmediate(aquariumAnim2Fish3);
            GameObject.DestroyImmediate(aquariumAnim1Fish1);
            GameObject.DestroyImmediate(aquariumAnim1Fish2);
            GameObject.DestroyImmediate(aquariumAnim1Fish3);
            GameObject.DestroyImmediate(aquariumAnim1Fish4);

#if DEBUG_AQUARIUM
            Logger.Debug("A19");
#endif
            // Adjust prefab position
            foreach (Transform tr in prefab.transform)
                tr.transform.localPosition = new Vector3(tr.transform.localPosition.x - 0.29f, tr.transform.localPosition.y, tr.transform.localPosition.z);

#if DEBUG_AQUARIUM
            Logger.Debug("A20");
#endif
            return prefab;
        }
    }
}
