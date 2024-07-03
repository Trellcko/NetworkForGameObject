using QFSW.QC;
using Trellcko.DefenseFromMonster.GamePlay.Data;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.GamePlay.Character
{
    public abstract class BaseCharacterBehaviour : NetworkBehaviour, IDamagable
    {
        [SerializeField] protected CharacterAnimatorController Animator;
        [SerializeField] private Health _health;

        public void Init(CharacterTransportData characterData)
        {
            Debug.Log("Init by sERVER SIDE");
            InitStateMachine(characterData);
        }

        [ClientRpc]
        public void InitClientRpc(CharacterTransportDataSerializer characterTransportDataSerializer
            , ClientRpcParams clientRpcParams = default)
        {
            Debug.Log("Init by CLIENT SIDE");
            InitStateMachine(characterTransportDataSerializer.myData);
        }

        public void TakeDamage(float damage)
        {
            TakeDamageServerRPC(damage);
        }

        [ServerRpc(RequireOwnership = false)]
        private void TakeDamageServerRPC(float damage)
        {
            _health.TakeDamage(damage);
        }

        protected abstract void InitStateMachine(CharacterTransportData characterData);
    }
}
