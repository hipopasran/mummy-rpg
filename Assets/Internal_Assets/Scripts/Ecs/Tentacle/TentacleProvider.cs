using System.Collections.Generic;
using Obi;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Secret
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class TentacleProvider : MonoProvider<TentacleComponent>
    {
        [SerializeField] private bool _isReady;
        [SerializeField] private Transform _enemy;
        [SerializeField] private ObiRope _rope;
        [SerializeField] private Transform _root;

        public bool IsReady => _isReady;
        public Transform Enemy => _enemy;
        public Transform Root => _root;
        
        public void SetReady()
        {
            if(_enemy && _enemy.TryGetComponent(out Enemy enemyProvider))
            {
                enemyProvider.SendExpRequest();
                enemyProvider.SendCargoRequest();
                Destroy(_enemy.gameObject);
            }
            // _rope.distanceConstraintsEnabled = false;
            if (gameObject.TryGetComponent(out TentacleHomeFilterProvider homeProvider))
            {
                Destroy(homeProvider);
            }

            _enemy = null;
            _isReady = true;
        }

        public void SetAttack(Transform enemy)
        {
            _isReady = false;
            // _rope.distanceConstraintsEnabled = false;
            var attackFilterProvider = gameObject.AddComponent<TentacleAttackFilterProvider>();
            attackFilterProvider.Setup(enemy);
            _enemy = enemy;
        }

        public void SetHome()
        {
            // _rope.distanceConstraintsEnabled = true;
            _isReady = false;
            if (gameObject.TryGetComponent(out TentacleAttackFilterProvider attackProvider))
            {
                Destroy(attackProvider);
            }

            gameObject.AddComponent<TentacleHomeFilterProvider>();
            _enemy = null;
        }
    }
}
