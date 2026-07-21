using System;
using TMPro;
using UnityEngine;

namespace Secret
{
    public class PlayerUpgradeButtonController : MonoBehaviour
    {
        [SerializeField] private UpgradeType _upgradeType;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _currentValueText;
        [SerializeField] private TextMeshProUGUI _nextValueText;
        [SerializeField] private TextMeshProUGUI _mainResCostText;

        private void OnEnable()
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            var upgrade = PlayerUpgradeManager.Instance.GetUpgradeByType(_upgradeType);
            var currentLevel = PlayerUpgradeManager.Instance.GetCurrentUpgradeStateByType(_upgradeType);
            var currentValue = PlayerUpgradeManager.Instance.GetCurrentValueByType(_upgradeType);
            var nextValue = PlayerUpgradeManager.Instance.GetNextValueByType(_upgradeType);

            _levelText.text = currentLevel + "/" + upgrade.Levels.Count;
            _currentValueText.text = ValueStringHelper.ScoreShow(currentValue);
            _nextValueText.text = ValueStringHelper.ScoreShow(nextValue);
            
            UpdateCost(currentLevel, upgrade);
        }

        private void UpdateCost(int currentLevel, Upgrade upgrade)
        {
            _mainResCostText.text = ValueStringHelper.ScoreShow(upgrade.Levels[currentLevel].Cost.Resources[0].Value);
        }
    }
}
