using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsFabricator
{
    public static class AssetsHelper
    {
        // Load AssetBundles (they must only be loaded once).
        public static AssetBundle Assets = AssetBundle.LoadFromFile(@"./QMods/DecorationsFabricator/Assets/decorationassets.assets");
    }
}
