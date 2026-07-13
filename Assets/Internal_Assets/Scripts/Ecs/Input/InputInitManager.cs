using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Secret
{
    public class InputInitManager : MonoBehaviour
    {
        public static InputInitManager Instance;

        [SerializeField] private InputActionAsset _inputActions;
        [SerializeField] private Image _joystickBack;
        
        private Vector3 _joystickStartPoint;
        private InputAction _moveAction;
        private Vector2 _moveValue;

        public InputAction MoveAction => _moveAction;

        private void OnEnable()
        {
            _inputActions.FindActionMap("Player").Enable();
        }

        private void OnDisable()
        {
            _inputActions.FindActionMap("Player").Disable();
        }

        private void Awake()
        {
            Instance = this;

            _joystickStartPoint = _joystickBack.rectTransform.localPosition;
            
            _moveAction = InputSystem.actions.FindAction("Move");
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    _joystickBack.gameObject.SetActive(true);
                    _joystickBack.transform.position = Input.mousePosition;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                // _joystickBack.gameObject.SetActive(false);
                _joystickBack.rectTransform.localPosition = _joystickStartPoint;
            }
        }
    }
}
