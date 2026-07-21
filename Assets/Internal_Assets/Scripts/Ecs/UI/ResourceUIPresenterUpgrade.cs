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
        
        private void OnEnable()
        {
            PlayerOveralStats.Instance.OnAddResource += TryCheckValueChange;
            
            UpdateResourceStart();
        }

        private void OnDisable()
        {
            if (PlayerOveralStats.Instance) PlayerOveralStats.Instance.OnAddResource -= TryCheckValueChange;
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
            var resPack = PlayerOveralStats.Instance.GetResourceByType(_resourceType);
            if (resPack != null)
            {
                UpdateText(resPack.Value);
            }
            else
            {
                _root.SetActive(false);
            }
        }

    }
}
