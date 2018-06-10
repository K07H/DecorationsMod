using UnityEngine;

namespace DecorationsMod
{
    public class Lamp_C : Constructable
    {
        public override bool SetState(bool value, bool setAmount = true)
        {
            bool retval = base.SetState(value, setAmount);
            if (this._constructed)
            {
                // Enable light
                var reactorRodLight = this.gameObject.GetComponentInChildren<Light>();
                reactorRodLight.enabled = true;
            }
            return retval;
        }
    }
}
