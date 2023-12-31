using TMPro;
using Trellcko.DefenseFromMonster.Core;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.UI
{
    public class CurrencyDisplayer : MonoBehaviour
    {
        [SerializeField] private Currency _currency;
        [SerializeField] private TextMeshProUGUI _text;

        private void OnEnable()
        {
            _currency.ValueUpdated += OnValueUpdated;
            if (_currency.IsSpawned)
            {
                OnValueUpdated();
            }
        }

        private void OnDisable()
        {
            _currency.ValueUpdated -= OnValueUpdated;
        }

        private void OnValueUpdated()
        {
            _text.SetText(_currency.Value.ToString());
        }
    }
}
