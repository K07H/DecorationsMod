using UnityEngine;
using Nautilus.Assets;

#if SUBNAUTICA_NAUTILUS
using System.Diagnostics.CodeAnalysis;
#endif

namespace DecorationsMod
{
    public interface IDecorationItem
    {
        // Property signatures
        string ClassID { get; }
        string PrefabFileName { get; }
        TechType TechType { get; }
        Sprite Sprite { get; }

        // Method signatures
        GameObject GetGameObject();
        void RegisterItem();
    }
    
#if SUBNAUTICA_NAUTILUS
    public abstract class DecorationItem : Nautilus.Assets.CustomPrefab, IDecorationItem
#else
    public abstract class DecorationItem : SMLHelper.V2.Assets.ModPrefab, IDecorationItem
#endif
    {
        #region Constructor

#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public DecorationItem(string classID, string nameKey, string descKey, string icon, params object[] translationArgs) : this(
            PrefabInfo.WithTechType(classID, LanguageHelper.GetFriendlyWord(nameKey, translationArgs), LanguageHelper.GetFriendlyWord(descKey, translationArgs), unlockAtStart: true)
            .WithFileName(DefaultResourcePath + classID)
            .WithIcon(AssetsHelper.Assets.LoadAsset<Sprite>(icon))) { }

        [SetsRequiredMembers]
        public DecorationItem(string classID, string nameKey, string descKey, string icon) : this(
            PrefabInfo.WithTechType(classID, LanguageHelper.GetFriendlyWord(nameKey), LanguageHelper.GetFriendlyWord(descKey), unlockAtStart: true)
            .WithFileName(DefaultResourcePath + classID)
            .WithIcon(AssetsHelper.Assets.LoadAsset<Sprite>(icon))) { }

        [SetsRequiredMembers]
        public DecorationItem(string classID, string nameKey, string descKey, Atlas.Sprite icon, params object[] translationArgs) : this(
            PrefabInfo.WithTechType(classID, LanguageHelper.GetFriendlyWord(nameKey, translationArgs), LanguageHelper.GetFriendlyWord(descKey, translationArgs), unlockAtStart: true)
            .WithFileName(DefaultResourcePath + classID)
            .WithIcon(icon)) { }

        [SetsRequiredMembers]
        public DecorationItem(string classID, string nameKey, string descKey, Atlas.Sprite icon) : this(
            PrefabInfo.WithTechType(classID, LanguageHelper.GetFriendlyWord(nameKey), LanguageHelper.GetFriendlyWord(descKey), unlockAtStart: true)
            .WithFileName(DefaultResourcePath + classID)
            .WithIcon(icon)) { }

        [SetsRequiredMembers]
        public DecorationItem(Nautilus.Assets.PrefabInfo info) : base(info)
        {
            SetGameObject(this.GetGameObject);
        }
#else
        public DecorationItem() : base("", "") { }
#endif

        #endregion
        #region Attributes

        // This is used as the default path when we add a new resource to the game
        public const string DefaultResourcePath = "WorldEntities/Environment/Wrecks/";

        // This is used to know if we already registered our item in the game
        public bool IsRegistered = false;

        // This is used to know if item appears in habitat builder menu
        public bool IsHabitatBuilder = false;
        
        // The item root GameObject
        public GameObject GameObject { get; set; }

        public Sprite Sprite { get; set; }

        // The item recipe
#if SUBNAUTICA_NAUTILUS
        public Nautilus.Crafting.RecipeData Recipe { get; set; }
        public string ClassID => Info.ClassID;
        public string PrefabFileName => Info.PrefabFileName;
        public TechType TechType => Info.TechType;
        public abstract GameObject GetGameObject();
#elif SUBNAUTICA
        public SMLHelper.V2.Crafting.TechData Recipe { get; set; }
#else
        public SMLHelper.V2.Crafting.RecipeData Recipe { get; set; }
#endif

        #endregion
        #region Abstract and virtual methods

        //public abstract GameObject GetGameObject();

        public virtual void RegisterItem()
        {
            if (this.IsRegistered == false && this.GameObject != null)
            {
                // Associate new recipe
                if (this.Recipe != null)
#if SUBNAUTICA_NAUTILUS
                    Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.Info.TechType, this.Recipe);
#else
                    SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);
#endif

                // Update PlaceTool parameters
                PrefabsHelper.SetDefaultPlaceTool(this.GameObject, null, null, false, false, true);
                PlaceTool placeTool = this.GameObject.GetComponent<GenericPlaceTool>();
                if (placeTool == null)
                    placeTool = this.GameObject.GetComponent<PlaceTool>();
                if (placeTool != null)
                {
#if SUBNAUTICA_NAUTILUS
                    TechType techType = this.Info.TechType;
#else
                    TechType techType = this.TechType;
#endif
                    if (techType == TechType.Poster
                        || techType == TechType.PosterAurora
                        || techType == TechType.PosterExoSuit1
                        || techType == TechType.PosterExoSuit2
                        || techType == TechType.PosterKitty
#if BELOWZERO
                        || techType == TechType.PosterSpyPenguin
#endif
                    )
                    {
                        placeTool.allowedOnGround = false;
                        placeTool.allowedOnWalls = true;
                        placeTool.hasAnimations = false;
                        placeTool.hasBashAnimation = false;
                        placeTool.hasFirstUseAnimation = false;
                    }
                    else
                    {
                        placeTool.allowedOnGround = true;
                        placeTool.allowedOnWalls = false;
                    }
                }

                // Set the buildable prefab
#if SUBNAUTICA_NAUTILUS
                this.Register();
#else
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);
#endif

                this.IsRegistered = true;
            }
        }

        #endregion
    }
}
