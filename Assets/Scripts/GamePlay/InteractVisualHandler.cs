using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.GamePlay
{
    public class InteractVisualHandler : NetworkBehaviour
    {
        [SerializeField] private List<Mine> _mines;
        [SerializeField] private PopUpTextSpawner _popUpTextSpawner;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            foreach(var mine in _mines)
            {
                print("subsctibe");
                mine.Interacted += OnMineInteracted;
            }
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            foreach (var mine in _mines)
            {
                mine.Interacted -= OnMineInteracted;
            }
        }

        private void OnMineInteracted(IInteractable obj)
        {
            Mine mine = obj as Mine;
            OnMineInteractedServerRpc(mine.transform.position);
        }

        [ServerRpc(RequireOwnership = false)]
        private void OnMineInteractedServerRpc(Vector3 position)
        {
            OnMineInteractedClientRpc(position);
        }

        [ClientRpc]
        private void OnMineInteractedClientRpc(Vector3 position)
        {
            _popUpTextSpawner.Spawn("+1", position + Vector3.up);
        }
    }
}
