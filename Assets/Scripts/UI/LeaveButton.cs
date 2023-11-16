using Trellcko.DefenseFromMonster.Core;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Trellcko.DefenseFromMonster.UI
{
    public class LeaveButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(Leave);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Leave);
        }

        private void Leave()
        {
            NetworkManager.Singleton.Shutdown();
            NetworkManager.Singleton.SceneManager.LoadScene(
                SceneName.LobbyScene.ToString(), 
                UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }
}
