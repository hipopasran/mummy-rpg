using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TriInspector;
using UnityEditor.Localization.Plugins.XLIFF.V12;

namespace Secret
{
    public class PlayerOveralStats : MonoBehaviour
    {
        public static PlayerOveralStats Instance;
        
        [Title("Resources In Cargo")] 
        [SerializeField] private List<ResourcePack> _resources;

        public void AddResources(ResourcePack resource)
        {
            var x = _resources.FirstOrDefault(x => x.ResourceType == resource.ResourceType);
            if (x == null)
            {
                AddNewResources(resource);
            }
            else
            {
                AddExistingResource(x,resource);
            }
            
        }

        private void AddExistingResource(ResourcePack playerRes, ResourcePack resource)
        {
            playerRes.Value += resource.Value;
        }

        private void AddNewResources(ResourcePack resource)
        {
            _resources.Add(resource);
        }
        
        // Init
        private void Awake()
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
    }
}
