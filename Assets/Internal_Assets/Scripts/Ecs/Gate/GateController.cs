using System.Collections;
using System;
using TriInspector;
using UnityEngine;

namespace Secret
{
    public class GateController : MonoBehaviour
    {
        [SerializeField] private string _gateId;
        
        [SerializeField] private float _duration;
        [SerializeField] private float _rightAngle;
        [SerializeField] private float _leftAngle;
        
        [SerializeField] private Transform _rightRoot;
        [SerializeField] private Transform _leftRoot;

        private Coroutine _animationCoroutine;

        [Button]
        public void OpenButton()
        {
            OpenGate();
        }

        private void OpenGate()
        {
            PlayerPrefs.SetInt(_gateId, 1);
            PlayerPrefs.Save();
            
            if(_animationCoroutine != null) StopCoroutine(_animationCoroutine);
            _animationCoroutine = StartCoroutine(OpenAnimation());
        }

        private IEnumerator OpenAnimation()
        {
            Quaternion startRotationRight = _rightRoot.localRotation;
            Quaternion targetRotationRight = Quaternion.Euler(0, _rightAngle, 0); 
            
            Quaternion startRotationLeft = _leftRoot.localRotation;
            Quaternion targetRotationLeft = Quaternion.Euler(0, _leftAngle, 0);
            
            float elapsedTime = 0f;

            while (elapsedTime < _duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / _duration; 
                
                _rightRoot.localRotation = Quaternion.Slerp(startRotationRight, targetRotationRight, t);
                _leftRoot.localRotation = Quaternion.Slerp(startRotationLeft, targetRotationLeft, t);
                
                yield return null;
            }

            _rightRoot.localRotation = targetRotationRight;
            _leftRoot.localRotation = targetRotationLeft;
        }

        private void OpenInMoment()
        {
            Quaternion targetRotationRight = Quaternion.Euler(0, _rightAngle, 0); 
            Quaternion targetRotationLeft = Quaternion.Euler(0, _leftAngle, 0);
            
            _rightRoot.localRotation = targetRotationRight;
            _leftRoot.localRotation = targetRotationLeft;
        }

        private void Awake()
        {
            Load();
        }

        private void Load()
        {
            if (PlayerPrefs.HasKey(_gateId))
            {
                if(PlayerPrefs.GetInt(_gateId) == 1)
                    OpenInMoment();
            }
        }
    }
}
