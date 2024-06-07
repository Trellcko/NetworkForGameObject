using QFSW.QC;
using Trellcko.DefenseFromMonster.GamePlay;
using Trellcko.DefenseFromMonster.GamePlay.Player;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Core.SM
{
    public abstract class BaseBehaviour : NetworkBehaviour, IDamagable
    {
        [SerializeField] protected CharacterAnimatorController Animator;

        float health = 10f;

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
            health -= 10f;

            Debug.Log(Animator.transform.parent.name + " "  + health);
        }

        protected abstract void InitStateMachine(CharacterData characterData);
    }
}
