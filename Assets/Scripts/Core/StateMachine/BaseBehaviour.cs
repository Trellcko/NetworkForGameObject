using Trellcko.DefenseFromMonster.GamePlay;
using Trellcko.DefenseFromMonster.GamePlay.Player;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Core.SM
{
    public abstract class BaseBehaviour : NetworkBehaviour
    {
        [SerializeField] protected CharacterAnimatorController Animator;

        public void Init(CharacterData characterData)
        {
            InitStateMachine(characterData);
        }

        protected abstract void InitStateMachine(CharacterData characterData);
    }
}
