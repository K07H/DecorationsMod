using UnityEngine;

namespace DecorationsMod.Fixers
{
    public class InventoryConsoleCommandsFixer
    {
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
                                        GameObject gameObject = CraftData.InstantiateFromPrefab(techType, false);
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
                                    // Dont call original function if item being spawned is one of our new flora.
                                    return false;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            // Give back execution to original function.
            return true;
        }
    }
}
