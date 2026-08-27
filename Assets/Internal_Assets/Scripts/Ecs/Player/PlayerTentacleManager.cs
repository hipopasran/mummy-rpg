using System;
using UnityEngine;

namespace Secret
{
    public class PlayerTentacleManager : MonoBehaviour
    {
        public static PlayerTentacleManager Instance;

        [SerializeField] private TentacleProvider _tentaclePrefab;
        [SerializeField] private Transform _tentacleRoot;
        [SerializeField] private PlayerDamageTrigger _playerDamageTrigger;

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            if(PlayerCurrentParams.Instance) PlayerCurrentParams.Instance.OnTentacleUpgrade -= CreateTentacle;
        }

        private void Start()
        {
            PlayerCurrentParams.Instance.OnTentacleUpgrade += CreateTentacle;
            CreateStartTentacles();
        }

        private void CreateStartTentacles()
        {
            var count = PlayerCurrentParams.Instance.Tentacle;
            for (int i = 0; i < count; i++)
            {
                CreateTentacle();
            }
        }

        private void CreateTentacle()
        {
            var newTentacle = Instantiate(_tentaclePrefab, _tentacleRoot);
            _playerDamageTrigger.AddTentacle(newTentacle);
        }
    }
}