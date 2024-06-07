using System;
using System.Collections.Generic;
using Trellcko.Assets.Scripts.GamePlay.Player;
using Trellcko.DefenseFromMonster.Core.SM;
using Trellcko.DefenseFromMonster.GamePlay;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trellcko
{
    public class PlayersInitializer : NetworkBehaviour
    {
        [SerializeField] private CharacterData characterData;
        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += OnLoadEventCompleted;
            }


        }

        public override void OnNetworkDespawn()
        {
            if (IsServer)
            {
                NetworkManager.Singleton.SceneManager.OnLoadEventCompleted -= OnLoadEventCompleted;
            }
        }

        private void OnLoadEventCompleted(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
        {
            foreach (var playerId in NetworkManager.ConnectedClientsIds)
            {
                Debug.Log(playerId);
                var spawned = characterData.Create(Vector3.zero, Quaternion.identity);
                spawned.Item1.name = spawned.Item1.name + " " + playerId.ToString();
                spawned.Item2.SpawnAsPlayerObject(playerId, true);
            }
        }
    }
}
