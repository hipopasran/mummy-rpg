using System;
using UnityEngine;

namespace Secret
{
    [Serializable]
    public class ResourcePack
    {
        public ResourceType ResourceType;
        [SerializeField] public float Value;
        public Sprite icon;
    }
}
