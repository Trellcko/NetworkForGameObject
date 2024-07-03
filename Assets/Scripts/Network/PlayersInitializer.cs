using System.Collections.Generic;
using Trellcko.DefenseFromMonster.GamePlay.Character;
using Trellcko.DefenseFromMonster.GamePlay.Character.Player;
using Trellcko.DefenseFromMonster.GamePlay.Data;
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
                SpawnPlayer(playerId);
            }
        }

        private void SpawnPlayer(ulong playerId)
        {
             BaseCharacterBehaviour spawned =
                Instantiate(characterData.BaseBehaviour, Vector3.zero, Quaternion.identity);
            NetworkObject result = spawned.GetComponent<NetworkObject>();
            result.SpawnAsPlayerObject(playerId, true);

            (spawned as PlayerBehaviour).SetId(playerId);

            CharacterTransportDataSerializer characterTransportDataSerializer = characterData.ConvertToTransportData();

            spawned.Init(characterTransportDataSerializer.myData);

            Debug.Log("Real + " + spawned.OwnerClientId);

            ClientRpcParams clientRpcParams = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new ulong[] { playerId }
                }
            };

            spawned.InitClientRpc(characterTransportDataSerializer, clientRpcParams);


            spawned.name += playerId;
        }
    }

}
