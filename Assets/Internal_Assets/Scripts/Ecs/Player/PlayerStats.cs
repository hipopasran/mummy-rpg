using UnityEngine;
using TriInspector;

namespace Secret
{
    public class PlayerStats : MonoBehaviour
    {
        public static PlayerStats Instance;

        [Title("Exp")]
        [SerializeField] private float _expCurrent;
        [SerializeField] private float _expMax;

        [Title("Cargo")]
        [SerializeField] private int _cargoCurrent;
        [SerializeField] private int _cargoMax;

        public float ExpCurrent => _expCurrent;
        public float ExpMax => _expMax;

        public int CargoCurrent => _cargoCurrent;
        public int CargoMax => _cargoMax;

        public void AddExp(float exp)
        {
            _expCurrent += exp;
        }

        public void AddCargo(int cargo)
        {
            _cargoCurrent += cargo;
        }
        
        private void Awake()
        {
            Instance = this;
        }
    }
}
