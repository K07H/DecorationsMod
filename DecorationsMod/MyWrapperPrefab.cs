using SMLHelper.V2.Assets;
using UnityEngine;

namespace DecorationsMod
{
    public delegate GameObject MyGetResource();

    public class MyWrapperPrefab : ModPrefab
    {
        public MyGetResource GetResourceDelegate;

        public MyWrapperPrefab(string classId, string prefabFileName, TechType techType, MyGetResource getResourceDelegate) :
            base(classId, prefabFileName, techType)
        {
            GetResourceDelegate = getResourceDelegate;
        }

        public override GameObject GetGameObject()
        {
            return GetResourceDelegate.Invoke();
        }
    }

    /*
    internal class MyWrapperPrefab : SMLHelper.V2.Assets.ModPrefab
    {
        internal SMLHelper.CustomPrefab Prefab;

        internal MyWrapperPrefab(SMLHelper.CustomPrefab prefab) : base(prefab.ClassID, prefab.PrefabFileName, prefab.TechType)
        {
            Prefab = prefab;
        }

        public override GameObject GetGameObject()
        {
            return Prefab.GetResource() as GameObject;
        }
    }
    */
}
