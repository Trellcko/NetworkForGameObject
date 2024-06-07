using QFSW.QC;
using System.Linq;
using Trellcko.DefenseFromMonster.GamePlay.Character.Player;
using UnityEngine;

namespace Trellcko
{
    public class GameCommander : MonoBehaviour
    {
        [Command]
        public void TakeDamagePlayer(ulong id, float damage)
        {
            PlayerBehaviour[] players = FindObjectsOfType<PlayerBehaviour>();

            PlayerBehaviour needPlayer = players.Where(x => x.OwnerClientId == id).FirstOrDefault();

            if (needPlayer)
            {
                needPlayer.TakeDamage(damage);
                return;
            }

            Debug.Log($"Player with ID {id} doesn't exist");
        }
    }
}
