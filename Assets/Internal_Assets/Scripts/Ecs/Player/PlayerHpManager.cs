using System;
using UnityEngine;

namespace Secret
{
    public class PlayerHpManager : MonoBehaviour
    {
        private static string PlayerHpKey = "PlayerHpKey";

        public static PlayerHpManager Instance;

        public Action OnPlayerDeath;

        [SerializeField] private float _currentHp;
        [SerializeField] private float _maxHp;
        [SerializeField] private HpView _hpView;

        public float CurrentHp => _currentHp;
        public float MaxHp => _maxHp;

        public void ResetHp()
        {
            _currentHp = _maxHp;
            _hpView.UpdateValues();
        }

        public void UpgradeHp(float value)
        {
            if (_currentHp >= _maxHp)
            {
                _currentHp = value;
            }

            _maxHp = value;
            
            _hpView.UpdateValues();
        }

        public void RemoveHp(float value)
        {
            _currentHp -= value;
            
            CheckPlayerDeath();
        }

        private void CheckPlayerDeath()
        {
            if (_currentHp <= 0)
            {
                OnPlayerDeath?.Invoke();
            }
        }

        #region Unity Meth
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Init();
        }

        private void OnDestroy()
        {
            Save();
        }

        private void OnDisable()
        {
            Save();
        }
        
        #endregion

        #region Save/load
        
        private void Init()
        {
            _maxHp = PlayerCurrentParams.Instance.Hp;
            Load();
            
            _hpView.UpdateValues();
        }

        private void Load()
        {
            _currentHp = PlayerPrefs.HasKey(PlayerHpKey) ? PlayerPrefs.GetFloat(PlayerHpKey) : _maxHp;
        }

        private void Save()
        {
            PlayerPrefs.SetFloat(PlayerHpKey, _currentHp);
            PlayerPrefs.Save();
        }
        
        #endregion
    }
}
