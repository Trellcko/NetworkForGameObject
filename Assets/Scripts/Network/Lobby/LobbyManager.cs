using QFSW.QC;
using System.Collections;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Network.Lobby
{
    public class LobbyManager : MonoBehaviour
    {
        [Min(30)]
        [SerializeField] private float _maxTimeToSendHeartBeat = 30f;

        [Min(1.1f)]
        [SerializeField] private float _maxTimeToUpdateLobby = 1.1f;

        private Unity.Services.Lobbies.Models.Lobby _hostLobby;

        private float _timeToSendHeartBeat;
        private float _timeToUpdateLobby;

        private void Update()
        {
            HandleSendHeartBeat();
        }

        private async void HandleSendHeartBeat()
        {
            if (_hostLobby != null)
            {
                _timeToSendHeartBeat += Time.deltaTime;
                if (_timeToSendHeartBeat > _maxTimeToSendHeartBeat)
                {
                    await LobbyService.Instance.SendHeartbeatPingAsync(_hostLobby.Id);
                    _timeToSendHeartBeat = 0;
                }
            }
        }

        private async void HadnlePollLobbyForUpdates()
        {
            _timeToUpdateLobby += Time.deltaTime;
            if (_timeToUpdateLobby > _maxTimeToUpdateLobby)
            {
                await LobbyService.Instance.GetLobbyAsync("id");
                _timeToUpdateLobby = 0;
            }
        }

        [Command]
        public async void CreateLobby()
        {
            try
            {
                var lobby = await LobbyService.Instance.CreateLobbyAsync
                    (AuthenticationService.Instance.PlayerName + "`s game", 2);
                Debug.Log("Lobby: " + AuthenticationService.Instance.PlayerName + "`s game is created");
                _hostLobby = lobby;
            }
            catch(LobbyServiceException ex)
            {
                Debug.LogException(ex);
            }
        }

        [Command]
        public async void GetLobbies()
        {
            try 
            {
                var response = await LobbyService.Instance.QueryLobbiesAsync();

                Debug.Log("Lobbies Count: " + response.Results.Count);
            }
            catch(LobbyServiceException ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}
