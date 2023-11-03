using Sirenix.OdinInspector;
using Trellcko.DefenseFromMonster.Core;
using UnityEngine;

namespace Trellcko
{
    public class CurrencyTest : MonoBehaviour
    {
        [SerializeField] private Currency _currency;

        [Button("Add")]
        public void Test(int value)
        {
            _currency.AddServerRpc(value);
        }
    }
}
