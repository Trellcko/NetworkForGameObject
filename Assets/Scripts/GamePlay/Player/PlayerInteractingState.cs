using Trellcko.DefenseFromMonster.Core.SM;
using Trellcko.DefenseFromMonster.GamePlay;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Player
{
    public class PlayerInteractingState : BaseState
    {
        private PlayerAnimatorController _animator;
        private Transform _interactPoint;

        private Collider[] _colliders= new Collider[5];
        private float _radius = 0.5f;

        public PlayerInteractingState(PlayerAnimatorController animator, Transform interactPoint)
        {
            _interactPoint = interactPoint;
            _animator = animator;
        }

        public override void Enter()
        {
            _animator.SetAttack();
            _animator.PlayerInteractAnimationFrameCompleted += OnPlayerInteractAnimationFrameCompleted;
            _animator.PlayerInteractAnimationCompleted += OnPlayerInteractAnimationCompleted;
        }

        public override void Exit()
        {
            _animator.PlayerInteractAnimationFrameCompleted -= OnPlayerInteractAnimationFrameCompleted;
            _animator.PlayerInteractAnimationCompleted -= OnPlayerInteractAnimationCompleted;
        }

        private void OnPlayerInteractAnimationFrameCompleted()
        {
            int count = Physics.OverlapSphereNonAlloc(_interactPoint.position, _radius, _colliders);

            Debug.Log(count);
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (_colliders[i].TryGetComponent(out IInteractable interactable))
                    {
                        Debug.Log(_colliders[i].name);
                        interactable.Interact();
                    }
                }
            }
        }

        private void OnPlayerInteractAnimationCompleted()
        {
            GoToState<PlayerMovingState>();
        }
    }
}
