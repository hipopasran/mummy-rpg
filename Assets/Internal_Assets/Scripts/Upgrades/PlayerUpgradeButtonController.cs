using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Secret
{
    public class PlayerUpgradeButtonController : MonoBehaviour
    {
        [SerializeField] private UpgradeType _upgradeType;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _currentValueText;
        [SerializeField] private TextMeshProUGUI _nextValueText;
        [SerializeField] private TextMeshProUGUI _mainResCostText;
        
        [SerializeField] private GameObject _arrowValues;
        [SerializeField] private Button _buttonBuy;


        public void TryBuyUpgrade()
        {

        }

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

            if (nextValue >= 9999)
            {
                _levelText.text = "MAX";
                _currentValueText.text = ValueStringHelper.ScoreShow(currentValue);
                _nextValueText.text = "";
                _arrowValues.SetActive(false);
                _buttonBuy.gameObject.SetActive(false);
            }
            else
            {
                _levelText.text = currentLevel + "/" + upgrade.Levels.Count;
                _currentValueText.text = ValueStringHelper.ScoreShow(currentValue);
                _nextValueText.text = ValueStringHelper.ScoreShow(nextValue);
            
                UpdateCost(currentLevel, upgrade);
            }
        }

        private void UpdateCost(int currentLevel, Upgrade upgrade)
        {
            _mainResCostText.text = ValueStringHelper.ScoreShow(upgrade.Levels[currentLevel].Cost.Resources[0].Value);
            var res = PlayerResourceStats.Instance.GetResourceByType(upgrade.Levels[currentLevel].Cost.Resources[0]
                .ResourceType);
            if (res.Value < upgrade.Levels[currentLevel].Cost.Resources[0].Value)
            {
                _buttonBuy.interactable = false;
                _mainResCostText.color = Color.red;
            }
            else
            {
                _buttonBuy.interactable = true;
                _mainResCostText.color = Color.white;
            }
        }
    }
}
