using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace Secret
{
    public class CargoFullView : MonoProvider<CargoFullComponent>
    {
        public void ResetTimer()
        {
            ref var c = ref Stash.Get(Entity);
            c.CurrentTime = c.TimeToShow;
            c.GameObjectLink.SetActive(true);
        }
    }
}
