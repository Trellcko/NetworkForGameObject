using Cinemachine;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Player
{
    public class PlayerVirtualCameraAttacher : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;

        private void OnEnable()
        {
            PlayerBehaviour.Spawned += OnSpawned;
        }

        private void OnDisable()
        {
            PlayerBehaviour.Spawned -= OnSpawned;
        }

        private void OnSpawned(PlayerBehaviour behaviour)
        {
            if (behaviour.IsLocalPlayer)
            {
                _camera.Follow = behaviour.transform;
            }
        }
    }
}
