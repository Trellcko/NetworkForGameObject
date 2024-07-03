using System;
using Trellcko.DefenseFromMonster.Core.SM;
using Trellcko.DefenseFromMonster.GamePlay.Data;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.GamePlay.Character.Player
{
    public class PlayerBehaviour : BaseCharacterBehaviour
    {
        private StateMachine _stateMachine;

        public static event Action<PlayerBehaviour> Spawned;

        private NetworkVariable<ulong> _id = new();

        public void SetId(ulong id)
        {
            if (IsServer)
            {
                this._id.Value = id;
                Debug.Log(this._id.Value);
            }
        }

        

        public void OnDrawGizmos()
        {
            if (Animator)
            {
                Gizmos.DrawSphere(Animator.ModelInteractPoint.transform.position, 0.1f);
            }
        }

        public override void OnNetworkSpawn()
        {
            Spawned?.Invoke(this);
        }

        protected override void InitStateMachine(CharacterTransportData characterData)
        {
            Debug.Log("_id id :" + _id.Value);

            _stateMachine = new StateMachine(
                new PlayerMovingState(Animator, transform,
                characterData.Speed, characterData.AngularSpeed, OwnerClientId),
                new MeleeAttackState(Animator, characterData.MeleeAttackDamage)
                );
            _stateMachine.SetState<PlayerMovingState>();
        }

        private void FixedUpdate()
        {
            _stateMachine?.FixedUpdate();
        }

        private void Update()
        {
            _stateMachine?.Update();
        }
    }
}
