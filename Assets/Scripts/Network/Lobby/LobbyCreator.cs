using QFSW.QC;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Network.Lobby
{
    public class LobbyCreator : MonoBehaviour
    {
        [Command]
        public async void CreateLobby()
        {
            try
            {
                var lobby = await LobbyService.Instance.CreateLobbyAsync
                    (AuthenticationService.Instance.PlayerName + "`s game", 2);
                Debug.Log("Lobby: " + AuthenticationService.Instance.PlayerName + "`s game is created");  
            }
            catch(LobbyServiceException ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}
