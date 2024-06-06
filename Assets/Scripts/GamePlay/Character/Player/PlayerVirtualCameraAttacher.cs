using Cinemachine;
using Trellcko.DefenseFromMonster.Core.SM;
using UnityEngine;

namespace Trellcko.Assets.Scripts.GamePlay.Player
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

        private void OnSpawned(BaseBehaviour behaviour)
        {
            if (behaviour.IsLocalPlayer)
            {
                _camera.Follow = behaviour.transform;
            }
        }
    }
}
