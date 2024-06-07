using System;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.GamePlay.Character
{
    public class Health : NetworkBehaviour
    {
        [SerializeField] private float _maxHealth;

        private float _currentHealth;

        public event Action<float> Update;

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            InvokeUpdateClientRPC(_currentHealth);
        }

        [ClientRpc]
        private void InvokeUpdateClientRPC(float health)
        {
            Debug.Log("Health: " + health + " ID: " + this.NetworkObjectId + " Name: " + name);
        }
    }
}
