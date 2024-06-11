using System;
using UnityEngine;
using Unity.Netcode;

namespace Trellcko.DefenseFromMonster.Input
{
    public class InputEvents : NetworkSingelton<InputEvents>
    {
        [SerializeField] private InputActions _inputActions;

        public static event Action<ulong, Vector2> MovementPerfomed;
        public static event Action<ulong> MovementCanceled;

        public static event Action<ulong> InteractPerformed;

        public override void OnNetworkSpawn()
        {
            _inputActions = new InputActions();
            _inputActions.Enable();
            _inputActions.Player.Movement.performed += OnMovementPerformed;
            _inputActions.Player.Movement.canceled += OnMovementCanceled;
            _inputActions.Player.Interact.performed += OnInteractPerfomed;
        }

        public override void OnNetworkDespawn()
        {
            _inputActions.Player.Movement.performed -= OnMovementPerformed;
            _inputActions.Player.Movement.canceled -= OnMovementCanceled;
            _inputActions.Player.Interact.performed-= OnInteractPerfomed;
        }

        private void OnInteractPerfomed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            InvokeIntercatPerfomedServerRPC();
        }

        private void OnMovementPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            InvokeMovementPerfomedServerRPC(obj.ReadValue<Vector2>());
        }

        private void OnMovementCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            InvokeMovementCanceledServerRPC();
        }

        [ServerRpc(RequireOwnership = false)]
        public void InvokeMovementPerfomedServerRPC(Vector2 input, ServerRpcParams param = default)
        {
            MovementPerfomed?.Invoke(param.Receive.SenderClientId, input);
        }
        [ServerRpc(RequireOwnership = false)]
        public void InvokeMovementCanceledServerRPC(ServerRpcParams param = default)
        {

            MovementCanceled?.Invoke(param.Receive.SenderClientId);
        }
        [ServerRpc(RequireOwnership = false)]
        public void InvokeIntercatPerfomedServerRPC(ServerRpcParams param = default)
        {
            InteractPerformed?.Invoke(param.Receive.SenderClientId);
        }

    }
}
