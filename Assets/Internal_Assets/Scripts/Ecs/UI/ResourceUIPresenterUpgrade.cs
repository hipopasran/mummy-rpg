using System;
using UnityEngine;
using TMPro;

namespace Secret
{
    public class ResourceUIPresenterUpgrade : MonoBehaviour
    {
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private GameObject _root;
        [SerializeField] private TextMeshProUGUI _valueText;

        [SerializeField] private bool _showIfZero;
        
        private void OnEnable()
        {
            PlayerResourceStats.Instance.OnAddResource += TryCheckValueChange;
            
            UpdateResourceStart();
        }

        private void OnDisable()
        {
            if (PlayerResourceStats.Instance) PlayerResourceStats.Instance.OnAddResource -= TryCheckValueChange;
        }

        private void TryCheckValueChange(ResourcePack res)
        {
            if (res.ResourceType == _resourceType)
            {
                UpdateText(res.Value);
            }
        }
        
        private void UpdateText(double value)
        {
            _valueText.text = ValueStringHelper.ScoreShow(value);
        }
        
        private void UpdateResourceStart()
        {
            var resPack = PlayerResourceStats.Instance.GetResourceByType(_resourceType);
            if (resPack != null)
            {
                UpdateText(resPack.Value);
            }
            else
            {
                if (_showIfZero)
                {
                    UpdateText(0);
                }
                else
                {
                    _root.SetActive(false);   
                }
            }
        }

    }
}
