using Mono.CSharp;
using System;
using Trellcko.DefenseFromMonster.Core;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.GamePlay
{
    public class Mine : MonoBehaviour, IInteractable
    {
        [SerializeField] private Currency _currency;

        public event Action<IInteractable> Interacted;

        public void Interact()
        {
            print("+1");
            _currency.AddServerRpc(1);
            Interacted?.Invoke(this);
        }
    }
}
