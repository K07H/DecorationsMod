using DecorationsMod.Controllers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsMod.Fixers
{
    public class PictureFrameFixer
    {
        public static void OnHandHover_Postfix(PictureFrame __instance, HandTargetEventData eventData)
        {
            if (!__instance.enabled)
                return;
            if (__instance.gameObject.name.StartsWith("CustomPictureFrame(Clone)", true, CultureInfo.InvariantCulture))
            {
                HandReticle.main.SetTextRaw(HandReticle.TextType.Hand, LanguageHelper.GetFriendlyWord(ConfigSwitcher.UseCompactTooltips ? "CustomPictureFrameTooltipCompact" : "CustomPictureFrameTooltip"));
            }
            // else, we are in regular PictureFrame
            return;
        }

        public static bool OnHandClick_Prefix(PictureFrame __instance, HandTargetEventData eventData)
        {
            if (!__instance.enabled)
                return true;
            // If current item is one of our Customizable Picture Frame.
            if (__instance.gameObject.name.StartsWith("CustomPictureFrame(Clone)", true, CultureInfo.InvariantCulture))
            {

                // Minimum CustomPictureFrame size = normal size / minSizeRatio
                float minSizeRatio = 4.0f;

                CustomPictureFrameController cpfController = __instance.gameObject.GetComponent<CustomPictureFrameController>();
                GameObject frame = __instance.gameObject.FindChild("mesh");
                GameObject poster = __instance.gameObject.FindChild("poster_decorations(Clone)");
                GameObject posterModel = poster.FindChild("model");
                GameObject magnetModel = posterModel.FindChild("poster_kitty");
                MeshRenderer magnetRenderer = magnetModel.GetComponent<MeshRenderer>();
                GameObject bgBisModel = posterModel.FindChild("poster_background_bis");
                MeshRenderer bgBisRenderer = bgBisModel.GetComponent<MeshRenderer>();
                GameObject bgPivotModel = posterModel.FindChild("poster_background_pivot");
                MeshRenderer bgPivotRenderer = bgPivotModel.GetComponent<MeshRenderer>();

                if (Input.GetKey(KeyCode.R))
                {
                    GameObject pictureFrame = frame.FindChild("submarine_Picture_Frame");
                    MeshRenderer frameRenderer = pictureFrame.GetComponent<MeshRenderer>();

                    // Rotate frame
                    if (cpfController.Flipped)
                        frame.transform.localEulerAngles = new Vector3(frame.transform.localEulerAngles.x, frame.transform.localEulerAngles.y, frame.transform.localEulerAngles.z + 90f);
                    else
                        frame.transform.localEulerAngles = new Vector3(frame.transform.localEulerAngles.x, frame.transform.localEulerAngles.y, frame.transform.localEulerAngles.z - 90f);

                    // Adjust poster style
                    if (!frameRenderer.enabled)
                    {
                        bgBisRenderer.enabled = !bgBisRenderer.enabled;
                        bgPivotRenderer.enabled = !bgPivotRenderer.enabled;
                        // Adjust magnet pos
                        if (bgPivotRenderer.enabled)
                            magnetModel.transform.localPosition = new Vector3(0f, -0.0115f, 0f);
                        else
                            magnetModel.transform.localPosition = new Vector3(0f, 0f, 0f);
                    }
                    else
                    {
                        bgBisRenderer.enabled = false;
                        bgPivotRenderer.enabled = false;
                    }

                    // Toogle flip
                    cpfController.Flipped = !cpfController.Flipped;

                    // Rotate collider
                    GameObject trigger = __instance.gameObject.FindChild("Trigger");
                    BoxCollider collider = trigger.GetComponent<BoxCollider>();
                    collider.size = new Vector3(collider.size.y, collider.size.x, collider.size.z);

                    // Rotate image
                    __instance.imageRenderer.transform.localScale = new Vector3(__instance.imageRenderer.transform.localScale.y, __instance.imageRenderer.transform.localScale.x, __instance.imageRenderer.transform.localScale.z);

                    // Rotate constructable bounds extents
                    var constructableBounds = __instance.gameObject.GetComponent<ConstructableBounds>();
                    constructableBounds.bounds.extents = new Vector3(constructableBounds.bounds.extents.y, constructableBounds.bounds.extents.x, constructableBounds.bounds.extents.z);

                    // Refresh picture
                    PictureFrameEnumHelper.SetStateMethod.Invoke(__instance, new object[] { PictureFrameEnumHelper.ThumbnailEnumValue });
                    PictureFrameEnumHelper.SetStateMethod.Invoke(__instance, new object[] { PictureFrameEnumHelper.FullEnumValue });

                    return false;
                }
                else if (Input.GetKey(KeyCode.F))
                {
                    // Get objects.
                    GameObject pictureFrame = frame.FindChild("submarine_Picture_Frame");
                    GameObject frameButton = pictureFrame.FindChild("submarine_Picture_Frame_button");
                    MeshRenderer frameRenderer = pictureFrame.GetComponent<MeshRenderer>();
                    MeshRenderer buttonRenderer = frameButton.GetComponent<MeshRenderer>();
                    GameObject bgModel = posterModel.FindChild("poster_background");
                    MeshRenderer bgRenderer = bgModel.GetComponent<MeshRenderer>();

                    // Switch frame style.
                    if (frameRenderer.enabled) // If we're in picture frame mode, switch to poster mode.
                    {
                        // Disable picture frame
                        frameRenderer.enabled = false;
                        buttonRenderer.enabled = false;
                        // Enable poster magnet
                        magnetRenderer.enabled = true;
                        bgRenderer.enabled = false;
                        // Enable bg bis
                        bgBisRenderer.enabled = !cpfController.Flipped;
                        bgPivotRenderer.enabled = cpfController.Flipped;
                        // Adjust magnet pos.
                        if (cpfController.Flipped)
                            magnetModel.transform.localPosition = new Vector3(0f, -0.0115f, 0f);
                        else
                            magnetModel.transform.localPosition = new Vector3(0f, 0f, 0f);
                        // Adjust image pos.
                        __instance.imageRenderer.transform.localPosition = new Vector3(__instance.imageRenderer.transform.localPosition.x, __instance.imageRenderer.transform.localPosition.y, __instance.imageRenderer.transform.localPosition.z - 0.0045f);
                        // Display message to user.
                        ErrorMessage.AddDebug("Picture Frame: Poster mode enabled.");
                    }
                    else if (magnetRenderer.enabled) // Else if we're in poster mode, switch to plain image mode.
                    {
                        // Disable picture frame
                        frameRenderer.enabled = false;
                        buttonRenderer.enabled = false;
                        // Disable poster magnet and poster bg
                        magnetRenderer.enabled = false;
                        bgRenderer.enabled = false;
                        // Enable bg bis
                        bgBisRenderer.enabled = !cpfController.Flipped;
                        bgPivotRenderer.enabled = cpfController.Flipped;
                        // Display message to user.
                        ErrorMessage.AddDebug("Picture Frame: No frame mode enabled.");
                    }
                    else // Else if we're if plain image mode, switch to picture frame mode.
                    {
                        // Enable picture frame
                        frameRenderer.enabled = true;
                        buttonRenderer.enabled = true;
                        // Disable poster magnet and poster bg
                        magnetRenderer.enabled = false;
                        bgRenderer.enabled = false;
                        // Disable bg bis
                        bgBisRenderer.enabled = false;
                        bgPivotRenderer.enabled = false;
                        // Adjust image pos
                        __instance.imageRenderer.transform.localPosition = new Vector3(__instance.imageRenderer.transform.localPosition.x, __instance.imageRenderer.transform.localPosition.y, __instance.imageRenderer.transform.localPosition.z + 0.0045f);
                        // Display message to user.
                        ErrorMessage.AddDebug("Picture Frame: Picture frame mode enabled.");
                    }

                    return false;
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    // CustomPictureFrame scale ratio step
                    float scaleRatio = 1.05f;

                    if (frame.transform.localScale.x >= 3.0f)
                    {
                        // Set minimum size
                        frame.transform.localScale = new Vector3(cpfController.OriginFrameScale.x * (1.0f / minSizeRatio), cpfController.OriginFrameScale.y * (1.0f / minSizeRatio), cpfController.OriginFrameScale.z);
                        poster.transform.localPosition = new Vector3(cpfController.OriginPosterPosition.x * (1.0f / minSizeRatio), cpfController.OriginPosterPosition.y * (1.0f / minSizeRatio), cpfController.OriginPosterPosition.z);
                        posterModel.transform.localPosition = new Vector3(cpfController.OriginPosterModelPosition.x * (1.0f / minSizeRatio), cpfController.OriginPosterModelPosition.y * (1.0f / minSizeRatio), cpfController.OriginPosterModelPosition.z);
                        posterModel.transform.localScale = new Vector3(cpfController.OriginPosterModelScale.x * (1.0f / minSizeRatio), cpfController.OriginPosterModelScale.y * (1.0f / minSizeRatio), cpfController.OriginPosterModelScale.z);
                        __instance.imageRenderer.transform.localScale = new Vector3(cpfController.OriginImageScale.x * (1.0f / minSizeRatio), cpfController.OriginImageScale.y * (1.0f / minSizeRatio), cpfController.OriginImageScale.z * (1.0f / minSizeRatio));
                        magnetModel.transform.localScale = new Vector3(cpfController.OriginMagnetScale.x, (cpfController.OriginMagnetScale.y * (1.0f / minSizeRatio)) + 0.1f, cpfController.OriginMagnetScale.z);
                        frame.transform.localPosition = new Vector3(cpfController.OriginFramePosition.x, cpfController.OriginFramePosition.y, cpfController.OriginFramePosition.z + 0.0001f);

                        if (cpfController.Flipped)
                        {
#if DEBUG_CUSTOM_PICTURE_FRAME
                            Logger.Debug("ENTERING FLIPPED");
                            Logger.Debug("BG BIS IS " + (bgBisRenderer.enabled ? "ENABLED" : "DISABLED"));
                            Logger.Debug("BG PIVOT IS " + (bgPivotRenderer.enabled ? "ENABLED" : "DISABLED"));
#endif
                            // Rotate image
                            __instance.imageRenderer.transform.localScale = new Vector3(__instance.imageRenderer.transform.localScale.y, __instance.imageRenderer.transform.localScale.x, __instance.imageRenderer.transform.localScale.z);

                        }
#if DEBUG_CUSTOM_PICTURE_FRAME
                        else
                        {
                            Logger.Debug("ENTERING NOT FLIPPED");
                            Logger.Debug("BG BIS IS " + (bgBisRenderer.enabled ? "ENABLED" : "DISABLED"));
                            Logger.Debug("BG PIVOT IS " + (bgPivotRenderer.enabled ? "ENABLED" : "DISABLED"));
                        }
#endif
                    }
                    else
                    {
                        // Increase size
                        frame.transform.localScale = new Vector3(frame.transform.localScale.x * scaleRatio, frame.transform.localScale.y * scaleRatio, frame.transform.localScale.z); // *= scaleRatio;
                        poster.transform.localPosition = new Vector3(poster.transform.localPosition.x, poster.transform.localPosition.y * scaleRatio, poster.transform.localPosition.z);
                        posterModel.transform.localPosition = new Vector3(posterModel.transform.localPosition.x, posterModel.transform.localPosition.y * scaleRatio, posterModel.transform.localPosition.z);
                        posterModel.transform.localScale = new Vector3(posterModel.transform.localScale.x * scaleRatio, posterModel.transform.localScale.y * scaleRatio, posterModel.transform.localScale.z); // *= scaleRatio;
                        __instance.imageRenderer.transform.localScale = new Vector3(__instance.imageRenderer.transform.localScale.x * scaleRatio, __instance.imageRenderer.transform.localScale.y * scaleRatio, __instance.imageRenderer.transform.localScale.z * scaleRatio); //*= scaleRatio;
                        magnetModel.transform.localScale = new Vector3(magnetModel.transform.localScale.x, magnetModel.transform.localScale.y * scaleRatio, magnetModel.transform.localScale.z);
                        frame.transform.localPosition = new Vector3(frame.transform.localPosition.x, frame.transform.localPosition.y, frame.transform.localPosition.z * 1.0f);
                    }

                    // Refresh picture
                    PictureFrameEnumHelper.SetStateMethod.Invoke(__instance, new object[] { PictureFrameEnumHelper.ThumbnailEnumValue });
                    PictureFrameEnumHelper.SetStateMethod.Invoke(__instance, new object[] { PictureFrameEnumHelper.FullEnumValue });

                    return false;
                }
                else if (Input.GetKey(KeyCode.G))
                {
                    PrefabIdentifier id = __instance.gameObject.GetComponent<PrefabIdentifier>();
                    if (id != null)
                    {
                        cpfController.RandomImage = !cpfController.RandomImage;
                        ErrorMessage.AddDebug("Picture Frame: Random mode " + (cpfController.RandomImage ? "enabled." : "disabled."));
                        PictureFrameEnumHelper.SetStateMethod.Invoke(__instance, new object[] { PictureFrameEnumHelper.NoneEnumValue });
                        __instance.fileName = null;
                        if (cpfController.RandomImage)
                            cpfController.SetRandomImage(id, __instance);
                    }

                    return false;
                }
                else if (Input.GetKey(KeyCode.T))
                {
                    PrefabIdentifier id = __instance.gameObject.GetComponent<PrefabIdentifier>();
                    if (id != null)
                    {
                        //cpfController.Slideshow = !cpfController.Slideshow;
                        ErrorMessage.AddDebug("Picture Frame: Slideshow mode " + (cpfController.Slideshow ? "disabled." : "enabled."));
                        if (cpfController.Slideshow)
                            cpfController.Slideshow = false;
                        else
                        {
                            PictureFrameEnumHelper.SetStateMethod.Invoke(__instance, new object[] { PictureFrameEnumHelper.NoneEnumValue });
                            __instance.fileName = null;
                            cpfController.StartSlideshow(id, __instance);
                        }
                    }

                    return false;
                }
            }
            return true;
        }
    }
}
