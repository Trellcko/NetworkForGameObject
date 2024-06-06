using System;
using Trellcko.DefenseFromMonster.Core.SM;
using Trellcko.DefenseFromMonster.GamePlay;
using UnityEngine;

namespace Trellcko.Assets.Scripts.GamePlay.Player
{
    public class PlayerBehaviour : BaseBehaviour
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
                new PlayerInteractingState(Animator)
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
