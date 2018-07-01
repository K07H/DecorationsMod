using System;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace DecorationsMod.Controllers
{
    public class ReactorLampBrightness : HandTarget, IHandTarget, IProtoEventListener
    {
        private Texture yellow = AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_yellow");
        private Texture orange = AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_orange");
        private Texture red = AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_red");
        private Texture pink = AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_pink");
        private Texture purple = AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_purple");
        private Texture blue = AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_blue");
        private Texture cyan = AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum");
        private Texture green = AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_green");
        private Texture white = AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_white");

        private bool isOn = true;
        private float savedIntensity = -1.0f;
        private float savedRange = -1.0f;
        private Color savedGlowColor = Color.black;

        public Texture GetNextEmissionMap(Texture current)
        {
            switch (current.name)
            {
                case "nuclear_reactor_rod_illum_white":
                    ErrorMessage.AddDebug("Lamp: Center color updated (yellow)");
                    return yellow;
                case "nuclear_reactor_rod_illum_yellow":
                    ErrorMessage.AddDebug("Lamp: Center color updated (orange)");
                    return orange;
                case "nuclear_reactor_rod_illum_orange":
                    ErrorMessage.AddDebug("Lamp: Center color updated (red)");
                    return red;
                case "nuclear_reactor_rod_illum_red":
                    ErrorMessage.AddDebug("Lamp: Center color updated (pink)");
                    return pink;
                case "nuclear_reactor_rod_illum_pink":
                    ErrorMessage.AddDebug("Lamp: Center color updated (purple)");
                    return purple;
                case "nuclear_reactor_rod_illum_purple":
                    ErrorMessage.AddDebug("Lamp: Center color updated (blue)");
                    return blue;
                case "nuclear_reactor_rod_illum_blue":
                    ErrorMessage.AddDebug("Lamp: Center color updated (cyan)");
                    return cyan;
                case "nuclear_reactor_rod_illum":
                    ErrorMessage.AddDebug("Lamp: Center color updated (green)");
                    return green;
                case "nuclear_reactor_rod_illum_green":
                    ErrorMessage.AddDebug("Lamp: Center color updated (white)");
                    return white;
                default:
                    ErrorMessage.AddDebug("Lamp: Center color updated (white)");
                    return white;
            }
        }

        public void OnHandClick(GUIHand hand)
        {
            if (!enabled) return;

            Light reactorRodLight = this.gameObject.GetComponentInChildren<Light>();
            if (reactorRodLight == null)
                return;

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
            if (renderer == null)
                return;

            if (Input.GetKey(KeyCode.F)) // Adjust range
            {
                if (reactorRodLight.range >= 80.0f)
                    reactorRodLight.range = 0f;
                else
                    reactorRodLight.range += 3.0f;
                ErrorMessage.AddDebug("Lamp: Light range updated (" + Math.Min(80, (int)reactorRodLight.range) + "/80)");
            }
            else if (Input.GetKey(KeyCode.I)) // Adjust intensity
            {
                //Renderer[] renderers = this.gameObject.GetComponentsInChildren<Renderer>();
                if (reactorRodLight.intensity >= 2.5f)
                    reactorRodLight.intensity = 0f;
                else
                    reactorRodLight.intensity += 0.1f;
                ErrorMessage.AddDebug("Lamp: Light intensity updated (" + String.Format("{0:0.00}", (reactorRodLight.intensity / 2.5f)) + "/1)");
            }
            else if (Input.GetKey(KeyCode.T)) // Adjust reactor rod glow intensity
            {
                // Get current reactor rod glow intensity
                Color glowColor = renderer.sharedMaterial.GetColor("_GlowColor");
                if (glowColor.r >= 2.5f)
                {
                    renderer.material.DisableKeyword("MARMO_EMISSION"); // Disable emission
                    renderer.sharedMaterial.SetColor("_GlowColor", new Color(0.0f, 0.0f, 0.0f, 1.0f)); // Reset glow intensity
                    ErrorMessage.AddDebug("Lamp: Center intensity updated (" + String.Format("{0:0.00}", 0.0f) + "/1)");
                }
                else if (glowColor.r == 0.0f)
                {
                    renderer.sharedMaterial.SetColor("_GlowColor", new Color(0.1f, 0.1f, 0.1f, 1.0f));
                    renderer.material.EnableKeyword("MARMO_EMISSION"); // Enable emission
                    ErrorMessage.AddDebug("Lamp: Center intensity updated (" + String.Format("{0:0.00}", 0.04f) + "/1)");
                }
                else
                {
                    renderer.sharedMaterial.SetColor("_GlowColor", new Color(glowColor.r + 0.1f, glowColor.g + 0.1f, glowColor.b + 0.1f, 1.0f)); // Increase glow intensity
                    ErrorMessage.AddDebug("Lamp: Center intensity updated (" + String.Format("{0:0.00}", (glowColor.r + 0.1f) / 2.5f) + "/1)");
                }
            }
            else if (Input.GetKey(KeyCode.E)) // Adjust emission map
            {
                renderer.material.SetTexture("_Illum", GetNextEmissionMap(renderer.material.GetTexture("_Illum")));
            }
            else if (Input.GetKey(KeyCode.R)) // Adjust red levels
            {
                if (reactorRodLight.color.r >= 1.0f)
                    reactorRodLight.color = new Color(0f, reactorRodLight.color.g, reactorRodLight.color.b);
                else
                    reactorRodLight.color = new Color(reactorRodLight.color.r + 0.1f, reactorRodLight.color.g, reactorRodLight.color.b);
                ErrorMessage.AddDebug("Lamp: Red levels updated (" + String.Format("{0:0.0}", reactorRodLight.color.r) + "/1)");
            }
            else if (Input.GetKey(KeyCode.G)) // Adjust green levels
            {
                if (reactorRodLight.color.g >= 1.0f)
                    reactorRodLight.color = new Color(reactorRodLight.color.r, 0f, reactorRodLight.color.b);
                else
                    reactorRodLight.color = new Color(reactorRodLight.color.r, reactorRodLight.color.g + 0.1f, reactorRodLight.color.b);
                ErrorMessage.AddDebug("Lamp: Green levels updated (" + String.Format("{0:0.0}", reactorRodLight.color.g) + "/1)");
            }
            else if (Input.GetKey(KeyCode.B)) // Adjust blue levels
            {
                if (reactorRodLight.color.b >= 1.0f)
                    reactorRodLight.color = new Color(reactorRodLight.color.r, reactorRodLight.color.g, 0f);
                else
                    reactorRodLight.color = new Color(reactorRodLight.color.r, reactorRodLight.color.g, reactorRodLight.color.b + 0.1f);
                ErrorMessage.AddDebug("Lamp: Blue levels updated (" + String.Format("{0:0.0}", reactorRodLight.color.b) + "/1)");
            }
            else // ON/OFF
            {
                if (isOn)
                    SwitchLampOff(renderer, reactorRodLight);
                else
                    SwitchLampOn(renderer, reactorRodLight);
            }
        }

        private void SwitchLampOff(Renderer renderer, Light reactorRodLight)
        {
            // Get current reactor rod intensity
            savedGlowColor = renderer.sharedMaterial.GetColor("_GlowColor");
            // Get current intensity
            savedIntensity = reactorRodLight.intensity;
            savedRange = reactorRodLight.range;

            // Disable emission
            renderer.material.DisableKeyword("MARMO_EMISSION");
            // Disable reactor rod intensity
            renderer.sharedMaterial.SetColor("_GlowColor", new Color(0.0f, 0.0f, 0.0f, 1.0f));
            // Disable lamp itensity
            reactorRodLight.intensity = 0.0f;
            // Disable lamp range
            reactorRodLight.range = 0.0f;

            isOn = false;
        }

        private void SwitchLampOn(Renderer renderer, Light reactorRodLight)
        {
            // Restore reactor rod intensity
            renderer.sharedMaterial.SetColor("_GlowColor", savedGlowColor);
            // Enable emission
            renderer.material.EnableKeyword("MARMO_EMISSION");
            // Disable lamp itensity
            reactorRodLight.intensity = savedIntensity;
            // Disable lamp range
            reactorRodLight.range = savedRange;

            isOn = true;
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
#if DEBUG_LAMP
            Logger.Log("DEBUG: Entering onProtoSerialize for ReactorLampBrightness name=[" + this.gameObject.name + "]");
#endif
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            if (id == null)
                if ((id = this.gameObject.GetComponent<PrefabIdentifier>()) == null)
                    return;

            string saveFolder = FilesHelper.GetSaveFolderPath();
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            Light reactorRodLight = this.gameObject.GetComponentInChildren<Light>();
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
            if (renderer != null && reactorRodLight != null)
            {
                bool turnedOn = false;
                if (!this.isOn)
                {
                    SwitchLampOn(renderer, reactorRodLight);
                    turnedOn = true;
                }

                string range = Convert.ToString(reactorRodLight.range);
                string intensity = Convert.ToString(reactorRodLight.intensity);
                
                Texture current = renderer.material.GetTexture("_Illum");
                Color currentGlow = renderer.material.GetColor("_GlowColor");
                string rodColor = "0";
                string glowColor = "0";
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

                    glowColor = Convert.ToString(currentGlow.r);
                }

                string red = Convert.ToString(reactorRodLight.color.r);
                string green = Convert.ToString(reactorRodLight.color.g);
                string blue = Convert.ToString(reactorRodLight.color.b);

                File.WriteAllText(Path.Combine(saveFolder, "reactorlamp_" + id.Id + ".txt"), range + Environment.NewLine +
                    intensity + Environment.NewLine +
                    rodColor + Environment.NewLine +
                    red + Environment.NewLine +
                    green + Environment.NewLine +
                    blue + Environment.NewLine +
                    glowColor + Environment.NewLine +
                    (this.isOn ? (turnedOn ? "0" : "1") : "0"));

                if (turnedOn)
                    SwitchLampOff(renderer, reactorRodLight);
            }
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
#if DEBUG_LAMP
            Logger.Log("Entering onProtoDeserialize for ReactorLampBrightness name=[" + this.gameObject.name + "]");
#endif
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            if (id == null)
                return;

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
                if (state != null && state.Length == 8)
                {
                    reactorRodLight.range = float.Parse(state[0], CultureInfo.InvariantCulture.NumberFormat);
                    reactorRodLight.intensity = float.Parse(state[1], CultureInfo.InvariantCulture.NumberFormat);
                    switch (state[2])
                    {
                        case "0": // White
                            renderer.material.SetTexture("_Illum", white); break;
                        case "1": // Yellow
                            renderer.material.SetTexture("_Illum", yellow); break;
                        case "2": // Orange
                            renderer.material.SetTexture("_Illum", orange); break;
                        case "3": // Red
                            renderer.material.SetTexture("_Illum", red); break;
                        case "4": // Pink
                            renderer.material.SetTexture("_Illum", pink); break;
                        case "5": // Purple
                            renderer.material.SetTexture("_Illum", purple); break;
                        case "6": // Blue
                            renderer.material.SetTexture("_Illum", blue); break;
                        case "7": // Cyan
                            renderer.material.SetTexture("_Illum", cyan); break;
                        case "8": // Green
                            renderer.material.SetTexture("_Illum", green); break;
                        default: // Default (White)
                            renderer.material.SetTexture("_Illum", white); break;
                    }
                    float Cred = float.Parse(state[3], CultureInfo.InvariantCulture.NumberFormat);
                    float Cgreen = float.Parse(state[4], CultureInfo.InvariantCulture.NumberFormat);
                    float Cblue = float.Parse(state[5], CultureInfo.InvariantCulture.NumberFormat);
                    reactorRodLight.color = new Color(Cred, Cgreen, Cblue);
                    float Cglow = float.Parse(state[6], CultureInfo.InvariantCulture.NumberFormat);
                    renderer.material.SetColor("_GlowColor", new Color(Cglow, Cglow, Cglow, 1.0f));
                    if (state[7].CompareTo("0") == 0)
                        SwitchLampOff(renderer, reactorRodLight);
                }
            }
        }
    }
}
