using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Secret
{
    public class ExchangerButtonController : MonoBehaviour
    {
        [SerializeField] private Image _iconFirst;
        [SerializeField] private TextMeshProUGUI _countFirstText;
        [SerializeField] private Image _iconSecond;
        [SerializeField] private TextMeshProUGUI _countSecondText;
        [SerializeField] private Button _button;

        [SerializeField] private ResourceType _firstType;
        [SerializeField] private int _countFirst;
        [SerializeField] private ResourceType _secondType;
        [SerializeField] private int _countSecond;

        public void Setup(Sprite First, int countFirst, ResourceType firstType, Sprite Second, int countSecond, ResourceType secondType)
        {
            _iconFirst.sprite = First;
            _countFirstText.text = ValueStringHelper.ScoreShow(countFirst);

            _iconSecond.sprite = Second;
            _countSecondText.text = ValueStringHelper.ScoreShow(countSecond);

            _firstType = firstType;
            _countFirst = countFirst;

            _secondType = secondType;
            _countSecond = countSecond;
        }

        public void CheckButton()
        {
            if(_firstType != ResourceType.Test_Debug) CheckForValue(PlayerResourceStats.Instance.GetResourceByType(_firstType));
        }

        public void OnClick()
        {
            PlayerResourceStats.Instance.RemoveResourceByType(_firstType, _countFirst);
            PlayerResourceStats.Instance.AddResourceByType(_secondType, _countSecond);
        }

        private void OnEnable()
        {
            PlayerResourceStats.Instance.OnAddResource += CheckForValue;
            if(_firstType != ResourceType.Test_Debug) CheckForValue(PlayerResourceStats.Instance.GetResourceByType(_firstType));
        }

        private void OnDisable()
        {
            PlayerResourceStats.Instance.OnAddResource -= CheckForValue;
        }

        private void CheckForValue(ResourcePack res)
        {
            if (res == null)
            {
                _button.interactable = false;
                return;
            }
            
            if (res.ResourceType != _firstType) return;
            if (res.Value < _countFirst)
            {
                _button.interactable = false;
            }
            else
            {
                _button.interactable = true;
            }
        }
    }
}
