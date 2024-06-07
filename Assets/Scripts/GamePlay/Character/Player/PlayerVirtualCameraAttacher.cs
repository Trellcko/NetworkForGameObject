using Cinemachine;
using Trellcko.DefenseFromMonster.GamePlay.Character;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.GamePlay.Character.Player
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

        private void OnSpawned(BaseCharacterBehaviour behaviour)
        {
            if (behaviour.IsLocalPlayer)
            {
                _camera.Follow = behaviour.transform;
            }
        }
    }
}
