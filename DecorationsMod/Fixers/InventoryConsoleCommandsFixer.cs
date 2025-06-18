using System;
using System.Collections;
using UnityEngine;

namespace DecorationsMod.Fixers
{
    public class InventoryConsoleCommandsFixer
    {
        public static IEnumerator ItemCmdSpawnAsync_Postfix(IEnumerator values, int number, TechType techType)
        {
            foreach (IDecorationItem item in DecorationsMod.DecorationItems)
                if (techType == item.TechType) // If item being spawned is one of our decoration items.
                {
#if DEBUG_FLORA_CONSOLE
                    Logger.Debug("DEBUG: ItemCmdSpawnAsync_Postfix: T1 TechType=[" + techType.AsString() + "][" + Convert.ToString((int)techType) + "] ClassID=[" + (item.ClassID ?? "null") + "]");
#endif
                    if (!string.IsNullOrEmpty(item.ClassID) && CustomFlora.AllPlants.Contains(item.ClassID)) // If item being spawned is one of our new flora.
                    {
#if DEBUG_FLORA_CONSOLE
                        Logger.Debug("DEBUG: ItemCmdSpawnAsync_Postfix: T2");
#endif
                        TaskResult<GameObject> result = new TaskResult<GameObject>();
                        for (int i = 0; i < number; i++)
                        {
                            yield return CraftData.InstantiateFromPrefabAsync(techType, result, false);
                            GameObject gameObject = result.Get();
                            if (gameObject != null)
                            {
                                gameObject.transform.position = MainCamera.camera.transform.position + MainCamera.camera.transform.forward * 3f;
                                CrafterLogic.NotifyCraftEnd(gameObject, techType);
                                Pickupable component = gameObject.GetComponent<Pickupable>();
                                if (component != null && !Inventory.main.Pickup(component, false))
                                {
                                    ErrorMessage.AddError(Language.main.Get("InventoryFull"));
                                    // Hide plant, show seed and enable pickupable
                                    PrefabsHelper.HidePlantAndShowSeed(gameObject.transform, item.ClassID);
                                }
                            }
                        }
                        // Dont call origin function if item being spawned is one of our new flora.
                        yield break;
                    }
                }
            // Give back execution to origin function.
            yield return values;
            yield break;
        }

        /*
        public static bool OnConsoleCommand_item_Prefix(NotificationCenter.Notification n)
        {
            if (n != null && n.data != null && n.data.Count > 0)
            {
                string text = (string)n.data[0];
                if (UWE.Utils.TryParseEnum<TechType>(text, out TechType techType) && techType != TechType.None)
                {
                    if (CraftData.IsAllowed(techType))
                    {
                        foreach (IDecorationItem item in DecorationsMod.DecorationItems)
                        {
                            // If item being spawned is one of our decoration items.
                            if (techType == item.TechType)
                            {
                                // If item being spawned is one of our new flora.
                                if (!string.IsNullOrEmpty(item.ClassID) && CustomFlora.AllPlants.Contains(item.ClassID))
                                {
                                    int num = 1;
                                    if (n.data.Count > 1 && int.TryParse((string)n.data[1], out int num2))
                                        num = num2;
                                    for (int i = 0; i < num; i++)
                                    {
                                        GameObject gameObject = PrefabsHelper.InstantiateFromPrefabSync(techType);
                                        if (gameObject != null)
                                        {
                                            gameObject.transform.position = MainCamera.camera.transform.position + MainCamera.camera.transform.forward * 3f;
                                            CrafterLogic.NotifyCraftEnd(gameObject, techType);
                                            Pickupable component = gameObject.GetComponent<Pickupable>();
                                            if (component != null && !Inventory.main.Pickup(component, false))
                                            {
                                                ErrorMessage.AddError(Language.main.Get("InventoryFull"));
                                                // Hide plant, show seed and enable pickupable
                                                PrefabsHelper.HidePlantAndShowSeed(gameObject.transform, item.ClassID);
                                            }
                                        }
                                    }
                                    // Dont call origin function if item being spawned is one of our new flora.
                                    return false;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            // Give back execution to origin function.
            return true;
        }
        */
    }
}
