using System;
using System.Collections.Generic;
using UnityEngine;

namespace Secret
{
    public class FarmManager : MonoBehaviour
    {
        [SerializeField] private int _farmLevelIncome;
        [SerializeField] private int _farmLevelCapacity;
        
        [SerializeField] private FarmUpgrade _incomeUpgrade;
        [SerializeField] private FarmUpgrade _capacityUpgrade;
        
        [SerializeField] private float _currentIncome;
        [SerializeField] private float _currentCapacity;
        [SerializeField] private float _currentValue;

        public float Income => _currentIncome;
        public float Capacity => _currentCapacity;
        public float CurrentValue => _currentValue;

        public void AddValue(float value)
        {
            _currentValue += value;
            if (_currentValue >= _currentCapacity) _currentValue = _currentCapacity;
        }

        public void ResetValue()
        {
            _currentValue = 0;
        }

        public void ButUpgradeByType(FarmUpgradeType upgradeType)
        {
            if (upgradeType == FarmUpgradeType.Income)
            {
                _farmLevelIncome += 1;
                _currentIncome = _incomeUpgrade.Levels[_farmLevelIncome].Value;
            }

            if (upgradeType == FarmUpgradeType.Capacity)
            {
                _farmLevelCapacity += 1;
                _currentCapacity = _capacityUpgrade.Levels[_farmLevelCapacity].Value;
            }
        }

        public FarmUpgrade GetUpgradeByType(FarmUpgradeType upgradeType)
        {
            if (upgradeType == FarmUpgradeType.Income)
                return _incomeUpgrade;
            else
                return _capacityUpgrade;
        }

        public int GetCurrentUpgradeStateByType(FarmUpgradeType upgradeType)
        {
            if (upgradeType == FarmUpgradeType.Income)
                return _farmLevelIncome;
            else
                return _farmLevelCapacity;
        }

        public float GetCurrentValueByType(FarmUpgradeType upgradeType)
        {
            if (upgradeType == FarmUpgradeType.Income)
            {
                if (_farmLevelIncome < 0)
                {
                    return _incomeUpgrade.StartValue;
                }
                else
                {
                    if (_farmLevelIncome >= _incomeUpgrade.Levels.Count)
                    {
                        return 9999;
                    }
                    else
                    {
                        return _incomeUpgrade.Levels[_farmLevelIncome].Value;
                    }
                }
            }
            else
            {
                if (_farmLevelCapacity < 0)
                {
                    return _incomeUpgrade.StartValue;
                }
                else
                {
                    if (_farmLevelCapacity >= _capacityUpgrade.Levels.Count)
                    {
                        return 9999;
                    }
                    else
                    {
                        return _capacityUpgrade.Levels[_farmLevelCapacity].Value;
                    }
                }
            }
        }

        public float GetNextValueByType(FarmUpgradeType upgradeType)
        {
            if (upgradeType == FarmUpgradeType.Income)
            {
                if (_farmLevelIncome >= _incomeUpgrade.Levels.Count - 1)
                {
                    return 9999;
                }

                return _incomeUpgrade.Levels[_farmLevelIncome + 1].Value;
            }
            else
            {
                if (_farmLevelCapacity >= _capacityUpgrade.Levels.Count - 1)
                {
                    return 9999;
                }

                return _capacityUpgrade.Levels[_farmLevelCapacity + 1].Value;
            }
        }

        private void Awake()
        {
            Load();
        }

        private void Start()
        {
            
        }

        private void Load()
        {
            
        }
    }
}