using System;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace DecorationsMod.Controllers
{
    public class ReactorLampBrightness : HandTarget, IHandTarget, IProtoEventListener
    {
        public Texture GetNextEmissionMap(Texture current)
        {
            switch (current.name)
            {
                case "nuclear_reactor_rod_illum_white":
                    return AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_yellow");
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
                case "nuclear_reactor_rod_illum":
                    return AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_green");
                default:
                    return AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_white");
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
        
        public void OnProtoSerialize(ProtobufSerializer serializer)
        {
            string saveFolder = FilesHelper.GetSaveFolderPath();
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            var reactorRodLight = this.gameObject.GetComponentInChildren<Light>();

            string range = Convert.ToString(reactorRodLight.range);
            string intesity = Convert.ToString(reactorRodLight.intensity);
            
            string rodColor = "0";
            Renderer[] renderers = this.gameObject.GetComponentsInChildren<Renderer>();
            Texture current = null;
            foreach (Renderer renderer in renderers)
            {
                if (renderer.name.CompareTo("nuclear_reactor_rod_mesh") == 0)
                {
                    current = renderer.material.GetTexture("_Illum");
                    break;
                }
            }
            if (current != null)
            {
                switch (current.name)
                {
                    case "nuclear_reactor_rod_illum_yellow":
                        rodColor = "1"; // Yellow
                        break;
                    case "nuclear_reactor_rod_illum_orange":
                        rodColor = "2"; // Orange
                        break;
                    case "nuclear_reactor_rod_illum_red":
                        rodColor = "3"; // Red
                        break;
                    case "nuclear_reactor_rod_illum_pink":
                        rodColor = "4"; // Pink
                        break;
                    case "nuclear_reactor_rod_illum_purple":
                        rodColor = "5"; // Purple
                        break;
                    case "nuclear_reactor_rod_illum_blue":
                        rodColor = "6"; // Blue
                        break;
                    case "nuclear_reactor_rod_illum":
                        rodColor = "7"; // Cyan
                        break;
                    case "nuclear_reactor_rod_illum_green":
                        rodColor = "8"; // Green
                        break;
                    default:
                        rodColor = "0"; // White
                        break;
                }
            }

            string red = Convert.ToString(reactorRodLight.color.r);
            string green = Convert.ToString(reactorRodLight.color.g);
            string blue = Convert.ToString(reactorRodLight.color.b);

            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            File.WriteAllText(Path.Combine(saveFolder, "reactorlamp_" + id.Id + ".txt"), range + Environment.NewLine +
                intesity + Environment.NewLine +
                rodColor + Environment.NewLine +
                red + Environment.NewLine +
                green + Environment.NewLine +
                blue);
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            string filePath = Path.Combine(FilesHelper.GetSaveFolderPath(), "reactorlamp_" + id.Id + ".txt");
            if (File.Exists(filePath))
            {
                var reactorRodLight = this.gameObject.GetComponentInChildren<Light>();
                Renderer[] renderers = this.gameObject.GetComponentsInChildren<Renderer>();
                Renderer renderer = null;
                foreach (Renderer rend in renderers)
                {
                    if (rend.name.CompareTo("nuclear_reactor_rod_mesh") == 0)
                    {
                        renderer = rend;
                        break;
                    }
                }

                string rawState = File.ReadAllText(filePath);
                string[] state = rawState.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (state.Length == 6)
                {
                    reactorRodLight.range = float.Parse(state[0], CultureInfo.InvariantCulture.NumberFormat);
                    reactorRodLight.intensity = float.Parse(state[1], CultureInfo.InvariantCulture.NumberFormat);
                    switch (state[2])
                    {
                        case "1": // Yellow
                            renderer.material.SetTexture("_Illum", AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_yellow"));
                            break;
                        case "2": // Orange
                            renderer.material.SetTexture("_Illum", AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_orange"));
                            break;
                        case "3": // Red
                            renderer.material.SetTexture("_Illum", AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_red"));
                            break;
                        case "4": // Pink
                            renderer.material.SetTexture("_Illum", AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_pink"));
                            break;
                        case "5": // Purple
                            renderer.material.SetTexture("_Illum", AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_purple"));
                            break;
                        case "6": // Blue
                            renderer.material.SetTexture("_Illum", AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_blue"));
                            break;
                        case "7": // Cyan
                            renderer.material.SetTexture("_Illum", AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum"));
                            break;
                        case "8": // Green
                            renderer.material.SetTexture("_Illum", AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_green"));
                            break;
                        default: // White
                            renderer.material.SetTexture("_Illum", AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_white"));
                            break;
                    }
                    float red = float.Parse(state[3], CultureInfo.InvariantCulture.NumberFormat);
                    float green = float.Parse(state[4], CultureInfo.InvariantCulture.NumberFormat);
                    float blue = float.Parse(state[5], CultureInfo.InvariantCulture.NumberFormat);
                    reactorRodLight.color = new Color(red, green, blue);
                }
            }
        }
    }
}
