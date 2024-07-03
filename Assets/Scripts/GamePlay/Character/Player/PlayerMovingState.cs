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

        private int _tick;
        private float _tickRate = 1f / 60f;
        private float _tickDeltaTime;

        private const int _buffer = 1024;

        public PlayerMovingState(CharacterAnimatorController animator, Transform transform, float speed, float angularSpeed, ulong id)
        {
            Debug.Log(id + " moving");
            _id = id;
            _animator = animator;
            _transform = transform;
            _speed = speed;
            _angularSpeed = angularSpeed;
        }

        public override void Enter()
        {
            Debug.Log(_id + " in move state");

            InputEvents.MovementCanceled += StopMovement;
            InputEvents.MovementPerfomed += SetDirection;
            InputEvents.InteractPerformed += OnInteractPerformed;
        }

        public override void Exit()
        {
            InputEvents.InteractPerformed -= OnInteractPerformed;
            InputEvents.MovementPerfomed -= SetDirection;
            InputEvents.MovementCanceled -= StopMovement;
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
            Debug.Log(obj + " Id: " + id + " MyId: " + _id + " name: " + _transform.name);
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
