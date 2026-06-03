using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TriInspector;

namespace Secret
{
    public class PlayerLiveStats : MonoBehaviour
    {
        public static PlayerLiveStats Instance;

        [Title("Exp")]
        [SerializeField] private float _expCurrent;
        [SerializeField] private float _expMax;

        [Title("Cargo")]
        [SerializeField] private int _cargoCurrent;
        [SerializeField] private int _cargoMax;

        [Title("Resources In Cargo")] 
        [SerializeField] private List<ResourcePack> _cargoResources;

        public float ExpCurrent => _expCurrent;
        public float ExpMax => _expMax;

        public int CargoCurrent => _cargoCurrent;
        public int CargoMax => _cargoMax;

        public bool IsCargoFull => _cargoCurrent >= _cargoMax;

        public bool IsHaveCargoPlace(int cargoNeed)
        {
            return _cargoCurrent + cargoNeed <= _cargoMax;
        }

        public void AddExp(float exp)
        {
            _expCurrent += exp;
        }

        public void AddCargo(int cargo)
        {
            _cargoCurrent += cargo;
        }

        public void AddResToCargo(List<ResourcePack> resources)
        {
            foreach (var res in resources)
            {
                var x = _cargoResources.FirstOrDefault(x => x.ResourceType == res.ResourceType);
                if (x != null)
                {
                    x.Value += res.Value;
                }
                else
                {
                    _cargoResources.Add(res);
                }
            }
        }
        
        private void Awake()
        {
            Instance = this;
        }
    }
}
