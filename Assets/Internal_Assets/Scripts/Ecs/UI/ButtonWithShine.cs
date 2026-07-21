using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Secret
{
    public class ButtonWithShine : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Transform _shine;
        [SerializeField] private float _timer;

        private Coroutine _waitCoroutine;
        private Coroutine _shineCoroutine;

        private void OnEnable()
        {
            _waitCoroutine = StartCoroutine(Wait(5f, () =>
            {
                Shine();
            }));
        }

        private void OnDisable()
        {
            if(_waitCoroutine != null) StopCoroutine(_waitCoroutine);
            if(_shineCoroutine != null) StopCoroutine(_shineCoroutine);
            
            _shine.localPosition = new Vector3(-84f, 0f, 0f);
        }

        private void Shine()
        {
            if(_waitCoroutine != null) StopCoroutine(_waitCoroutine);
            if(_shineCoroutine != null) StopCoroutine(_shineCoroutine);
            
            if (!_button.interactable)
            {
                return;
            }
            
            _timer = Random.Range(4f, 10f);
            _waitCoroutine = StartCoroutine(Wait(_timer, () =>
            {
                _shineCoroutine = StartCoroutine(ShineProgress(() =>
                {
                    Shine();
                }));
            }));

        }

        private IEnumerator ShineProgress(Action callback)
        {
            _shine.localPosition = new Vector3(-84f, 0f, 0f);
            var time = 0f;
            while (time < 1f)
            {
                time += Time.deltaTime;
                _shine.localPosition = Vector3.Lerp(_shine.localPosition, new Vector3(84f, 0f,0f), time/1f);
                yield return null;
            }
            
            callback?.Invoke();
        }

        private IEnumerator Wait(float time, Action callback)
        {
            yield return new WaitForSeconds(time);
            callback?.Invoke();
        }
    }
}
