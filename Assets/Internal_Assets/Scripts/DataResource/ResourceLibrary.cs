using System.Collections.Generic;
using UnityEngine;

namespace Secret
{

    [CreateAssetMenu(fileName = "ResourceLibrary", menuName = "Resources/ResourceLibrary", order = 1)]
    public class ResourceLibrary : ScriptableObject
    {
        public List<ResourceSetup> _resources;
    }
}
