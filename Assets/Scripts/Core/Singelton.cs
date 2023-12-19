using Trellcko.DefenseFromMonster.Network;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Core
{
    public class Singelton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        public virtual void Awake()
        {
            if (FindObjectsOfType<NetworkConnecntionHandler>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}
