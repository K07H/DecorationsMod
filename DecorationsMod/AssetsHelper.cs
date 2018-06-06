using UnityEngine;

namespace DecorationsMod
{
    public static class AssetsHelper
    {
        // Load AssetBundles (they must only be loaded once).
        public static AssetBundle Assets = AssetBundle.LoadFromFile(@"./QMods/DecorationsMod/Assets/decorationassets.assets");
    }
}
