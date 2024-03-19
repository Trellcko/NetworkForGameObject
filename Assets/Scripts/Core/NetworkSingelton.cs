using Trellcko.DefenseFromMonster.Network;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko
{
    public class NetworkSingelton<T> : NetworkBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        public virtual void Awake()
        {
            if (FindObjectsOfType<T>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}
