using System.Collections.Generic;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;

namespace Secret
{
    public struct CargoRequest : IRequestData
    {
        public Entity TargetEntity;
        public int Cargo;
        public List<ResourcePack> Resource;
    }
}
