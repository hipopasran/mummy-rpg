using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Secret
{
    public class PlayerUpgradeManager : MonoBehaviour
    {
        public static PlayerUpgradeManager Instance;
        [SerializeField] private List<CurrentUpgradeState> _currentUpgrades;
        [SerializeField] private List<Upgrade> _upgrades;

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
                if (upg.CurrentUpgradeIndex >= upgrade.Levels.Count)
                {
                    return 9999;
                }

                return upgrade.Levels[upg.CurrentUpgradeIndex + 1].Value;
            }

            return 9999;
        }

        private void Awake()
        {
            Instance = this;
        }
    }

    [Serializable]
    public class CurrentUpgradeState
    {
        public int CurrentUpgradeIndex;
        public UpgradeType UpgradeType;
    }
}
