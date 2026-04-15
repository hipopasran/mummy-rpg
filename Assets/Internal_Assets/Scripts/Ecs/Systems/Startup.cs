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

            systemGroup.AddSystem(new MobileInputSystem());
            systemGroup.AddSystem(new MovementSystem());


            #endregion

            this._world.AddSystemsGroup(order: 0, systemGroup);
        }
    }
}
