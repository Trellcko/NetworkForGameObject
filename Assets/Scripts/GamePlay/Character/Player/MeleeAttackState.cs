using Trellcko.DefenseFromMonster.Core.SM;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.GamePlay.Character.Player
{
    public class MeleeAttackState : BaseState
    {
        private CharacterAnimatorController _animator;
        private Transform _interactPoint => _animator.ModelInteractPoint;

        private Collider[] _colliders = new Collider[5];
        private float _radius = 0.5f;
        private float _damage = 0.5f;

        public MeleeAttackState(CharacterAnimatorController animator, float damage)
        {
            _animator = animator;
            _damage = damage;
        }

        public override void Enter()
        {
            _animator.SetAttack();
            _animator.AttackAnimationFrameCompleted += OnPlayerAttackAnimationFrameCompleted;
            _animator.AttackAnimationCompleted += OnPlayerAttackAnimationCompleted;
        }

        public override void Exit()
        {
            _animator.AttackAnimationFrameCompleted -= OnPlayerAttackAnimationFrameCompleted;
            _animator.AttackAnimationCompleted -= OnPlayerAttackAnimationCompleted;
        }

        private void OnPlayerAttackAnimationFrameCompleted()
        {
            int count = Physics.OverlapSphereNonAlloc(_interactPoint.position, _radius, _colliders);

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (_colliders[i].TryGetComponent(out IDamagable interactable))
                    {
                        interactable.TakeDamage(_damage);
                    }
                }
            }
        }

        private void OnPlayerAttackAnimationCompleted()
        {
            GoToState<PlayerMovingState>();
        }
    }
}
