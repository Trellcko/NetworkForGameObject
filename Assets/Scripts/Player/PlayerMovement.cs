using Trell.DefenseFromMonster.Input;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace Trell.DefenseFromMonster.Plater
{
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private float _speed = 1f;

        private Vector3 _direction;

        public override void OnNetworkSpawn()
        {
            InputEvents.Instance.MovementPerfomed += SetDirection;
            InputEvents.Instance.MovementCanceled += StopMovement;
        }

        public override void OnNetworkDespawn()
        {
            InputEvents.Instance.MovementPerfomed -= SetDirection;
            InputEvents.Instance.MovementCanceled -= StopMovement;
        }

        private void Update()
        {
            transform.position += _direction * _speed * Time.deltaTime;
        }

        private void SetDirection(Vector2 obj)
        {
            Vector3 target = new Vector3(obj.x, 0, obj.y);
            _direction = target;
        }
        private void StopMovement()
        {
            _direction = Vector3.zero;
        }


    }
}
