using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TriInspector;

namespace Secret
{
    public class PlayerResourceStats : MonoBehaviour
    {
        public static PlayerResourceStats Instance;
        public Action<ResourcePack> OnAddResource;
        
        [Title("Resources In Cargo")] 
        [SerializeField] private List<ResourcePack> _resources;

        public ResourcePack GetResourceByType(ResourceType resType)
        {
            var res = _resources.FirstOrDefault(x => x.ResourceType == resType);
            return res;
        }

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
            
            OnAddResource?.Invoke(playerRes);
        }

        private void AddNewResources(ResourcePack resource)
        {
            _resources.Add(resource);
            
            OnAddResource?.Invoke(resource);
        }
        
        // Init
        private void Awake()
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
    }
}
