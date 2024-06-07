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


        public void TakeDamage(float damage)
        {
            TakeDamageServerRPC(damage);
        }

        [ServerRpc(RequireOwnership = false)]
        private void TakeDamageServerRPC(float damage)
        {
            _health.TakeDamage(damage);
        }

        protected abstract void InitStateMachine(CharacterData characterData);
    }
}
