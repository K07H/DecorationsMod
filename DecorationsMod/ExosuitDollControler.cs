using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod
{
    public class ExosuitDollControler : HandTarget, IHandTarget
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
    }
}
