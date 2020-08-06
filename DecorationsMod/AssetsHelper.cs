using UnityEngine;

namespace DecorationsMod
{
    public static class AssetsHelper
    {
        // Load AssetBundles (they must only be loaded once).
        public static AssetBundle Assets = AssetBundle.LoadFromFile(@"./QMods/DecorationsMod/Assets/decorationassets.assets");

        // Creates an audio asset.
        public static FMODAsset CreateAsset(string id, string name, string path)
        {
            FMODAsset asset = ScriptableObject.CreateInstance<FMODAsset>();
            asset.name = name;
            asset.id = id;
            asset.path = path;
            return asset;
        }
    }
}
