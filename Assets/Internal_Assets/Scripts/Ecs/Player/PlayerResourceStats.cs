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

        [Title("Resource Library")] 
        [SerializeField] private ResourceLibrary _library;

        public ResourcePack GetResourceByType(ResourceType resType)
        {
            var res = _resources.FirstOrDefault(x => x.ResourceType == resType);
            return res;
        }

        public void RemoveResource(ResourcePack resource)
        {
            var playerRes = _resources.FirstOrDefault(x => x.ResourceType == resource.ResourceType);
            if(playerRes == null) return;

            playerRes.Value -= resource.Value;

            OnAddResource?.Invoke(playerRes);
        }

        public void RemoveResourceByType(ResourceType resType, float count)
        {
            var playerRes = _resources.FirstOrDefault(x => x.ResourceType == resType);
            if(playerRes == null) return;

            playerRes.Value -= count;
            
            OnAddResource?.Invoke(playerRes);
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

        public void AddResourceByType(ResourceType resType, float count)
        {
            var x = _resources.FirstOrDefault(x => x.ResourceType == resType);
            if (x == null)
            {
                AddNewResourceByTypeAndCount(resType,count);
            }
            else
            {
                AddExistingResourceWithCount(x, count);
            }
        }

        private void AddExistingResourceWithCount(ResourcePack playerRes, float count)
        {
            playerRes.Value += count;
            
            OnAddResource?.Invoke(playerRes);
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

        private void AddNewResourceByTypeAndCount(ResourceType resType, float count)
        {
            var resSetup = _library._resources.FirstOrDefault(x => x.ResourceType == resType);
            var resPack = new ResourcePack() { ResourceType = resType, Value = count, icon = resSetup.Icon };
            _resources.Add(resPack);
            
            OnAddResource?.Invoke(resPack);
        }
        
        // Init
        private void Awake()
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
    }
}
