using Sirenix.OdinInspector;
using Trell.DefenseFromMonster.Core;
using UnityEngine;

namespace Trell
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
