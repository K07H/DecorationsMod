using System;
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
            if (this.gameObject != null && this.gameObject.transform != null)
            {
                try
                {
                    foreach (Transform current in this.gameObject.transform)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(current.name) && current.name.StartsWith("Sign(Clone)") && current.gameObject != null)
                                GameObject.DestroyImmediate(current.gameObject);
                        }
                        catch (Exception) { }
                    }
                }
                catch (Exception) { }
            }
        }
    }
}
