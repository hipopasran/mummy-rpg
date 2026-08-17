using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TriInspector;
using Scellecs.Morpeh;

namespace Secret
{
    public class PlayerCargoManager : MonoBehaviour
    {
        public static PlayerCargoManager Instance;
        
        private Request<CargoClearRequest> _cargoClearRequest;
        private Request<CargoUpgradeRequest> _cargoUpdateRequest;

        [SerializeField] private ResourceLibrary _resourceLibrary;
        [SerializeField] private Transform _playerLink;
        [SerializeField] private ParticleSystem _eatParticle;

        [Title("Cargo")]
        [SerializeField] private int _cargoCurrent;
        [SerializeField] private int _cargoMax;

        [Title("Cargo Visual")] 
        [SerializeField] private Transform _cargoVisualRoot;
        [SerializeField] private CargoResourceBlock _resourceBlock;
        [SerializeField] private List<CargoResourceBlock> _resourceBlocks;
        [SerializeField] private CargoView _cargoView;

        [Title("Resources In Cargo")] 
        [SerializeField] private List<ResourcePack> _cargoResources;
        

        public int CargoCurrent => _cargoCurrent;
        public int CargoMax => _cargoMax;

        public Transform PlayerLink => _playerLink;

        public bool IsCargoFull => _cargoCurrent >= CargoMax;

        public bool IsHaveCargoPlace(int cargoNeed)
        {
            return _cargoCurrent + cargoNeed <= CargoMax;
        }

        public void PlayEatParticle()
        {
            _eatParticle.Play();
        }

        public void AddCargo(int cargo)
        {
            _cargoCurrent += cargo;
        }

        public void ClearCargo()
        {
            foreach (var res in _cargoResources)
            {
                PlayerResourceStats.Instance.AddResources(res);
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
            _cargoUpdateRequest = World.Default.GetRequest<CargoUpgradeRequest>();
            
        }

        private void Start()
        {
            PlayerCurrentParams.Instance.OnCargoUpgrade += CargoUpdate;
            _cargoMax = PlayerCurrentParams.Instance.Cargo;
            _cargoView.ResetCargo();
        }

        private void OnDisable()
        {
            if(PlayerCurrentParams.Instance != null) PlayerCurrentParams.Instance.OnCargoUpgrade += CargoUpdate;
        }

        private void CargoUpdate()
        {
            _cargoMax = PlayerCurrentParams.Instance.Cargo;
            _cargoUpdateRequest.Publish(new CargoUpgradeRequest());
        }
    }
}
