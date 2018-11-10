using UnityEngine;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;

namespace DecorationsMod
{
    public interface IDecorationItem
    {
        // Property signatures
        string ClassID { get; set; }
        TechType TechType { get; set; }

        // Method signatures
        GameObject GetPrefab();
        void RegisterItem();
    }

    public abstract class DecorationItem : IDecorationItem
    {
        #region Attributes

        // This is used as the default path when we add a new resource to the game
        public const string DefaultResourcePath = "WorldEntities/Environment/Wrecks/";

        // This is used to know if we already registered our item in the game
        public bool IsRegistered = false;

        // This is used to know if item appears in habitat builder menu
        public bool IsHabitatBuilder = false;

        // The item class ID
        public string ClassID { get; set; }

        // The item resource path
        public string ResourcePath { get; set; }
        
        // The item root GameObject
        public GameObject GameObject { get; set; }

        // The item TechType
        public TechType TechType { get; set; }

        // The item recipe
        public TechData Recipe { get; set; }

        #endregion
        #region Abstract and virtual methods

        public abstract GameObject GetPrefab();

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
                SMLHelper.CustomPrefabHandler.customPrefabs.Add(new SMLHelper.CustomPrefab(this.ClassID, this.ResourcePath, this.TechType, this.GetPrefab));

                // Associate new recipe
                if (this.Recipe != null)
                    CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                this.IsRegistered = true;
            }
        }

        #endregion
    }
}
