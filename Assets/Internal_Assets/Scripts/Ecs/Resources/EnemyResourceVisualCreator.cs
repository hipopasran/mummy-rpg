using System;
using UnityEngine;

namespace Secret
{
    public class EnemyResourceVisualCreator : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Transform _resourceVisualRoot;
        [SerializeField] private CargoResourceBlock _resourceBlock;

        private void Start()
        {
            CreateResourceVisual();
        }

        private void CreateResourceVisual()
        {
            var resources = _enemy.GetResources();
            foreach (var res in resources)
            {
                var b = Instantiate(_resourceBlock, _resourceVisualRoot);
                b.Setup(res);
            }
        }
    }
}
