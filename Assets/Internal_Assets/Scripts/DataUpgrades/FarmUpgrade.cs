using System;
using UnityEngine;
using System.Collections.Generic;

namespace Secret
{
    [CreateAssetMenu(menuName = "Data/" + nameof(FarmUpgrade))]
    public class FarmUpgrade : ScriptableObject
    {
        public FarmUpgradeType UpgradeType;
        public float StartValue;
        public List<UpgradeLevel> Levels;
    }
    
    [Serializable]
    public enum FarmUpgradeType
    {
        Income,
        Capacity
    }
}
