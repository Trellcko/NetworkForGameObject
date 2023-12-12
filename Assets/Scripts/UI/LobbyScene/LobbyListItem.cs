using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trellcko.DefenseFromMonster.UI.LobbyScene
{
    public class LobbyListItem : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _count;
        
        public void Initialize(bool isEnable, string name, int currentCount, int maxCount)
        {
            _button.interactable = isEnable;
            _name.SetText(name);
            _count.SetText($"{currentCount}/{maxCount}");
        }
    }
}
