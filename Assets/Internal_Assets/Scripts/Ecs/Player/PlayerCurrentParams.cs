using System;
using System.Collections.Generic;
using UnityEngine;

namespace Secret
{
    public class PlayerCurrentParams : MonoBehaviour
    {
        public static PlayerCurrentParams Instance;

        public Action OnCargoUpgrade;

        #region Keys
        
        private static string DamageKey = "Damage_Param_Key";
        private static string CargoKey = "Cargo_Param_Key";
        private static string SpeedKey = "Speed_Param_Key";
        private static string RadiusKey = "Radius_Param_Key";
        private static string TentacleKey = "Tentacle_Param_Key";
        private static string HpKey = "Hp_Param_Key";
        #endregion

        [SerializeField] private float _damageValue;
        [SerializeField] private float _cargoValue;
        [SerializeField] private float _speedValue;
        [SerializeField] private float _radiusValue;
        [SerializeField] private float _tentacleCountValue;
        [SerializeField] private float _hpValue;
        
        [SerializeField] private List<Upgrade> _upgrades;

        #region GetParams

        public float Damage => _damageValue;
        public int Cargo => (int)_cargoValue;
        public float Speed => _speedValue;
        public float Radius => _radiusValue;
        public int Tentacle => (int)_tentacleCountValue;
        public float Hp => _hpValue;

        #endregion

        #region Init and destroy
        
        private void Awake()
        {
            Instance = this;
            
            LoadStartValues();
            LoadParamsFromSaves();
        }

        private void OnDestroy()
        {
            SaveCurrentParams();
        }

        private void OnDisable()
        {
            SaveCurrentParams();
        }
        
        #endregion

        public void UpdateValueParam(UpgradeType upgradeType, float value)
        {
            switch (upgradeType)
            {
                case UpgradeType.Damage:
                    _damageValue = value;
                    break;
                case UpgradeType.Cargo:
                    _cargoValue = value;
                    OnCargoUpgrade?.Invoke();
                    break;
                case UpgradeType.Health:
                    _hpValue = value;
                    PlayerHpManager.Instance.UpgradeHp(_hpValue);
                    break;
                case UpgradeType.Speed:
                    _speedValue = value;
                    break;
                case UpgradeType.AttackRadius:
                    _radiusValue = value;
                    break;
                case UpgradeType.Tentacle:
                    _tentacleCountValue = value;
                    break;
            }
        }

        #region Load/Save
        
        private void SaveCurrentParams()
        {
            PlayerPrefs.SetFloat(DamageKey, _damageValue);
            PlayerPrefs.SetFloat(CargoKey, _cargoValue);
            PlayerPrefs.SetFloat(SpeedKey, _speedValue);
            PlayerPrefs.SetFloat(RadiusKey, _radiusValue);
            PlayerPrefs.SetFloat(TentacleKey, _tentacleCountValue);
            PlayerPrefs.SetFloat(HpKey, _hpValue);
            PlayerPrefs.Save();
        }
        
        private void LoadStartValues()
        {
            foreach (var upgrade in _upgrades)
            {
                switch (upgrade.UpgradeType)
                {
                    case UpgradeType.Damage:
                        _damageValue = upgrade.StartValue;
                        break;
                    case UpgradeType.Cargo:
                        _cargoValue = upgrade.StartValue;
                        break;
                    case UpgradeType.Health:
                        _hpValue = upgrade.StartValue;
                        break;
                    case UpgradeType.Speed:
                        _speedValue = upgrade.StartValue;
                        break;
                    case UpgradeType.AttackRadius:
                        _radiusValue = upgrade.StartValue;
                        break;
                    case UpgradeType.Tentacle:
                        _tentacleCountValue = upgrade.StartValue;
                        break;
                }
            }
        }

        private void LoadParamsFromSaves()
        {
            if (PlayerPrefs.HasKey(DamageKey)) _damageValue = PlayerPrefs.GetFloat(DamageKey);
            if (PlayerPrefs.HasKey(CargoKey)) _cargoValue = PlayerPrefs.GetFloat(CargoKey);
            if (PlayerPrefs.HasKey(SpeedKey)) _speedValue = PlayerPrefs.GetFloat(SpeedKey);
            if (PlayerPrefs.HasKey(RadiusKey)) _radiusValue = PlayerPrefs.GetFloat(RadiusKey);
            if (PlayerPrefs.HasKey(TentacleKey)) _tentacleCountValue = PlayerPrefs.GetFloat(TentacleKey);
            if (PlayerPrefs.HasKey(HpKey)) _hpValue = PlayerPrefs.GetFloat(HpKey);
        }
        
        #endregion
    }
}
