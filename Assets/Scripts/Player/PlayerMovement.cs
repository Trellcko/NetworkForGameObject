using Trellcko.DefenseFromMonster.Input;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Player
{
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _angularSpeed = 10f;
        [SerializeField] private PlayerAnimatorController _animatorController;

        private Vector3 _direction;

        public override void OnNetworkSpawn()
        {
            InputEvents.MovementPerfomed += SetDirection;
            InputEvents.MovementCanceled += StopMovement;
        }

        public override void OnNetworkDespawn()
        {
            InputEvents.MovementPerfomed -= SetDirection;
            InputEvents.MovementCanceled -= StopMovement;
        }

        private void Update()
        {
            if (_direction == Vector3.zero) return;
            transform.position += _direction * _speed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, 
                Quaternion.LookRotation(_direction), 
                Time.deltaTime * _angularSpeed);
        }

        private void SetDirection(Vector2 obj)
        {
            Vector3 target = new Vector3(obj.x, 0, obj.y);
            _direction = target;
            _animatorController.SetSpeed(_speed);
        }
        private void StopMovement()
        {
            _direction = Vector3.zero;
            _animatorController.SetSpeed(0);
        }


    }
}
