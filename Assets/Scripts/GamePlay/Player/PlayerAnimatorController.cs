using System;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.GamePlay.Player
{
    public class PlayerAnimatorController : NetworkBehaviour
    {
        [SerializeField] private Animator _animator;

        public event Action PlayerInteractAnimationCompleted;
        public event Action PlayerInteractAnimationFrameCompleted;

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

        public void InvokeEventPlayerInteractAnimationFrameCompleted()
        {
            if (IsOwner)
            {
                PlayerInteractAnimationFrameCompleted?.Invoke();
            }
        }

        public void InvokeEventPlayerInteractAnimationCompleted()
        {
            if (IsOwner)
            {
                PlayerInteractAnimationCompleted?.Invoke();
            }
        }
    }
}
