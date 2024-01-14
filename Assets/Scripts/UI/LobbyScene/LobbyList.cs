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
        
        private List<LobbyListItem> _lobbyListItems = new();
        private List<Lobby> _lobbies;

        private float _currentTime;

        private void OnEnable()
        {
            LobbyManager.Instance.LobbiesListUpdated += OnLobbiesListUpdated;
        }

        private void OnDisable()
        {
            LobbyManager.Instance.LobbiesListUpdated -= OnLobbiesListUpdated;
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
                    _lobbyListItems[_lobbyListItems.Count - 1].Clicked += OnClicked;
                }
            }

            else if(_lobbyListItems.Count > lobbies.Count)
            {
                for(int i = lobbies.Count; i < _lobbyListItems.Count; i++)
                {
                    _lobbyListItems[i].gameObject.SetActive(false);
                }
            }



            for(int  i = 0; i < lobbies.Count; i++)
            {
                _lobbyListItems[i].gameObject.SetActive(true);
                _lobbyListItems[i].Initialize(lobbies[i]);
            }
        }

        private void OnClicked(Lobby obj)
        {
            LobbyManager.Instance.JoinByCode(obj.Id);
        }
    }
}
