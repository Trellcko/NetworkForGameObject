using System;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Input
{
    public class InputEvents : MonoBehaviour
    {
        [SerializeField] private InputActions _inputActions;
        public static InputEvents Instance { get; private set; }

        public static event Action<Vector2> MovementPerfomed;
        public static event Action MovementCanceled;

        public static event Action InteractPerformed;

        private void Awake()
        {
            if(FindObjectsOfType<InputEvents>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

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
            print("Lalalal");
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
