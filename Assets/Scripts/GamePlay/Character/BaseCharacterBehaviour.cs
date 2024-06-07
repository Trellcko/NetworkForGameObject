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

        public void Init(CharacterData characterData)
        {
            InitStateMachine(characterData);
        }

        [Command]
        public void TakeDamage(float damage)
        {
            TakeDamageServerRPC(damage);
        }

        [ServerRpc(RequireOwnership = false)]
        private void TakeDamageServerRPC(float damage)
        {
            _health.TakeDamage(10f);
        }

        protected abstract void InitStateMachine(CharacterData characterData);
    }
}
