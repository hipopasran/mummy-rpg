using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Secret
{
    public class CargoResourceBlock : MonoBehaviour
    {
        [SerializeField] private ResourceType _resType;
        [SerializeField] private Image _resIcon;
        [SerializeField] private TextMeshProUGUI _resValue;

        public ResourceType ResourceType => _resType;

        public void Setup(ResourcePack resource)
        {
            _resType = resource.ResourceType;
            // TODO: Set Icon
            _resValue.text = ValueStringHelper.ScoreShow(resource.Value);
        }
    }
}
