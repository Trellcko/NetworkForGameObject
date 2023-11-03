using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Player
{
    public class PlayerAnimatorController : NetworkBehaviour
    {
        [SerializeField] private Animator _animator;

        public const string SpeedParameterName = "Speed";
        public const string AttackParameterName = "Attack";

        public void SetSpeed(float speed)
        {
            if (IsOwner)
            {
                _animator.transform.localPosition = Vector3.zero;
                _animator.SetFloat(SpeedParameterName, speed);
            }
        }

        public void SetAttack()
        {
            if (IsOwner)
            {
                _animator.transform.localPosition = Vector3.zero;
                _animator.SetTrigger(AttackParameterName);
            }
        }
    }
}
