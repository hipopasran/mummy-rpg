using System.Collections.Generic;
using UnityEngine;

namespace Secret
{
    [CreateAssetMenu(fileName = "Data", menuName = "Levels/LevelSetup", order = 1)]
    public class PlayerLevelData : ScriptableObject
    {
        public List<double> Levels;
    }
}
