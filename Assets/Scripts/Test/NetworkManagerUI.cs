using Unity.Netcode;
using UnityEngine;

namespace Trell.DefenseFromMonster.Test
{
    public class NetworkManagerUI : MonoBehaviour
    {
        public void StartLikeServer()
        {
            NetworkManager.Singleton.StartServer();
        }
        public void StartLikeHost()
        {
            NetworkManager.Singleton.StartHost();
        }
        public void StartLikeClient()
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}