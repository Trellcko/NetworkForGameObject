using System;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.GamePlay.Character
{
    public class CharacterAnimatorController : NetworkBehaviour
    {
        [SerializeField] private Animator _animator;


        [field: SerializeField] public Transform ModelInteractPoint;

        public event Action AttackAnimationCompleted;
        public event Action AttackAnimationFrameCompleted;

        public const string SpeedParameterName = "Speed";
        public const string AttackParameterName = "Attack";

        public void SetAnimator(Animator animator)
        {
            _animator = animator;
        }

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
                AttackAnimationFrameCompleted?.Invoke();
            }
        }

        public void InvokeEventPlayerInteractAnimationCompleted()
        {
            if (IsOwner)
            {
                AttackAnimationCompleted?.Invoke();
            }
        }
    }
}
