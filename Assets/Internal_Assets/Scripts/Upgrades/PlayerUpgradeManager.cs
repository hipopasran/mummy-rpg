using System;
using System.Collections.Generic;
using System.Linq;
using TriInspector;
using UnityEngine;

namespace Secret
{
    public class PlayerUpgradeManager : MonoBehaviour
    {
        public static PlayerUpgradeManager Instance;

        [Title("Upgrade Button")]
        [SerializeField] private GameObject _avliableUpgradeCircle;

        [Title("Upgrades Link")]
        [SerializeField] private List<CurrentUpgradeState> _currentUpgrades;
        [SerializeField] private List<Upgrade> _upgrades;

        #region Buy Methods

        public void BuyUpgradeByType(UpgradeType upgradeType)
        {
            var upgrade = _currentUpgrades.FirstOrDefault(x => x.UpgradeType == upgradeType);
            upgrade.CurrentUpgradeIndex += 1;

            var currentUpgrade = GetCurrentValueByType(upgradeType);
            
            PlayerCurrentParams.Instance.UpdateValueParam(upgrade.UpgradeType, currentUpgrade);
        }

        #endregion

        #region Get Methods
        
        public Upgrade GetUpgradeByType(UpgradeType upgradeType)
        {
            var upgrade = _upgrades.FirstOrDefault(x => x.UpgradeType == upgradeType);
            return upgrade;
        }

        public int GetCurrentUpgradeStateByType(UpgradeType upgradeType)
        {
            var upg = _currentUpgrades.FirstOrDefault(x => x.UpgradeType == upgradeType);
            if (upg != null)
            {
                return upg.CurrentUpgradeIndex + 1;
            }
            else
            {
                return 0;
            }
        }

        public float GetCurrentValueByType(UpgradeType upgradeType)
        {
            var upg = _currentUpgrades.FirstOrDefault(x => x.UpgradeType == upgradeType);
            if (upg != null)
            {
                if (upg.CurrentUpgradeIndex < 0)
                {
                    var upgrade = _upgrades.FirstOrDefault(x => x.UpgradeType == upgradeType);
                    return upgrade.StartValue;
                }
                else
                {
                    var upgrade = _upgrades.FirstOrDefault(x => x.UpgradeType == upgradeType);
                    if (upg.CurrentUpgradeIndex >= upgrade.Levels.Count)
                    {
                        return 9999;
                    }
                    else
                    {
                        return upgrade.Levels[upg.CurrentUpgradeIndex].Value;
                    }
                }
            }

            return 9999;
        }

        public float GetNextValueByType(UpgradeType upgradeType)
        {
            var upg = _currentUpgrades.FirstOrDefault(x => x.UpgradeType == upgradeType);
            if (upg != null)
            {
                var upgrade = _upgrades.FirstOrDefault(x => x.UpgradeType == upgradeType);
                if (upg.CurrentUpgradeIndex >= upgrade.Levels.Count - 1)
                {
                    return 9999;
                }

                return upgrade.Levels[upg.CurrentUpgradeIndex + 1].Value;
            }

            return 9999;
        }
        
        #endregion

        private void Init()
        {
            PlayerResourceStats.Instance.OnAddResource += UpdateAvaliableUpgrade;
        }

        private void UpdateAvaliableUpgrade(ResourcePack resourcePack)
        {
            var haveResourcesSome = false;
            foreach (var current in _currentUpgrades)
            {
                var upgrade = _upgrades.FirstOrDefault(x => x.UpgradeType == current.UpgradeType);
                if(upgrade == null) continue;

                if (current.CurrentUpgradeIndex < upgrade.Levels.Count-1)
                {
                    var cost = upgrade.Levels[current.CurrentUpgradeIndex + 1].Cost.Resources;
                    bool haveResources = true;
                    foreach (var res in cost)
                    {
                        var resPack = PlayerResourceStats.Instance.GetResourceByType(res.ResourceType);
                        if ( resPack != null && resPack.Value >= res.Value)
                        {
                            continue;
                        }
                        else
                        {
                            haveResources = false;
                        }
                    }

                    if (haveResources)
                    {
                        haveResourcesSome = true;
                    }
                }
            }

            if (haveResourcesSome)
            {
                _avliableUpgradeCircle.SetActive(true);
            }
            else
            {
                _avliableUpgradeCircle.SetActive(false);
            }
            
        }
        
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
            if(PlayerResourceStats.Instance) PlayerResourceStats.Instance.OnAddResource -= UpdateAvaliableUpgrade;
        }
    }

    [Serializable]
    public class CurrentUpgradeState
    {
        public int CurrentUpgradeIndex;
        public UpgradeType UpgradeType;
    }
}
