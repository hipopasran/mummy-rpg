using UnityEngine;
using Scellecs.Morpeh;
using Unity.AI.Navigation;
using UnityEngine;
using VContainer;

namespace Secret
{
    public class Startup : MonoBehaviour
    {
        private World _world;

        private void Start()
        {
            Application.targetFrameRate = 60;
            this._world = World.Default;

            var systemGroup = this._world.CreateSystemsGroup();

            #region Systems

            // Player Movement
            systemGroup.AddSystem(new MobileInputSystem());
            systemGroup.AddSystem(new MovementSystem());
            systemGroup.AddSystem(new PlayerAnimationSystem());
            
            // Enemy Spawn
            systemGroup.AddSystem(new EnemyInitSpawnSystem());
            systemGroup.AddSystem(new SpawnerNeedSpawnSystem());
            
            // Enemy movement
            systemGroup.AddSystem(new EnemyWalkSystem());
            systemGroup.AddSystem(new EnemyWalkInProgressSystem());
            systemGroup.AddSystem(new EnemyWaitIdleSystem());
            
            // Tentacles
            systemGroup.AddSystem(new TentacleRootFollowSystem());
            systemGroup.AddSystem(new TentacleAttackSystem());
            systemGroup.AddSystem(new TentacleHomeSystem());
            
            // Enemy DMG
            systemGroup.AddSystem(new ApplyDamageSystem());
            systemGroup.AddSystem(new ApplyHealSystem());
            systemGroup.AddSystem(new HealWaitSystem());
            systemGroup.AddSystem(new EnemyFlyCargoSystem());
            
            // HealthBar
            systemGroup.AddSystem(new HealthBarRotationSystem());
            systemGroup.AddSystem(new HealthBarValueViewSystem());
            
            // Resources
            systemGroup.AddSystem(new ExpSystem());
            systemGroup.AddSystem(new CargoSystem());
            systemGroup.AddSystem(new CargoFullSystem());
            systemGroup.AddSystem(new CargoClearSystem());
            systemGroup.AddSystem(new CargoUpgradeSystem());
            
            // Camera
            systemGroup.AddSystem(new CameraScaleSystem());
            

            #endregion

            this._world.AddSystemsGroup(order: 0, systemGroup);
        }
    }
}
