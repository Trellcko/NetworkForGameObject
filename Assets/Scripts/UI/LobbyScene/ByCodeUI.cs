using Sirenix.Utilities;
using TMPro;
using Trellcko.DefenseFromMonster.Network.LobbyLogic;
using Unity.Services.Lobbies;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

namespace Trellcko.DefenseFromMonster.UI
{
    public class ByCodeUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _byCode;

        private void OnEnable()
        {
            _byCode.onClick.AddListener(ByCodeClick);
        }

        private void OnDisable()
        {
            _byCode.onClick.RemoveListener(ByCodeClick);
        }

        private void ByCodeClick()
        {
            if(_inputField.text.IsNullOrEmpty() || _inputField.text.IsNullOrWhitespace())
            {
                return;
            }

            else
            {
                LobbyManager.Instance.JoinByCode(_inputField.text);
            }
        }
    }
}
