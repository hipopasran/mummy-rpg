using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;

namespace Secret
{
    public struct ExpRequest : IRequestData
    {
        public Entity TargetEntity;
        public float Exp;
    }
}
