using System.Reflection;
using UnityEngine;

namespace DecorationsMod.Controllers
{
    public class CustomPlaceToolController : MonoBehaviour
    {
        public bool HandlingInput = false;

        private void Update()
        {
            if (HandlingInput && ConfigSwitcher.LockQuickslotsWhenPlacingItem)
            {
                GameInput.Button[] quickSlotBtns = (GameInput.Button[])QuickSlotButtons.GetValue(null);
                if (quickSlotBtns != null)
                {
                    for (int i = 0; i < quickSlotBtns.Length; i++)
                        if (GameInput.GetButtonDown(quickSlotBtns[i]))
                        {
                            this.Hide();
                            return;
                        }
                }
                foreach (KeyCode c in CustomPlaceToolController.Keys)
                    if (Input.GetKeyDown(c))
                    {
                        this.Hide();
                        return;
                    }
            }
        }

        public void Show()
        {
            if (HandlingInput || !ConfigSwitcher.LockQuickslotsWhenPlacingItem)
                return;
            Inventory.main.quickSlots.SetIgnoreHotkeyInput(true);
            HandlingInput = true;
        }

        public void Hide()
        {
            if (!HandlingInput || !ConfigSwitcher.LockQuickslotsWhenPlacingItem)
                return;
            Inventory.main.quickSlots.SetIgnoreHotkeyInput(false);
            HandlingInput = false;
        }

        private static readonly FieldInfo QuickSlotButtons = typeof(Player).GetField("quickSlotButtons", BindingFlags.NonPublic | BindingFlags.Static);

        public static readonly KeyCode[] Keys = new KeyCode[11]
        {
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9,
            KeyCode.Alpha0,
            KeyCode.Escape
        };
    }
}
