using Unity.Netcode.Components;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Core
{
    [DisallowMultipleComponent]
    public class ClientNetworkAnimator : NetworkAnimator
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}
