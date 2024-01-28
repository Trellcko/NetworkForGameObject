using Mono.CSharp;
using System;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.GamePlay
{
    public interface IInteractable
    {
        void Interact();
        event Action<IInteractable> Interacted;
    }
}
