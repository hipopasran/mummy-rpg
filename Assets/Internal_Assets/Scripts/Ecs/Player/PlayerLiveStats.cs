using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TriInspector;
using Scellecs.Morpeh;

namespace Secret
{
    public class PlayerLiveStats : MonoBehaviour
    {
        public static PlayerLiveStats Instance;
        
        private Request<CargoClearRequest> _cargoClearRequest;

        [SerializeField] private ResourceLibrary _resourceLibrary;
        [SerializeField] private Transform _playerLink;
        [SerializeField] private ParticleSystem _eatParticle;

        [Title("Exp")]
        [SerializeField] private float _expCurrent;
        [SerializeField] private float _expMax;

        [Title("Cargo")]
        [SerializeField] private int _cargoCurrent;
        [SerializeField] private int _cargoMax;

        [Title("Cargo Visual")] 
        [SerializeField] private Transform _cargoVisualRoot;
        [SerializeField] private CargoResourceBlock _resourceBlock;
        [SerializeField] private List<CargoResourceBlock> _resourceBlocks;

        [Title("Resources In Cargo")] 
        [SerializeField] private List<ResourcePack> _cargoResources;
        

        public float ExpCurrent => _expCurrent;
        public float ExpMax => _expMax;

        public int CargoCurrent => _cargoCurrent;
        public int CargoMax => _cargoMax;

        public Transform PlayerLink => _playerLink;

        public bool IsCargoFull => _cargoCurrent >= _cargoMax;

        public bool IsHaveCargoPlace(int cargoNeed)
        {
            return _cargoCurrent + cargoNeed <= _cargoMax;
        }

        public void PlayEatParticle()
        {
            _eatParticle.Play();
        }

        public void AddExp(float exp)
        {
            _expCurrent += exp;
        }

        public void AddCargo(int cargo)
        {
            _cargoCurrent += cargo;
        }

        public void ClearCargo()
        {
            foreach (var res in _cargoResources)
            {
                PlayerOveralStats.Instance.AddResources(res);
            }
            
            _cargoResources.Clear();

            foreach (var block in _resourceBlocks)
            {
                Destroy(block.gameObject);
            }

            _cargoCurrent = 0;
            _resourceBlocks.Clear();
            SendClearRequest();
        }

        public void AddResToCargo(List<ResourcePack> resources)
        {
            foreach (var res in resources)
            {
                var x = _cargoResources.FirstOrDefault(x => x.ResourceType == res.ResourceType);
                if (x != null)
                {
                    x.Value += res.Value;
                    AddVisualToCargo(x);
                }
                else
                {
                    _cargoResources.Add(res);
                    AddVisualToCargoNew(res);
                }
            }
        }
        
        public void SendClearRequest()
        {
            _cargoClearRequest.Publish(new CargoClearRequest());
        }

        public ResourceSetup GetResPackForLibrary(ResourceType resType)
        {
            var res = _resourceLibrary._resources.FirstOrDefault(x => x.ResourceType == resType);
            return res;
        }

        private void AddVisualToCargo(ResourcePack resource)
        {
            var block = _resourceBlocks.FirstOrDefault(x => x.ResourceType == resource.ResourceType);
            block.Setup(resource);
        }

        private void AddVisualToCargoNew(ResourcePack resource)
        {
            var res = _resourceLibrary._resources.FirstOrDefault(x => x.ResourceType == resource.ResourceType);
            resource.icon = res.Icon;
            var block = Instantiate(_resourceBlock, _cargoVisualRoot);
            block.Setup(resource);
            _resourceBlocks.Add(block);
        }
        
        private void Awake()
        {
            Instance = this;
            
            _cargoClearRequest = World.Default.GetRequest<CargoClearRequest>();
        }
    }
}
