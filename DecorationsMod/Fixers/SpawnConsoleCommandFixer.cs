﻿using System.Collections;
using UnityEngine;

namespace DecorationsMod.Fixers
{
    public class SpawnConsoleCommandFixer
    {
        public static IEnumerator SpawnAsync_Postfix(IEnumerator values, NotificationCenter.Notification n)
        {
            if (n != null && n.data != null && n.data.Count > 0)
            {
                string text = (string)n.data[0];
                TechType techType;
                if (UWE.Utils.TryParseEnum<TechType>(text, out techType) && techType != TechType.None)
                    if (CraftData.IsAllowed(techType))
                    {
                        bool isDecorationsModItem = false;

                        if (techType == OtherItems._nutrientBlockTechType || techType == OtherItems._toyCarTechType)
                            isDecorationsModItem = true;
                        else
                        {
                            foreach (IDecorationItem item in DecorationsMod.DecorationItems)
                                if (techType == item.TechType) // If item being spawned is one of our decoration items.
                                {
                                    isDecorationsModItem = true;
                                    break;
                                }
                        }

                        if (isDecorationsModItem)
                        {
                            CoroutineTask<GameObject> request = CraftData.GetPrefabForTechTypeAsync(techType, true);
                            yield return request;
                            GameObject result = request.GetResult();
                            if (result != null)
                            {
                                int num = 1;
                                int num2;
                                if (n.data.Count > 1 && int.TryParse((string)n.data[1], out num2))
                                    num = num2;
                                float maxDist = 12f;
                                if (n.data.Count > 2)
                                    maxDist = float.Parse((string)n.data[2]);
                                Debug.LogFormat("Spawning {0} {1}", new object[] { num, techType });
                                for (int i = 0; i < num; i++)
                                {
                                    GameObject gameObject = global::Utils.CreatePrefab(result, maxDist, i > 0);
                                    LargeWorldEntity.Register(gameObject);
                                    CrafterLogic.NotifyCraftEnd(gameObject, techType);
                                    gameObject.SendMessage("StartConstruction", SendMessageOptions.DontRequireReceiver);
                                }
                            }
                            else
                                ErrorMessage.AddDebug("Could not find prefab for TechType = " + techType);
                            request = null;
                            // Dont call origin function if item being spawned is one of our new flora.
                            yield break;
                        }
                    }
            }
            // Give back execution to origin function.
            yield return values;
            yield break;
        }

        /*
        public static bool OnConsoleCommand_spawn_Prefix(NotificationCenter.Notification n)
        {
            if (n != null && n.data != null && n.data.Count > 0)
            {
                string text = (string)n.data[0];
                if (UWE.Utils.TryParseEnum<TechType>(text, out TechType techType) && techType != TechType.None)
                    if (CraftData.IsAllowed(techType))
                        foreach (IDecorationItem item in DecorationsMod.DecorationItems)
                            if (techType == item.TechType) // If item being spawned is one of our decoration items.
                            {
                                // If item being spawned is one of our new flora.
                                if (!string.IsNullOrEmpty(item.ClassID) && CustomFlora.AllPlants.Contains(item.ClassID))
                                {
                                    GameObject prefabForTechType = CraftData.GetPrefabForTechType(techType, true);
                                    if (prefabForTechType != null)
                                    {
                                        int num = 1;
                                        int num2;
                                        if (n.data.Count > 1 && int.TryParse((string)n.data[1], out num2))
                                            num = num2;
                                        float maxDist = 12f;
                                        if (n.data.Count > 2)
                                            maxDist = float.Parse((string)n.data[2]);
                                        Debug.LogFormat("Spawning {0} {1}", new object[] { num, techType });
                                        for (int i = 0; i < num; i++)
                                        {
                                            GameObject gameObject = global::Utils.CreatePrefab(prefabForTechType, maxDist, i > 0);
                                            LargeWorldEntity.Register(gameObject);
                                            CrafterLogic.NotifyCraftEnd(gameObject, techType);
                                            gameObject.SendMessage("StartConstruction", SendMessageOptions.DontRequireReceiver);
                                            // Hide plant, show seed and enable pickupable
                                            PrefabsHelper.HidePlantAndShowSeed(gameObject.transform, item.ClassID);
                                        }
                                    }
                                    else
                                        ErrorMessage.AddDebug("Could not find prefab for TechType = " + techType);
                                    // Dont call origin function if item being spawned is one of our new flora.
                                    return false;
                                }
                                break;
                            }
            }
            // Give back execution to origin function.
            return true;
        }
        */
    }
}
