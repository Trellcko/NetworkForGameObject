using Unity.Netcode.Components;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Core
{
    [DisallowMultipleComponent]
    public class ClientNetworkTransform : NetworkTransform
    {

        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}
