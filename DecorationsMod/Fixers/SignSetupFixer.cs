using UnityEngine;

namespace DecorationsMod.Fixers
{
    public class SignSetupFixer : MonoBehaviour, IProtoEventListener
    {
        public void OnProtoSerialize(ProtobufSerializer serializer)
        {
        }

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            foreach (Transform current in this.gameObject.transform)
            {
                if (current.name.CompareTo("Sign(Clone)") == 0)
                    GameObject.DestroyImmediate(current.gameObject);
            }
        }
    }
}
