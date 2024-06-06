using System;
using Trellcko.DefenseFromMonster.Core;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trellcko.DefenseFromMonster.Network
{
    public class NetworkConnecntionHandler : NetworkSingelton<NetworkConnecntionHandler>
    {
        [SerializeField] private NetworkObject _player;

        public event Action TryingConnect;
        public event Action ConnectionCanceled;
        public const int MaxPlayers = 2;

        public void StartHost()
        {
            NetworkManager.Singleton.StartHost();
            NetworkManager.Singleton.ConnectionApprovalCallback -= OnConnectionApproval;
            NetworkManager.Singleton.ConnectionApprovalCallback += OnConnectionApproval;
            TryingConnect?.Invoke();
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnectCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;
        }

        public void StartClient()
        {
            NetworkManager.Singleton.StartClient();
            TryingConnect?.Invoke();
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnectCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;
        }

        private void OnClientDisconnectCallback(ulong obj)
        {
            ConnectionCanceled?.Invoke();
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
            if(NetworkManager.Singleton.ConnectedClientsIds.Count > MaxPlayers)
            {
                response.Approved = false;
                response.Reason = "Game is full";
                return;
            }

            response.Approved = true;

        }

        private void OnLoadEventCompleted(string sceneName, LoadSceneMode loadSceneMode, System.Collections.Generic.List<ulong> clientsCompleted, System.Collections.Generic.List<ulong> clientsTimedOut)
        {
            if (sceneName == SceneName.GameScene.ToString())
            {

                Debug.Log("Loaded");
            }
        }
    }
}
