using Trellcko.DefenseFromMonster.Core.SM;
using Trellcko.DefenseFromMonster.Input;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.GamePlay.Character.Player
{
    public class PlayerMovingState : BaseState
    {
        private CharacterAnimatorController _animator;

        private Vector3 _direction;
        private Transform _transform;
        private float _speed;
        private float _angularSpeed;

        private ulong _id;

        public PlayerMovingState(CharacterAnimatorController animator, Transform transform, float speed, float angularSpeed, ulong id)
        {
            _id = id;
            _animator = animator;
            _transform = transform;
            _speed = speed;
            _angularSpeed = angularSpeed;


            InputEvents.MovementPerfomed += SetDirection;
            InputEvents.MovementCanceled += StopMovement;
        }

        public override void Enter()
        {
            InputEvents.InteractPerformed += OnInteractPerformed;
        }

        public override void Exit()
        {
            InputEvents.InteractPerformed -= OnInteractPerformed;
        }

        public override void Update()
        {
            if (_direction == Vector3.zero) return;
            _transform.position += _direction * _speed * Time.deltaTime;
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation,
                Quaternion.LookRotation(_direction),
                Time.deltaTime * _angularSpeed);
        }

        private void StopMovement(ulong id)
        {
            if(_id != id) return;
            _direction = Vector3.zero;
            _animator.SetSpeed(0);
        }

        private void SetDirection(ulong id, Vector2 obj)
        {
            Debug.Log(obj);
            if (_id != id) return;
            Vector3 target = new Vector3(obj.x, 0, obj.y);
            _direction = target;

            _animator.SetSpeed(_speed);

        }

        private void OnInteractPerformed(ulong id)
        {
            if (_id != id) return;
            GoToState<MeleeAttackState>();
        }
    }
}
