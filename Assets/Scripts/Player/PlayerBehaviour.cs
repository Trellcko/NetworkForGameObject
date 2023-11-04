using Sirenix.OdinInspector;
using Trellcko.DefenseFromMonster.Core.SM;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Player
{
    public class PlayerBehaviour : NetworkBehaviour
    {
        [SerializeField] private PlayerAnimatorController _animator;

        [BoxGroup("Stats")]
        [TitleGroup("Stats/Movement")]
        [SerializeField] private float _speed;
        [TitleGroup("Stats/Movement")]
        [SerializeField] private float _angularSpeed;

        private StateMachine _stateMachine;

        public override void OnNetworkSpawn()
        {
            _stateMachine = new StateMachine(
                new PlayerMovingState(_animator, transform, _speed, _angularSpeed),
                new PlayerInteractingState(_animator)
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
