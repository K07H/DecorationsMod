using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DecorationsMod.Controllers
{
    public class ExosuitDollController : HandTarget, IHandTarget, IProtoEventListener
    {
        public void OnHandClick(GUIHand hand)
        {
            if (!enabled)
                return;

            GameObject model = this.gameObject.FindChild("prawnsuit");
            if (model == null)
                return;

            if (Input.GetKey(KeyCode.E))
            {
                GameObject rightArm = model.FindChild("ExosuitArmRight");
                GameObject rightArmRig = rightArm.FindChild("exosuit_01_armRight 1").FindChild("ArmRig 1");
                GameObject rightTorpedoArm = rightArmRig.FindChild("exosuit_arm_torpedoLauncher_geo 1");
                GameObject rightDrillArm = rightArmRig.FindChild("exosuit_drill_geo 1");
                GameObject rightGrapplinArm = rightArmRig.FindChild("exosuit_grapplingHook_geo 1");
                GameObject rightGrapplinHand = rightArmRig.FindChild("exosuit_grapplingHook_hand_geo 1");
                GameObject rightHandArm = rightArmRig.FindChild("exosuit_hand_geo 1");
                GameObject rightPropulsionArm = rightArmRig.FindChild("exosuit_propulsion_geo 1");

                if (rightTorpedoArm.GetComponent<Renderer>().enabled)
                {
                    List<Renderer> renderers = new List<Renderer>();
                    rightTorpedoArm.GetComponentsInChildren<Renderer>(renderers);
                    if (!renderers.Contains(rightTorpedoArm.GetComponent<Renderer>()))
                        renderers.Add(rightTorpedoArm.GetComponent<Renderer>());
                    foreach (Renderer rend in renderers)
                    {
                        rend.enabled = false;
                    }

                    rightDrillArm.GetComponent<Renderer>().enabled = true;
                }
                else if (rightDrillArm.GetComponent<Renderer>().enabled)
                {
                    rightDrillArm.GetComponent<Renderer>().enabled = false;

                    rightGrapplinArm.GetComponent<Renderer>().enabled = true;
                    List<Renderer> renderers = new List<Renderer>();
                    rightGrapplinHand.GetComponentsInChildren<Renderer>(renderers);
                    if (!renderers.Contains(rightGrapplinHand.GetComponent<Renderer>()))
                        renderers.Add(rightGrapplinHand.GetComponent<Renderer>());
                    foreach (Renderer rend in renderers)
                    {
                        rend.enabled = true;
                    }
                }
                else if (rightGrapplinArm.GetComponent<Renderer>().enabled)
                {
                    List<Renderer> renderers = new List<Renderer>();
                    rightGrapplinHand.GetComponentsInChildren<Renderer>(renderers);
                    if (!renderers.Contains(rightGrapplinHand.GetComponent<Renderer>()))
                        renderers.Add(rightGrapplinHand.GetComponent<Renderer>());
                    foreach (Renderer rend in renderers)
                    {
                        rend.enabled = false;
                    }
                    rightGrapplinArm.GetComponent<Renderer>().enabled = false;
                    
                    rightHandArm.GetComponent<Renderer>().enabled = true;
                }
                else if (rightHandArm.GetComponent<Renderer>().enabled)
                {
                    rightHandArm.GetComponent<Renderer>().enabled = false;

                    rightPropulsionArm.GetComponent<Renderer>().enabled = true;
                }
                else
                {
                    rightPropulsionArm.GetComponent<Renderer>().enabled = false;

                    List<Renderer> renderers = new List<Renderer>();
                    rightTorpedoArm.GetComponentsInChildren<Renderer>(renderers);
                    if (!renderers.Contains(rightTorpedoArm.GetComponent<Renderer>()))
                        renderers.Add(rightTorpedoArm.GetComponent<Renderer>());
                    foreach (Renderer rend in renderers)
                    {
                        rend.enabled = true;
                    }
                }
            }
            else
            {
                GameObject leftArm = model.FindChild("ExosuitArmLeft");
                GameObject leftArmRig = leftArm.FindChild("exosuit_01_armRight").FindChild("ArmRig");
                GameObject leftTorpedoArm = leftArmRig.FindChild("exosuit_arm_torpedoLauncher_geo");
                GameObject leftDrillArm = leftArmRig.FindChild("exosuit_drill_geo");
                GameObject leftGrapplinArm = leftArmRig.FindChild("exosuit_grapplingHook_geo");
                GameObject leftGrapplinHand = leftArmRig.FindChild("exosuit_grapplingHook_hand_geo");
                GameObject leftHandArm = leftArmRig.FindChild("exosuit_hand_geo");
                GameObject leftPropulsionArm = leftArmRig.FindChild("exosuit_propulsion_geo");

                if (leftTorpedoArm.GetComponent<Renderer>().enabled)
                {
                    List<Renderer> renderers = new List<Renderer>();
                    leftTorpedoArm.GetComponentsInChildren<Renderer>(renderers);
                    if (!renderers.Contains(leftTorpedoArm.GetComponent<Renderer>()))
                        renderers.Add(leftTorpedoArm.GetComponent<Renderer>());
                    foreach (Renderer rend in renderers)
                    {
                        rend.enabled = false;
                    }

                    leftDrillArm.GetComponent<Renderer>().enabled = true;
                }
                else if (leftDrillArm.GetComponent<Renderer>().enabled)
                {
                    leftDrillArm.GetComponent<Renderer>().enabled = false;

                    leftGrapplinArm.GetComponent<Renderer>().enabled = true;
                    List<Renderer> renderers = new List<Renderer>();
                    leftGrapplinHand.GetComponentsInChildren<Renderer>(renderers);
                    if (!renderers.Contains(leftGrapplinHand.GetComponent<Renderer>()))
                        renderers.Add(leftGrapplinHand.GetComponent<Renderer>());
                    foreach (Renderer rend in renderers)
                    {
                        rend.enabled = true;
                    }
                }
                else if (leftGrapplinArm.GetComponent<Renderer>().enabled)
                {
                    List<Renderer> renderers = new List<Renderer>();
                    leftGrapplinHand.GetComponentsInChildren<Renderer>(renderers);
                    if (!renderers.Contains(leftGrapplinHand.GetComponent<Renderer>()))
                        renderers.Add(leftGrapplinHand.GetComponent<Renderer>());
                    foreach (Renderer rend in renderers)
                    {
                        rend.enabled = false;
                    }
                    leftGrapplinArm.GetComponent<Renderer>().enabled = false;

                    leftHandArm.GetComponent<Renderer>().enabled = true;
                }
                else if (leftHandArm.GetComponent<Renderer>().enabled)
                {
                    leftHandArm.GetComponent<Renderer>().enabled = false;

                    leftPropulsionArm.GetComponent<Renderer>().enabled = true;
                }
                else
                {
                    leftPropulsionArm.GetComponent<Renderer>().enabled = false;

                    List<Renderer> renderers = new List<Renderer>();
                    leftTorpedoArm.GetComponentsInChildren<Renderer>(renderers);
                    if (!renderers.Contains(leftTorpedoArm.GetComponent<Renderer>()))
                        renderers.Add(leftTorpedoArm.GetComponent<Renderer>());
                    foreach (Renderer rend in renderers)
                    {
                        rend.enabled = true;
                    }
                }
            }
        }

        public void OnHandHover(GUIHand hand)
        {
            if (!enabled)
                return;

            var reticle = HandReticle.main;
            reticle.SetIcon(HandReticle.IconType.Hand, 1f);
            reticle.SetInteractText("SwitchExosuitModel");
        }

        public static void SetRightArm(GameObject gameObject, string arm)
        {
            GameObject rightArm = gameObject.FindChild("prawnsuit").FindChild("ExosuitArmRight");
            GameObject rightArmRig = rightArm.FindChild("exosuit_01_armRight 1").FindChild("ArmRig 1");
            GameObject rightTorpedoArm = rightArmRig.FindChild("exosuit_arm_torpedoLauncher_geo 1");
            GameObject rightDrillArm = rightArmRig.FindChild("exosuit_drill_geo 1");
            GameObject rightGrapplinArm = rightArmRig.FindChild("exosuit_grapplingHook_geo 1");
            GameObject rightGrapplinHand = rightArmRig.FindChild("exosuit_grapplingHook_hand_geo 1");
            GameObject rightHandArm = rightArmRig.FindChild("exosuit_hand_geo 1");
            GameObject rightPropulsionArm = rightArmRig.FindChild("exosuit_propulsion_geo 1");

            // Right torpedo arm
            if (arm.CompareTo("0") != 0)
            {
                List<Renderer> rightTorpedoArmRenderers = new List<Renderer>();
                rightTorpedoArm.GetComponentsInChildren<Renderer>(rightTorpedoArmRenderers);
                if (!rightTorpedoArmRenderers.Contains(rightTorpedoArm.GetComponent<Renderer>()))
                    rightTorpedoArmRenderers.Add(rightTorpedoArm.GetComponent<Renderer>());
                foreach (Renderer rend in rightTorpedoArmRenderers)
                {
                    rend.enabled = false;
                }
            }
            else
            {
                List<Renderer> renderers = new List<Renderer>();
                rightTorpedoArm.GetComponentsInChildren<Renderer>(renderers);
                if (!renderers.Contains(rightTorpedoArm.GetComponent<Renderer>()))
                    renderers.Add(rightTorpedoArm.GetComponent<Renderer>());
                foreach (Renderer rend in renderers)
                {
                    rend.enabled = true;
                }
            }

            // Right drill arm
            rightDrillArm.GetComponent<Renderer>().enabled = (arm.CompareTo("1") == 0);

            // Right grapplin arm
            if (arm.CompareTo("2") != 0)
            {
                List<Renderer> rightGrapplinArmRenderers = new List<Renderer>();
                rightGrapplinHand.GetComponentsInChildren<Renderer>(rightGrapplinArmRenderers);
                if (!rightGrapplinArmRenderers.Contains(rightGrapplinHand.GetComponent<Renderer>()))
                    rightGrapplinArmRenderers.Add(rightGrapplinHand.GetComponent<Renderer>());
                foreach (Renderer rend in rightGrapplinArmRenderers)
                {
                    rend.enabled = false;
                }
                rightGrapplinArm.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                rightGrapplinArm.GetComponent<Renderer>().enabled = true;
                List<Renderer> renderers = new List<Renderer>();
                rightGrapplinHand.GetComponentsInChildren<Renderer>(renderers);
                if (!renderers.Contains(rightGrapplinHand.GetComponent<Renderer>()))
                    renderers.Add(rightGrapplinHand.GetComponent<Renderer>());
                foreach (Renderer rend in renderers)
                {
                    rend.enabled = true;
                }
            }

            // Right propulsion arm
            rightPropulsionArm.GetComponent<Renderer>().enabled = (arm.CompareTo("3") == 0);

            // Right hand arm
            rightHandArm.GetComponent<Renderer>().enabled = (arm.CompareTo("4") == 0);
        }

        public static void SetLeftArm(GameObject gameObject, string arm)
        {
            GameObject leftArm = gameObject.FindChild("prawnsuit").FindChild("ExosuitArmLeft");
            GameObject leftArmRig = leftArm.FindChild("exosuit_01_armRight").FindChild("ArmRig");
            GameObject leftTorpedoArm = leftArmRig.FindChild("exosuit_arm_torpedoLauncher_geo");
            GameObject leftDrillArm = leftArmRig.FindChild("exosuit_drill_geo");
            GameObject leftGrapplinArm = leftArmRig.FindChild("exosuit_grapplingHook_geo");
            GameObject leftGrapplinHand = leftArmRig.FindChild("exosuit_grapplingHook_hand_geo");
            GameObject leftHandArm = leftArmRig.FindChild("exosuit_hand_geo");
            GameObject leftPropulsionArm = leftArmRig.FindChild("exosuit_propulsion_geo");

            // Right torpedo arm
            if (arm.CompareTo("0") != 0)
            {
                List<Renderer> leftTorpedoArmRenderers = new List<Renderer>();
                leftTorpedoArm.GetComponentsInChildren<Renderer>(leftTorpedoArmRenderers);
                if (!leftTorpedoArmRenderers.Contains(leftTorpedoArm.GetComponent<Renderer>()))
                    leftTorpedoArmRenderers.Add(leftTorpedoArm.GetComponent<Renderer>());
                foreach (Renderer rend in leftTorpedoArmRenderers)
                {
                    rend.enabled = false;
                }
            }
            else
            {
                List<Renderer> renderers = new List<Renderer>();
                leftTorpedoArm.GetComponentsInChildren<Renderer>(renderers);
                if (!renderers.Contains(leftTorpedoArm.GetComponent<Renderer>()))
                    renderers.Add(leftTorpedoArm.GetComponent<Renderer>());
                foreach (Renderer rend in renderers)
                {
                    rend.enabled = true;
                }
            }

            // Right drill arm
            leftDrillArm.GetComponent<Renderer>().enabled = (arm.CompareTo("1") == 0);

            // Right grapplin arm
            if (arm.CompareTo("2") != 0)
            {
                List<Renderer> rightGrapplinArmRenderers = new List<Renderer>();
                leftGrapplinHand.GetComponentsInChildren<Renderer>(rightGrapplinArmRenderers);
                if (!rightGrapplinArmRenderers.Contains(leftGrapplinHand.GetComponent<Renderer>()))
                    rightGrapplinArmRenderers.Add(leftGrapplinHand.GetComponent<Renderer>());
                foreach (Renderer rend in rightGrapplinArmRenderers)
                {
                    rend.enabled = false;
                }
                leftGrapplinArm.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                leftGrapplinArm.GetComponent<Renderer>().enabled = true;
                List<Renderer> renderers = new List<Renderer>();
                leftGrapplinHand.GetComponentsInChildren<Renderer>(renderers);
                if (!renderers.Contains(leftGrapplinHand.GetComponent<Renderer>()))
                    renderers.Add(leftGrapplinHand.GetComponent<Renderer>());
                foreach (Renderer rend in renderers)
                {
                    rend.enabled = true;
                }
            }

            // Right propulsion arm
            leftPropulsionArm.GetComponent<Renderer>().enabled = (arm.CompareTo("3") == 0);

            // Right hand arm
            leftHandArm.GetComponent<Renderer>().enabled = (arm.CompareTo("4") == 0);
        }

        // Save seamoth doll state
        public void OnProtoSerialize(ProtobufSerializer serializer)
        {
            string saveFolder = FilesHelper.GetSaveFolderPath();
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);
            
            GameObject rightArm = this.gameObject.FindChild("prawnsuit").FindChild("ExosuitArmRight");
            GameObject rightArmRig = rightArm.FindChild("exosuit_01_armRight 1").FindChild("ArmRig 1");
            GameObject rightTorpedoArm = rightArmRig.FindChild("exosuit_arm_torpedoLauncher_geo 1");
            GameObject rightDrillArm = rightArmRig.FindChild("exosuit_drill_geo 1");
            GameObject rightGrapplinArm = rightArmRig.FindChild("exosuit_grapplingHook_geo 1");
            GameObject rightPropulsionArm = rightArmRig.FindChild("exosuit_propulsion_geo 1");

            string state = "";

            if (rightTorpedoArm.GetComponent<Renderer>().enabled)
                state = "0";
            else if (rightDrillArm.GetComponent<Renderer>().enabled)
                state = "1";
            else if (rightGrapplinArm.GetComponent<Renderer>().enabled)
                state = "2";
            else if (rightPropulsionArm.GetComponent<Renderer>().enabled)
                state = "3";
            else // Right hand arm
                state = "4";

            GameObject leftArm = this.gameObject.FindChild("prawnsuit").FindChild("ExosuitArmLeft");
            GameObject leftArmRig = leftArm.FindChild("exosuit_01_armRight").FindChild("ArmRig");
            GameObject leftTorpedoArm = leftArmRig.FindChild("exosuit_arm_torpedoLauncher_geo");
            GameObject leftDrillArm = leftArmRig.FindChild("exosuit_drill_geo");
            GameObject leftGrapplinArm = leftArmRig.FindChild("exosuit_grapplingHook_geo");
            GameObject leftPropulsionArm = leftArmRig.FindChild("exosuit_propulsion_geo");

            if (leftTorpedoArm.GetComponent<Renderer>().enabled)
                state += "0";
            else if (leftDrillArm.GetComponent<Renderer>().enabled)
                state += "1";
            else if (leftGrapplinArm.GetComponent<Renderer>().enabled)
                state += "2";
            else if (leftPropulsionArm.GetComponent<Renderer>().enabled)
                state += "3";
            else // Right hand arm
                state += "4";

            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            File.WriteAllText(Path.Combine(saveFolder, "prawnsuitdoll_" + id.Id + ".txt"), state);
        }

        // Load seamoth doll state
        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            string filePath = Path.Combine(FilesHelper.GetSaveFolderPath(), "prawnsuitdoll_" + id.Id + ".txt");
            if (File.Exists(filePath))
            {
                string state = File.ReadAllText(filePath).Trim();
                if (state.Length == 2)
                {
                    string rightArmState = state.Substring(0, 1);
                    switch (rightArmState)
                    {
                        case "0": // Torpedo arm
                            SetRightArm(this.gameObject, "0");
                            break;
                        case "1": // Drill arm
                            SetRightArm(this.gameObject, "1");
                            break;
                        case "2": // Grapplin arm
                            SetRightArm(this.gameObject, "2");
                            break;
                        case "3": // Propulsion arm
                            SetRightArm(this.gameObject, "3");
                            break;
                        default: // Right hand arm
                            SetRightArm(this.gameObject, "4");
                            break;
                    }

                    string leftArmState = state.Substring(1, 1);
                    switch (leftArmState)
                    {
                        case "0": // Torpedo arm
                            SetLeftArm(this.gameObject, "0");
                            break;
                        case "1": // Drill arm
                            SetLeftArm(this.gameObject, "1");
                            break;
                        case "2": // Grapplin arm
                            SetLeftArm(this.gameObject, "2");
                            break;
                        case "3": // Propulsion arm
                            SetLeftArm(this.gameObject, "3");
                            break;
                        default: // Right hand arm
                            SetLeftArm(this.gameObject, "4");
                            break;
                    }
                }
            }
        }
    }
}
