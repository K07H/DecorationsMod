using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace DecorationsMod
{
    public class ReactorLampBrightness : HandTarget, IHandTarget
    {
        public Texture GetNextEmissionMap(Texture current)
        {
            switch (current.name)
            {
                case "nuclear_reactor_rod_illum_yellow":
                    return AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_orange");
                case "nuclear_reactor_rod_illum_orange":
                    return AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_red");
                case "nuclear_reactor_rod_illum_red":
                    return AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_pink");
                case "nuclear_reactor_rod_illum_pink":
                    return AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_purple");
                case "nuclear_reactor_rod_illum_purple":
                    return AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_blue");
                case "nuclear_reactor_rod_illum_blue":
                    return AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum");
                case "nuclear_reactor_rod_illum_cyan":
                    return AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_green");
                default:
                    return AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_yellow");
            }
        }

        public void OnHandClick(GUIHand hand)
        {
            if (!enabled) return;

            var reactorRodLight = this.gameObject.GetComponentInChildren<Light>();
            if (reactorRodLight == null) return;

            // Adjust intensity
            if (Input.GetKey(KeyCode.I))
            {
                if (reactorRodLight.intensity > 3.0f)
                {
                    reactorRodLight.intensity = 0f;
                    Renderer[] renderers = this.gameObject.GetComponentsInChildren<Renderer>();
                    foreach (Renderer renderer in renderers)
                    {
                        if (renderer.name.CompareTo("nuclear_reactor_rod_mesh") == 0)
                            renderer.material.DisableKeyword("MARMO_EMISSION"); // Disable emission
                    }
                }
                else if (reactorRodLight.intensity == 0f)
                {
                    Renderer[] renderers = this.gameObject.GetComponentsInChildren<Renderer>();
                    foreach (Renderer renderer in renderers)
                    {
                        if (renderer.name.CompareTo("nuclear_reactor_rod_mesh") == 0)
                            renderer.material.EnableKeyword("MARMO_EMISSION"); // Enable emission
                    }
                    reactorRodLight.intensity += 0.1f;
                }
                else
                    reactorRodLight.intensity += 0.1f;
            }
            else if (Input.GetKey(KeyCode.E)) // Adjust emission map
            {
                Renderer[] renderers = this.gameObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    if (renderer.name.CompareTo("nuclear_reactor_rod_mesh") == 0)
                        renderer.material.SetTexture("_Illum", GetNextEmissionMap(renderer.material.GetTexture("_Illum")));
                }
            }
            else if (Input.GetKey(KeyCode.R)) // Adjust red levels
            {
                if (reactorRodLight.color.r >= 1.0f)
                    reactorRodLight.color = new Color(0f, reactorRodLight.color.g, reactorRodLight.color.b);
                else
                    reactorRodLight.color = new Color(reactorRodLight.color.r + 0.1f, reactorRodLight.color.g, reactorRodLight.color.b);
            }
            else if (Input.GetKey(KeyCode.G)) // Adjust green levels
            {
                if (reactorRodLight.color.g >= 1.0f)
                    reactorRodLight.color = new Color(reactorRodLight.color.r, 0f, reactorRodLight.color.b);
                else
                    reactorRodLight.color = new Color(reactorRodLight.color.r, reactorRodLight.color.g + 0.1f, reactorRodLight.color.b);
            }
            else if (Input.GetKey(KeyCode.B)) // Adjust blue levels
            {
                if (reactorRodLight.color.b >= 1.0f)
                    reactorRodLight.color = new Color(reactorRodLight.color.r, reactorRodLight.color.g, 0f);
                else
                    reactorRodLight.color = new Color(reactorRodLight.color.r, reactorRodLight.color.g, reactorRodLight.color.b + 0.1f);
            }
            else // Adjust range
            {
                if (reactorRodLight.range > 60.0f)
                {
                    Renderer[] renderers = GetComponentsInChildren<Renderer>();
                    foreach (Renderer renderer in renderers)
                    {
                        if (renderer.name.CompareTo("nuclear_reactor_rod_mesh") == 0)
                            renderer.material.DisableKeyword("MARMO_EMISSION"); // Disable emission
                    }
                    reactorRodLight.range = 0f;
                }
                else if (reactorRodLight.range == 0f)
                {
                    Renderer[] renderers = GetComponentsInChildren<Renderer>();
                    foreach (Renderer renderer in renderers)
                    {
                        if (renderer.name.CompareTo("nuclear_reactor_rod_mesh") == 0)
                            renderer.material.EnableKeyword("MARMO_EMISSION"); // Enable emission
                    }
                    reactorRodLight.range += 3.0f;
                }
                else
                    reactorRodLight.range += 3.0f;
            }
        }

        public void OnHandHover(GUIHand hand)
        {
            if (!enabled)
                return;

            var reticle = HandReticle.main;
            reticle.SetIcon(HandReticle.IconType.Hand, 1f);
            reticle.SetInteractText("ToggleLamp");
        }
    }
}
