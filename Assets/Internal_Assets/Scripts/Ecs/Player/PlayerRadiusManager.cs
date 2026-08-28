using System;
using UnityEngine;

namespace Secret
{
    public class PlayerRadiusManager : MonoBehaviour
    {
        public static PlayerRadiusManager Instance;
        
        [SerializeField] private float _startRadiusSize;
        [SerializeField] private float _startRendrerSize;
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private SpriteRenderer _renderer;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            PlayerCurrentParams.Instance.OnRadiusUpgrade += RadiusUpgrade;
            
            RadiusUpgrade();
        }

        private void OnDestroy()
        {
            if (PlayerCurrentParams.Instance) PlayerCurrentParams.Instance.OnRadiusUpgrade -= RadiusUpgrade;
        }

        private void RadiusUpgrade()
        {
            var value = PlayerCurrentParams.Instance.Radius;
            _collider.radius = value;
            var renderSize = _startRendrerSize * (value / _startRadiusSize);
            _renderer.size = new Vector2(renderSize, renderSize);
        }
    }
}
