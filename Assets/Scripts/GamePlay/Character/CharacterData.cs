using Sirenix.OdinInspector;
using System;
using Trellcko.DefenseFromMonster.Core.SM;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.GamePlay
{
    [CreateAssetMenu(fileName = "new Character Data", menuName = "SO/Character")]
    public class CharacterData : SerializedScriptableObject
    {
        [field: SerializeField] public int ID;

        [field: SerializeField] public BaseBehaviour BaseBehaviour;

        [field: TabGroup("Parameters")]
        [field: SerializeField] public float Speed; 
        
        [field: TabGroup("Parameters")]
        [field: SerializeField] public float AngularSpeed;

        [field: TabGroup("Parameters")]
        [field: SerializeField] public float MeleeAttackDamage;


        public Tuple<BaseBehaviour, NetworkObject> Create(Vector3 position, Quaternion quaternion)
        {
            BaseBehaviour spawned = Instantiate(BaseBehaviour, position, quaternion);
            spawned.Init(this);
            NetworkObject result = spawned.GetComponent<NetworkObject>();
            
            return Tuple.Create(spawned, result);
        }
    }
}
