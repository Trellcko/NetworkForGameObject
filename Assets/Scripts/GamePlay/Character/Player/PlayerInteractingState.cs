using Trellcko.DefenseFromMonster.Core.SM;
using Trellcko.DefenseFromMonster.GamePlay;
using Trellcko.DefenseFromMonster.GamePlay.Player;
using UnityEngine;

namespace Trellcko.Assets.Scripts.GamePlay.Player
{
    public class PlayerInteractingState : BaseState
    {
        private CharacterAnimatorController _animator;
        private Transform _interactPoint => _animator.ModelInteractPoint;

        private Collider[] _colliders = new Collider[5];
        private float _radius = 0.5f;

        public PlayerInteractingState(CharacterAnimatorController animator)
        {
            _animator = animator;
        }

        public override void Enter()
        {
            _animator.SetAttack();
            _animator.InteractAnimationFrameCompleted += OnPlayerInteractAnimationFrameCompleted;
            _animator.InteractAnimationCompleted += OnPlayerInteractAnimationCompleted;
        }

        public override void Exit()
        {
            _animator.InteractAnimationFrameCompleted -= OnPlayerInteractAnimationFrameCompleted;
            _animator.InteractAnimationCompleted -= OnPlayerInteractAnimationCompleted;
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
