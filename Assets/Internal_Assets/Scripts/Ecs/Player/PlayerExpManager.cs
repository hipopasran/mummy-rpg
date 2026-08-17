using System;
using UnityEngine;
using TriInspector;

namespace Secret
{
    public class PlayerExpManager : MonoBehaviour
    {
        public static PlayerExpManager Instance;
        
        [Title("Exp")]
        [SerializeField] private float _expCurrent;
        [SerializeField] private float _expMax;
        
        public float ExpCurrent => _expCurrent;
        public float ExpMax => _expMax;

        #region Init
        
        private void Awake()
        {
            Instance = this;
        }
        
        #endregion
        
        public void AddExp(float exp)
        {
            _expCurrent += exp;
        }
    }
}
