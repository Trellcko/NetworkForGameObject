using System;
using Trellcko.DefenseFromMonster.Core.SM;
using Trellcko.DefenseFromMonster.GamePlay.Data;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.GamePlay.Character.Player
{
    public class PlayerBehaviour : BaseCharacterBehaviour
    {
        private StateMachine _stateMachine;

        public static event Action<PlayerBehaviour> Spawned;

        public void OnDrawGizmos()
        {
            if (Animator)
            {
                Gizmos.DrawSphere(Animator.ModelInteractPoint.transform.position, 0.1f);
            }
        }

        public override void OnNetworkSpawn()
        {
            Spawned?.Invoke(this);
        }

        protected override void InitStateMachine(CharacterData characterData)
        {
            _stateMachine = new StateMachine(
                new PlayerMovingState(Animator, transform, 
                characterData.Speed, characterData.AngularSpeed),
                new MeleeAttackState(Animator, characterData.MeleeAttackDamage)
                );
            _stateMachine.SetState<PlayerMovingState>();
        }

        private void FixedUpdate()
        {
            _stateMachine?.FixedUpdate();
        }

        private void Update()
        {
            _stateMachine?.Update();
        }
    }
}
