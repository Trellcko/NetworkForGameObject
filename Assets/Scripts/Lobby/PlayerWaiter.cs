using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Trellcko.DefenseFromMonster.Core;

namespace Trellcko.DefenseFromMonster.Lobby.PlayerWaiter
{
    public class PlayerWaiter : NetworkBehaviour
    {
        private Dictionary<ulong, bool> _readyPlayers = new();

        public void SetPlayerReady()
        {
            SetPlayerReadyServerRpc();
        }


        [ServerRpc(RequireOwnership = false)]
        public void SetPlayerReadyServerRpc(ServerRpcParams param = default)
        {
            _readyPlayers.Add(param.Receive.SenderClientId, true);

            
            foreach(var player in NetworkManager.Singleton.ConnectedClientsIds)
            {
                if(!_readyPlayers.ContainsKey(player) || !_readyPlayers[player])
                {
                    return;
                }
            }
            NetworkManager.SceneManager.LoadScene(SceneName.GameScene.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }
}
