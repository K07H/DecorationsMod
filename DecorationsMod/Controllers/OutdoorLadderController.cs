using System;
using UnityEngine;
using DecorationsMod.Fixers;

namespace DecorationsMod.Controllers
{
    public class OutdoorLadderController : HandTarget, IHandTarget
    {
        public void OnHandClick(GUIHand hand)
        {
            if (!enabled)
                return;

            GameObject model = this.gameObject.FindChild("OutdoorLadderModel");
            if (model == null)
                return;

            PrefabIdentifier pid = this.gameObject.GetComponent<PrefabIdentifier>();
            if (pid == null)
                return;

            // Update player position
            int direction = 0;
            bool inverted = false;
            if (ConstructableFixer.LadderDirections.ContainsKey(pid.Id))
            {
                direction = ConstructableFixer.LadderDirections[pid.Id].Key;
                inverted = ConstructableFixer.LadderDirections[pid.Id].Value;
            }
            Vector3 newDir;
            if (direction == 0)
                newDir = model.transform.forward;
            else
                newDir = Quaternion.Euler(0.0f, 180.0f, 0.0f) * model.transform.forward * -1.2f;
            float heightRange = inverted ? 2.9f : 2.8f;
            if (Player.main.transform.position.y < model.transform.position.y + heightRange)
                Player.main.SetPosition(new Vector3(model.transform.position.x, model.transform.position.y + heightRange, model.transform.position.z) + (newDir * -0.6f));
            else
                Player.main.SetPosition(new Vector3(model.transform.position.x, model.transform.position.y - 0.7f, model.transform.position.z) + (newDir * 0.6f));
            Player.main.OnPlayerPositionCheat();
        }

        public void OnHandHover(GUIHand hand)
        {
            if (!enabled)
                return;

            var reticle = HandReticle.main;
            reticle.SetIcon(HandReticle.IconType.Hand, 1f);
#if BELOWZERO
            reticle.SetTextRaw(HandReticle.TextType.Hand, Language.main.Get("ClimbUp"));
#else
            reticle.SetInteractText("ClimbUp");
#endif
        }
    }
}
