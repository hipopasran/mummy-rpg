using System;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace Secret
{
    public class PlayerExpManager : MonoBehaviour
    {
        private static string PLayerLevelKey = "PlayerLevelKey";
        private static string PlayerExpKey = "PlayerExpKey";
        
        public static PlayerExpManager Instance;
        
        [Title("Exp")]
        [SerializeField] private float _expCurrent;
        [SerializeField] private int _level;

        [Title("Player Levels")] 
        [SerializeField] private PlayerLevelData _levels;

        [SerializeField] private ExpView _expView;

        [Title("Gates")] 
        [SerializeField] private GateToOpen _firstLevel;
        [SerializeField] private GateToOpen _secondLevel;
        [SerializeField] private GateToOpen _thirdLevel;
        [SerializeField] private GateToOpen _fourthLevel;
        [SerializeField] private GateToOpen _fiveLevel;
        [SerializeField] private GateToOpen _sixLevel;
        [SerializeField] private GateToOpen _sevenLevel;
        [SerializeField] private GateToOpen _eightLevel;
        [SerializeField] private GateToOpen _nineLevel;
        
        public int Level => _level;
        public float ExpCurrent => _expCurrent;
        public float ExpMax => (float)_levels.Levels[_level];

        #region Init
        
        private void Awake()
        {
            Instance = this;
            
            LoadLevel();
        }

        private void Start()
        {
            _expView.UpdateValues();
        }

        #endregion
        
        public void AddExp(float exp)
        {
            _expCurrent += exp;
            
            CheckNewLevel();
        }

        private void CheckNewLevel()
        {
            if (_expCurrent >= _levels.Levels[_level])
            {
                _level += 1;
                TryCheckShowOpenGates();
                _expView.UpdateValues();
            }
        }

        private void TryCheckShowOpenGates()
        {
            switch (_level)
            {
                case 1:
                    ShowOpenGates(_firstLevel);
                    break;
            }
        }

        private void ShowOpenGates(GateToOpen gates)
        {
            foreach (var gate in gates.Gates)
            {
                gate.OpenButton();
            }
        }

        private void SaveLevel()
        {
            PlayerPrefs.SetFloat(PlayerExpKey, _expCurrent);
            PlayerPrefs.SetInt(PLayerLevelKey, _level);
            PlayerPrefs.Save();
        }

        private void LoadLevel()
        {
            _expCurrent = PlayerPrefs.HasKey(PlayerExpKey) ? PlayerPrefs.GetFloat(PlayerExpKey) : 0;
            _level = PlayerPrefs.HasKey(PLayerLevelKey) ? PlayerPrefs.GetInt(PLayerLevelKey) : 0;
        }

        private void OnDestroy()
        {
            SaveLevel();
        }

        private void OnDisable()
        {
            SaveLevel();
        }
    }

    [Serializable]
    public class GateToOpen
    {
        public List<GateController> Gates;
    }
}
