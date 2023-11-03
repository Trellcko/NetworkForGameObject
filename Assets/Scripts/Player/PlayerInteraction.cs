using Trellcko.DefenseFromMonster.Input;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private PlayerAnimatorController _animator;

        private void OnEnable()
        {
            InputEvents.InteractPerformed += OnInteractPerformed;
        }

        private void OnDisable()
        {
            InputEvents.InteractPerformed -= OnInteractPerformed;
        }

        private void OnInteractPerformed()
        {
            _animator.SetAttack();
        }
    }
}
