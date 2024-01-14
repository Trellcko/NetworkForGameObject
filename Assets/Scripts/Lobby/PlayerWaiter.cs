using System.Collections.Generic;
using Unity.Netcode;
using Trellcko.DefenseFromMonster.Core;
using Trellcko.DefenseFromMonster.Network.LobbyLogic;

namespace Trellcko.DefenseFromMonster.Network
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


            foreach (var player in NetworkManager.Singleton.ConnectedClientsIds)
            {
                if (!_readyPlayers.ContainsKey(player) || !_readyPlayers[player])
                {
                    return;
                }
            }

            LobbyManager.Instance.LockLobby();

            SceneLoader.Instance.LoadScene(SceneName.GameScene);
        }
    }
}
