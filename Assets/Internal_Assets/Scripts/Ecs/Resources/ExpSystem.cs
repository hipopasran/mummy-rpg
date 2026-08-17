using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class ExpSystem : ISystem
    {
        private Filter _filter;
        private Stash<ExpViewComponent> _expViewStash;
        
        private Request<ExpRequest> _expRequest;
        
        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            _expRequest = World.GetRequest<ExpRequest>();
            _filter = World.Filter.With<ExpViewComponent>().Build();
            _expViewStash = World.GetStash<ExpViewComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var request in _expRequest.Consume())
            {
                ApplyExp(request.Exp);
            }
        }

        private void ApplyExp(float exp)
        {
            PlayerExpManager.Instance.AddExp(exp);

            foreach (var entity in _filter)
            {
                ref var e = ref _expViewStash.Get(entity);
                e.ViewLink.UpdateValues();
            }
        }
    }
}
