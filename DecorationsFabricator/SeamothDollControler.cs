using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsMod
{
    public class SeamothDollControler : HandTarget, IHandTarget
    {
        public void OnHandClick(GUIHand hand)
        {
            if (!enabled)
                return;

            GameObject model = this.gameObject.FindChild("Model");
            if (model == null)
                return;
            
            GameObject extras = model.FindChild("Submersible_SeaMoth_extras");
            GameObject extra = extras.FindChild("Submersible_seaMoth_geo 1").FindChild("seaMoth_storage_01_L_geo");
            Renderer rend = extra.GetComponent<Renderer>();

            Renderer[] renderers = extras.GetAllComponentsInChildren<Renderer>();
            
            if (rend.enabled)
            {
                foreach (Renderer renderer in renderers)
                {
                    renderer.enabled = false;
                }
            }
            else
            {
                foreach (Renderer renderer in renderers)
                {
                    renderer.enabled = true;
                }
            }
        }

        public void OnHandHover(GUIHand hand)
        {
            if (!enabled)
                return;

            var reticle = HandReticle.main;
            reticle.SetIcon(HandReticle.IconType.Hand, 1f);
            reticle.SetInteractText("SwitchSeamothModel");
        }
    }
}
