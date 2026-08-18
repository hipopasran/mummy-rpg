using System;
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
                
                _expView.UpdateValues();
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
}
