using Trellcko.DefenseFromMonster.Core;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trellcko.DefenseFromMonster.Network
{
    public class NetworkConnecntionHandler : NetworkBehaviour
    {
        [SerializeField] private NetworkObject _player;

        public static NetworkConnecntionHandler Instansce;

        private void Awake()
        {
            Instansce = this;
            DontDestroyOnLoad(gameObject);
        }

        public void StartHost()
        {
            NetworkManager.Singleton.StartHost();
            NetworkManager.Singleton.ConnectionApprovalCallback += OnConnectionApproval;
        }

        public void StartClient()
        {
            NetworkManager.Singleton.StartClient();
        }

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += OnLoadEventCompleted;
            }
        }

        public override void OnNetworkDespawn()
        {
            NetworkManager.Singleton.SceneManager.OnLoadEventCompleted -= OnLoadEventCompleted;
        }

        private void OnConnectionApproval(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
        {
            if (SceneManager.GetActiveScene().name != SceneName.WaitingScene.ToString())
            {
                response.Approved = false;
                response.Reason = "Game is alredy start";
                return;
            }
            if(NetworkManager.Singleton.ConnectedClientsIds.Count > 2)
            {
                response.Approved = false;
                response.Reason = "Game is full";
                return;
            }
            response.Approved = true;

        }

        private void OnLoadEventCompleted(string sceneName, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode, System.Collections.Generic.List<ulong> clientsCompleted, System.Collections.Generic.List<ulong> clientsTimedOut)
        {
            if (sceneName == SceneName.GameScene.ToString())
            {
                foreach (var playerId in NetworkManager.ConnectedClientsIds)
                {
                    var spawned = Instantiate(_player);
                    spawned.SpawnAsPlayerObject(playerId, true);
                }
            }
        }
    }
}
