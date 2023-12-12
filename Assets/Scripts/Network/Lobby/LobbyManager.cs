using QFSW.QC;
using System;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Network.LobbyLogic
{
    public class LobbyManager : MonoBehaviour
    {
        [Min(30)]
        [SerializeField] private float _maxTimeToSendHeartBeat = 30f;

        [Min(1.1f)]
        [SerializeField] private float _maxTimeToUpdateLobby = 1.1f;

        [SerializeField] private float _maxTimeToRefreshLobbiesList;

        private Lobby _hostLobby;
        private Lobby _joinedLobby;

        public event Action<List<Lobby>> LobbiesListUpdated;

        private float _timeToSendHeartBeat;
        private float _timeToUpdateLobby;
        private float _timeToRefreshLobbiesList;

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
                var updatedLobby = await LobbyService.Instance.GetLobbyAsync("id");
                _joinedLobby = updatedLobby;
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

        public async void RefreshLobbiesList()
        {
            try 
            {
                if (UnityServices.State == ServicesInitializationState.Initialized && AuthenticationService.Instance.IsSignedIn)
                {
                    if (_timeToRefreshLobbiesList > _maxTimeToRefreshLobbiesList)
                    {
                        var response = await LobbyService.Instance.QueryLobbiesAsync();
                        _timeToRefreshLobbiesList = 0;
                        LobbiesListUpdated?.Invoke(response.Results);
                    }
                    else
                    {
                        _timeToRefreshLobbiesList += Time.deltaTime;
                    }

                }
            }
            catch(LobbyServiceException ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}
