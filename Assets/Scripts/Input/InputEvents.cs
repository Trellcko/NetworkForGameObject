using System;
using UnityEngine;
using Trellcko.DefenseFromMonster.Core;

namespace Trellcko.DefenseFromMonster.Input
{
    public class InputEvents : Singelton<InputEvents>
    {
        [SerializeField] private InputActions _inputActions;

        public static event Action<Vector2> MovementPerfomed;
        public static event Action MovementCanceled;

        public static event Action InteractPerformed;

        public override void Awake()
        {
            base.Awake();
            _inputActions = new InputActions();
            _inputActions.Enable();
        }

        private void OnEnable()
        {
            _inputActions.Player.Movement.performed += OnMovementPerformed;
            _inputActions.Player.Movement.canceled += OnMovementCanceled;
            _inputActions.Player.Interact.performed += OnInteractPerfomed;
        }

        private void OnDisable()
        {
            _inputActions.Player.Movement.performed -= OnMovementPerformed;
            _inputActions.Player.Movement.canceled -= OnMovementCanceled;
            _inputActions.Player.Interact.performed-= OnInteractPerfomed;
        }

        private void OnInteractPerfomed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            InteractPerformed?.Invoke();
        }

        private void OnMovementPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            MovementPerfomed?.Invoke(obj.ReadValue<Vector2>());
        }

        private void OnMovementCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            MovementCanceled?.Invoke();
        }

    }
}
