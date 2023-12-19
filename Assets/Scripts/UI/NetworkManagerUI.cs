using Trellcko.DefenseFromMonster.Core;
using Trellcko.DefenseFromMonster.Network;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Test
{
    public partial class NetworkManagerUI : MonoBehaviour
    {
        public void StartLikeHost()
        {
            NetworkConnecntionHandler.Instance.StartHost();
            NetworkManager.Singleton.SceneManager.LoadScene(SceneName.WaitingScene.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
        public void StartLikeClient()
        {
            NetworkConnecntionHandler.Instance.StartClient();
        }
    }
}