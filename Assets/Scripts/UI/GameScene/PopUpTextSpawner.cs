using Trellcko.DefenseFromMonster.Core.Pool;
using Trellcko.DefenseFromMonster.UI;
using UnityEngine;

namespace Trellcko
{
    public class PopUpTextSpawner : MonoBehaviour
    {
        [SerializeField] private NetworkObjectPool _pool;
        [SerializeField] private PopUpText _prefab;
        [SerializeField] private Camera _camera;

        
        public void Spawn(string text, Vector3 position)
        {
            var spawned = _pool.GetNetworkObject(_prefab.gameObject, position, Quaternion.identity);
            var popUp = spawned.GetComponent<PopUpText>();

            print(popUp.name);

            popUp.Init(_camera.transform, text);
            popUp.transform.position = position;
            popUp.Reseted += OnReseted;
        }

        private void OnReseted(PopUpText obj)
        {
            obj.Reseted -= OnReseted;
            _pool.ReturnNetworkObject(obj.NetworkObject, _prefab.gameObject);
        }
    }
}
