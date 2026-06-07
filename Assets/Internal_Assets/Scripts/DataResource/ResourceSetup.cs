using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Secret
{
    [CreateAssetMenu(fileName = "Data", menuName = "Resources/ResourceSetup", order = 1)]
    public class ResourceSetup : ScriptableObject
    {
        public ResourceType ResourceType;
        public string ResourceName;
        public Sprite Icon;
        
    }
}
