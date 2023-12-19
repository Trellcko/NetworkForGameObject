using Unity.Netcode;

namespace Trellcko.DefenseFromMonster.Core
{
    public class SceneLoader : NetworkSingelton<SceneLoader>
    {
        public void LoadScene(SceneName sceneName)
        {
            NetworkManager.SceneManager.LoadScene(sceneName.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }
}
