using System;
using System.Collections.Generic;
using UnityEngine;

namespace Secret
{
    [CreateAssetMenu(menuName = "Data/" + nameof(Upgrade))]
    public class Upgrade : ScriptableObject
    {
        public UpgradeType UpgradeType;
        public float StartValue;
        public List<UpgradeLevel> Levels;
    }

    [Serializable]
    public class UpgradeLevel
    {
        public float Value;
        public Cost Cost;
    }

    [Serializable]
    public class Cost
    {
        public List<ResourcePack> Resources;
    }

    [Serializable]
    public enum UpgradeType
    {
        Damage,
        Cargo,
        Speed,
        Tentacle,
        AttackRadius,
        Health
    }
}
