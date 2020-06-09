using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace DecorationsMod.Fixers
{
    public class StorageContainerFixer
    {
        public static Dictionary<string, Tuple<StorageContainer, bool>> _storages = new Dictionary<string, Tuple<StorageContainer, bool>>();

        public static void OnProtoDeserializeObjectTree_Postfix(StorageContainer __instance, ProtobufSerializer serializer)
        {
#if DEBUG_CARGO_CRATES
            Logger.Log("DEBUG: OnProtoDeserializeObjectTree()");
#endif
            PrefabIdentifier pid = __instance.gameObject.GetComponent<PrefabIdentifier>();
            if (pid != null && __instance.gameObject.transform.parent != null && __instance.gameObject.transform.parent.gameObject != null)
            {
#if DEBUG_CARGO_CRATES
                Logger.Log("DEBUG: OnProtoDeserializeObjectTree() storageConteiner Id=[" + pid.Id + "] objName=[" + __instance.gameObject.name + "] nbItems=[" + (__instance.container != null ? Convert.ToString(__instance.container.count) : "null") + "]");
#endif
                GameObject parentGO = __instance.gameObject.transform.parent.gameObject;
                PrefabIdentifier pid2 = parentGO.GetComponent<PrefabIdentifier>();
                if (pid2 != null && (parentGO.name.StartsWith("CargoBox01_damaged", true, CultureInfo.InvariantCulture) ||
                                     parentGO.name.StartsWith("CargoBox01a", true, CultureInfo.InvariantCulture) ||
                                     parentGO.name.StartsWith("CargoBox01b", true, CultureInfo.InvariantCulture) ||
                                     parentGO.name.StartsWith("DecorativeLocker", true, CultureInfo.InvariantCulture) ||
                                     parentGO.name.StartsWith("DecorativeLockerClosed", true, CultureInfo.InvariantCulture) ||
                                     parentGO.name.StartsWith("DecorativeLockerDoor", true, CultureInfo.InvariantCulture)))
                {
#if DEBUG_CARGO_CRATES
                    Logger.Log("DEBUG: OnProtoDeserializeObjectTree() parent storageConteiner Id=[" + pid2.Id + "] objName=[" + parentGO.name + "] nbItems=[" + (__instance.container != null ? Convert.ToString(__instance.container.count) : "null") + "]");
#endif
                    if (_storages.ContainsKey(pid2.Id))
                    {
                        if (_storages[pid2.Id].Item2)
                        {
#if DEBUG_CARGO_CRATES
                            Logger.Log("DEBUG: OnProtoDeserializeObjectTree() Setup A"); // Resetting
#endif
                            _storages[pid2.Id] = new Tuple<StorageContainer, bool>(__instance, false);
                        }
                        else
                        {
#if DEBUG_CARGO_CRATES
                            Logger.Log("DEBUG: OnProtoDeserializeObjectTree() Setup B"); // Transfering
#endif
                            _storages[pid2.Id] = new Tuple<StorageContainer, bool>(_storages[pid2.Id].Item1, true);
                            StorageHelper.TransferItems(__instance.storageRoot.gameObject, _storages[pid2.Id].Item1.container);
                            GameObject.Destroy(__instance.gameObject);
                        }
                    }
                    else
                    {
#if DEBUG_CARGO_CRATES
                        Logger.Log("DEBUG: OnProtoDeserializeObjectTree() Setup C"); // Registering
#endif
                        _storages.Add(pid2.Id, new Tuple<StorageContainer, bool>(__instance, false));
                    }
                }
            }
        }
    }
}
