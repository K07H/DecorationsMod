using System.IO;
using System.Reflection;
using UnityEngine;

namespace DecorationsMod
{
    public static class AssetsHelper
    {
        // Load AssetBundles (they must only be loaded once).
        public static readonly AssetBundle Assets = AssetBundle.LoadFromFile(FilesHelper.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets/decorationassets.assets"));

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
