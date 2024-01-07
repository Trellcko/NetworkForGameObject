using TMPro;
using Trellcko.DefenseFromMonster.Network.LobbyLogic;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.UI.WaitingScene
{
    public class IdLobbyDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _idLobbyDisplayerName;

        private void Start()
        {
            _idLobbyDisplayerName.SetText(LobbyManager.Instance.GetJoinedLobbyId()); 
        }
    }
}
