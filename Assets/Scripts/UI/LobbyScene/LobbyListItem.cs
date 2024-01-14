using System;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Trellcko.DefenseFromMonster.UI.LobbyScene
{
    public class LobbyListItem : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _count;

        public event Action<Lobby> Clicked;

        private Lobby _lobby;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void Initialize(Lobby lobby)
        {
            Debug.Log("IsEnable: " + !lobby.IsLocked);
            _button.interactable = !lobby.IsLocked;
            _name.SetText(lobby.Name);
            _count.SetText($"{lobby.Players.Count}/{lobby.MaxPlayers}");
            _lobby = lobby;
        }

        private void OnClick()
        {
            Clicked?.Invoke(_lobby);
        }

    }
}
