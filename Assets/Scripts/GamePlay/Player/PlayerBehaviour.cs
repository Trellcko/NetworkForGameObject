using Cinemachine;
using Sirenix.OdinInspector;
using System;
using Trellcko.DefenseFromMonster.Core.SM;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Player
{
    public class PlayerBehaviour : NetworkBehaviour
    {
        [SerializeField] private PlayerAnimatorController _animator;
        [SerializeField] private Transform _interactingPoint;
        
        [SerializeField] private float _speed;
        [SerializeField] private float _angularSpeed;

        public static event Action<PlayerBehaviour> Spawned;

        private StateMachine _stateMachine;

        public void OnDrawGizmos()
        {
            Gizmos.DrawSphere(_interactingPoint.transform.position, 0.1f);
        }

        public override void OnNetworkSpawn()
        {
            _stateMachine = new StateMachine(
                new PlayerMovingState(_animator, transform, _speed, _angularSpeed),
                new PlayerInteractingState(_animator, _interactingPoint)
                );
            _stateMachine.SetState<PlayerMovingState>();
            Spawned?.Invoke(this);
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
