using System.Collections.Generic;
using Trellcko.DefenseFromMonster.Network.LobbyLogic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.UI.LobbyScene
{
    public class LobbyList : MonoBehaviour
    {
        [SerializeField] private LobbyListItem _prefab;
        [SerializeField] private RectTransform _parent;
        [SerializeField] private LobbyManager _manager;

        private List<LobbyListItem> _lobbyListItems = new();

        private float _currentTime;

        private void OnEnable()
        {
            _manager.LobbiesListUpdated += OnLobbiesListUpdated;
        }

        private void OnDisable()
        {
            _manager.LobbiesListUpdated -= OnLobbiesListUpdated;
        }

        public LobbyListItem CreateLobbyListItem()
        {
            return Instantiate(_prefab, _parent);
        }

        private void OnLobbiesListUpdated(List<Lobby> lobbies)
        {
            if(_lobbyListItems.Count < lobbies.Count)
            {
                for(int  i = 0; i< lobbies.Count - _lobbyListItems.Count; i++)
                {
                    _lobbyListItems.Add(CreateLobbyListItem());
                }
            }

            else if(_lobbyListItems.Count > lobbies.Count)
            {
                for(int i = lobbies.Count; i < _lobbyListItems.Count; i++)
                {
                    _lobbyListItems[i - 1].gameObject.SetActive(false);
                }
            }



            for(int  i = 0; i < lobbies.Count; i++)
            {
                _lobbyListItems[i].gameObject.SetActive(true);
                _lobbyListItems[i].Initialize(lobbies[i].IsLocked, lobbies[i].Name, lobbies[i].Players.Count, lobbies[i].MaxPlayers);
            }
        }
    }
}
