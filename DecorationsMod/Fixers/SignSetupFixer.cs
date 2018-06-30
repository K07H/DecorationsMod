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
                if (current.name.StartsWith("Sign(Clone)"))
                    GameObject.DestroyImmediate(current.gameObject);
            }
        }
    }
}
