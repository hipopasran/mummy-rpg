using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class ExpSystem : ISystem
    {
        private Filter _filter;
        
        private Request<ExpRequest> _expRequest;
        
        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            _expRequest = World.GetRequest<ExpRequest>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var request in _expRequest.Consume())
            {
                ApplyExp(request.TargetEntity, request.Exp);
            }
        }

        private void ApplyExp(Entity entity, float exp)
        {
            PlayerStats.Instance.AddExp(exp);
        }
    }
}
