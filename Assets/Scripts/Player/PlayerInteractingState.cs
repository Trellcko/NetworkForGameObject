using Trellcko.DefenseFromMonster.Core.SM;

namespace Trellcko.DefenseFromMonster.Player
{
    public class PlayerInteractingState : BaseState
    {
        private PlayerAnimatorController _animator;
        public PlayerInteractingState(PlayerAnimatorController animator)
        {
            _animator = animator;
        }

        public override void Enter()
        {
            _animator.SetAttack();
            _animator.PlayerInteractAnimationCompleted += OnPlayerInteractAnimationCompleted;
        }

        public override void Exit()
        {
            _animator.PlayerInteractAnimationCompleted -= OnPlayerInteractAnimationCompleted;
        }

        private void OnPlayerInteractAnimationCompleted()
        {
            GoToState<PlayerMovingState>();
        }
    }
}
