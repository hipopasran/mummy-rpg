using UnityEngine;

namespace Secret
{
    public class PlayerStats : MonoBehaviour
    {
        public static PlayerStats Instance;

        [SerializeField] private float _expCurrent;
        [SerializeField] private float _expMax;

        public float ExpCurrent => _expCurrent;
        public float ExpMax => _expMax;

        public void AddExp(float exp)
        {
            _expCurrent += exp;
        }
        
        private void Awake()
        {
            Instance = this;
        }
    }
}
