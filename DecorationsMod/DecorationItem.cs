using SMLHelper;
using SMLHelper.Patchers;
using UnityEngine;

namespace DecorationsMod
{
    public interface IDecorationItem
    {
        // Property signatures
        string ClassID { get; }
        string PrefabFileName { get; }
        TechType TechType { get; }

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
        
        // The item resource path
        //public string PrefabFileName { get; set; }
        
        // The item root GameObject
        public GameObject GameObject { get; set; }
        
        // The item recipe
        public SMLHelper.V2.Crafting.TechData Recipe { get; set; }

        #endregion
        #region Abstract and virtual methods

        //public abstract GameObject GetGameObject();

        public virtual void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Update PlaceTool parameters
                var placeTool = this.GameObject.GetComponent<PlaceTool>();
                if (placeTool != null)
                {
                    placeTool.enabled = true;
                    placeTool.allowedInBase = true;
                    placeTool.allowedOnBase = true;
                    placeTool.allowedOnCeiling = false;
                    placeTool.allowedOnConstructable = true;
                    placeTool.allowedOnRigidBody = true;
                    placeTool.allowedOutside = ConfigSwitcher.AllowPlaceOutside;
                    placeTool.rotationEnabled = true;
                    
                    if (this.TechType == TechType.Poster ||
                        this.TechType == TechType.PosterAurora ||
                        this.TechType == TechType.PosterExoSuit1 ||
                        this.TechType == TechType.PosterExoSuit2 ||
                        this.TechType == TechType.PosterKitty)
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

                // Associate new recipe
                if (this.Recipe != null)
                    SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                this.IsRegistered = true;
            }
        }

        #endregion
    }
}
