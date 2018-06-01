using SMLHelper;
using SMLHelper.Patchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsFabricator
{
    public abstract class DecorationItem
    {
        #region Attributes

        // This is used as the default path when we add a new resource to the game
        public const string DefaultResourcePath = "WorldEntities/Environment/Wrecks/";

        // This is used to know if we already registered our item in the game
        public bool IsRegistered = false;

        // The item class ID
        public string ClassID { get; set; }

        // The item resource path
        public string ResourcePath { get; set; }
        
        // The item root GameObject
        public GameObject GameObject { get; set; }

        // The item TechType
        public TechType TechType { get; set; }

        // The item recipe
        public TechDataHelper Recipe { get; set; }

        #endregion
        #region Abstract and virtual methods

        public abstract GameObject GetPrefab();

        public virtual void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Set the buildable prefab
                CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, this.ResourcePath, this.TechType, this.GetPrefab));

                // Associate new recipe
                CraftDataPatcher.customTechData[this.TechType] = this.Recipe;

                this.IsRegistered = true;
            }
        }

        #endregion
    }
}
