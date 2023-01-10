using UnityEngine;

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
    
    public abstract class DecorationItem : SMLHelper.V2.Assets.ModPrefab, IDecorationItem
    {
        #region Constructor

        public DecorationItem() : base("", "") { }

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
#if SUBNAUTICA
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
                    SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                // Update PlaceTool parameters
                PrefabsHelper.SetDefaultPlaceTool(this.GameObject, null, null, false, false, true);
                PlaceTool placeTool = this.GameObject.GetComponent<GenericPlaceTool>();
                if (placeTool == null)
                    placeTool = this.GameObject.GetComponent<PlaceTool>();
                if (placeTool != null)
                {
                    if (this.TechType == TechType.Poster
                        || this.TechType == TechType.PosterAurora
                        || this.TechType == TechType.PosterExoSuit1
                        || this.TechType == TechType.PosterExoSuit2
                        || this.TechType == TechType.PosterKitty
#if BELOWZERO
                        || this.TechType == TechType.PosterSpyPenguin
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
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                this.IsRegistered = true;
            }
        }

        #endregion
    }
}
