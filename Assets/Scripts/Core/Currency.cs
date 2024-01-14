using System;
using System.Diagnostics;
using Unity.Netcode;

namespace Trellcko.DefenseFromMonster.Core
{
    public class Currency : NetworkBehaviour
    {
        public int Value => _currentCurrency.Value;

        public event Action ValueUpdated;

        private NetworkVariable<int> _currentCurrency = new();

        public override void OnNetworkSpawn()
        {
            _currentCurrency.OnValueChanged += OnValueChanged;
            ValueUpdated?.Invoke(); 
        }

        public override void OnNetworkDespawn()
        {
            _currentCurrency.OnValueChanged -= OnValueChanged;
        }

        [ServerRpc(RequireOwnership = false)]
        public void AddServerRpc(int value)
        {
            UnityEngine.Debug.Log("ServeRPC");
            if(value < 0) value = 0;
            _currentCurrency.Value += value;
        }

        [ServerRpc(RequireOwnership = false)]
        public void SpentServerRpc(int value)
        {
            if (value < 0) value = 0;
            _currentCurrency.Value -= value;
        }
    
        private void OnValueChanged(int was, int now)
        {
            ValueUpdated?.Invoke();
        }
    }
}
