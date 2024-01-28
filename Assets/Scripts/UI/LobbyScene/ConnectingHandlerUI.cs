using TMPro;
using Trellcko.DefenseFromMonster.Network;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Trellcko.DefenseFromMonster.UI
{
    public class ConnectingHandlerUI : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private GameObject _connetingTextGO;
        [SerializeField] private TextMeshProUGUI _errorText;
        [SerializeField] private Button _closeButton;

        private void Start()
        {
            NetworkConnecntionHandler.Instance.TryingConnect += ShowConnecting;
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDestroy()
        {
            NetworkConnecntionHandler.Instance.TryingConnect -= ShowConnecting;
            _closeButton.onClick.RemoveListener(Close);
        }

        public void ShowConnecting()
        {
            _content.SetActive(true);
            _connetingTextGO.SetActive(true);
            _errorText.gameObject.SetActive(false);
            _closeButton.gameObject.SetActive(false);
        }

        public void ShowError()
        {
            _content.SetActive(true);
            _connetingTextGO.SetActive(false);
            _errorText.gameObject.SetActive(true);
            _errorText.SetText(NetworkManager.Singleton.DisconnectReason);
            _closeButton.gameObject.SetActive(true);
        }

        private void Close()
        {
            _content.SetActive(false);
        }
    }
}
