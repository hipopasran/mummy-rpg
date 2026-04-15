using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Secret
{
    public class InputInitManager : MonoBehaviour
    {
        public static InputInitManager Instance;

        [SerializeField] private InputActionAsset _inputActions;
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
            
            _moveAction = InputSystem.actions.FindAction("Move");
        }
    }
}
