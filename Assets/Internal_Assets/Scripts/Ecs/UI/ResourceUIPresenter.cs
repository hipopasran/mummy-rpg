using System;
using TMPro;
using UnityEngine;

namespace Secret
{
    public class ResourceUIPresenter : MonoBehaviour
    {
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private TextMeshProUGUI _valueText;
        
        private void Start()
        {
            PlayerOveralStats.Instance.OnAddResource += TryCheckValueChange;
            
            UpdateResourceStart();
        }

        private void OnDestroy()
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
                UpdateText(0);
            }
        }
    }
}
