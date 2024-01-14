using QFSW.QC;
using System;
using System.Collections.Generic;
using Trellcko.DefenseFromMonster.Core;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Network.LobbyLogic
{
    public class LobbyManager : Singelton<LobbyManager>
    {
        [Min(30)]
        [SerializeField] private float _maxTimeToSendHeartBeat = 30f;

        [Min(1.1f)]
        [SerializeField] private float _maxTimeToUpdateLobby = 1.1f;

        [SerializeField] private float _maxTimeToRefreshLobbiesList;
        
        public List<Lobby> Lobbies { get; private set; }

        private Lobby _hostLobby;
        private Lobby _joinedLobby;

        private float _timeToSendHeartBeat;
        private float _timeToUpdateLobby;
        private float _timeToRefreshLobbiesList;

        public event Action<List<Lobby>> LobbiesListUpdated;

        private void Update()
        {
            HandleSendHeartBeat();
            HandlePollLobbyForUpdates();
            if (_hostLobby == null && _joinedLobby == null)
            {
                RefreshLobbiesList();
            }
        }

        public string GetJoinedLobbyId()
        {
            if (_joinedLobby != null)
            {
                return _joinedLobby.Id;
            }
            return "";
        }

        private async void HandleSendHeartBeat()
        {
            if (_hostLobby != null)
            {
                _timeToSendHeartBeat += Time.deltaTime;
                if (_timeToSendHeartBeat > _maxTimeToSendHeartBeat)
                {
                    _timeToSendHeartBeat = 0;
                    await LobbyService.Instance.SendHeartbeatPingAsync(_hostLobby.Id);
                }
            }
        }

        public async void LockLobby()
        {
            try
            {
                _hostLobby = await LobbyService.Instance.UpdateLobbyAsync(_hostLobby.Id, new() { IsLocked = true });
            }

            catch(Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public async void HandlePollLobbyForUpdates()
        {
            if (_joinedLobby != null)
            {
                _timeToUpdateLobby += Time.deltaTime;
                if (_timeToUpdateLobby > _maxTimeToUpdateLobby)
                {
                    _timeToUpdateLobby = 0;
                    var updatedLobby = await LobbyService.Instance.GetLobbyAsync(_joinedLobby.Id);
                    _joinedLobby = updatedLobby;
                }
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
                _joinedLobby = lobby;
                NetworkManager.Singleton.StartHost();
                SceneLoader.Instance.LoadScene(SceneName.WaitingScene);
            }
            catch(Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public async void JoinByCode(string id)
        {
            try
            {
                NetworkManager.Singleton.StartClient();
                var lobby = await LobbyService.Instance.GetLobbyAsync(id);
                _joinedLobby = lobby;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public async void QuickJoinToLobby()
        {
            try
            {
                if (Lobbies.Count > 0)
                {
                    var lobby = await LobbyService.Instance.QuickJoinLobbyAsync();
                    _joinedLobby = lobby;
                    NetworkManager.Singleton.StartClient();
                }
                else
                {
                    CreateLobby();
                }
            }
            catch (Exception ex)
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
                        _timeToRefreshLobbiesList = 0;
                        var response = await LobbyService.Instance.QueryLobbiesAsync();
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
