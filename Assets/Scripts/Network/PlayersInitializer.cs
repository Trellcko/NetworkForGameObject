using Mono.CSharp;
using System.Collections.Generic;
using Trellcko.DefenseFromMonster.GamePlay.Character;
using Trellcko.DefenseFromMonster.GamePlay.Data;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
                SpawnPlayer(playerId);
            }
        }

        private void SpawnPlayer(ulong playerId)
        {
            BaseCharacterBehaviour spawned =
                Instantiate(characterData.BaseBehaviour, Vector3.zero, Quaternion.identity);
            NetworkObject result = spawned.GetComponent<NetworkObject>();
            result.SpawnAsPlayerObject(playerId, true);
            spawned.Init(characterData);
        }
    }
}
