using Sirenix.OdinInspector;
using System;
using Trellcko.DefenseFromMonster.GamePlay.Character;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.GamePlay.Data
{
    [CreateAssetMenu(fileName = "new Character Data", menuName = "SO/Character")]
    public class CharacterData : SerializedScriptableObject
    {
        [field: SerializeField] public int ID;

        [field: SerializeField] public BaseCharacterBehaviour BaseBehaviour;

        [field: TabGroup("Parameters")]
        [field: SerializeField] public float Speed; 
        
        [field: TabGroup("Parameters")]
        [field: SerializeField] public float AngularSpeed;

        [field: TabGroup("Parameters")]
        [field: SerializeField] public float MeleeAttackDamage;


        public Tuple<BaseCharacterBehaviour, NetworkObject> Create(Vector3 position, Quaternion quaternion)
        {
            BaseCharacterBehaviour spawned = Instantiate(BaseBehaviour, position, quaternion);
            spawned.Init(this);
            NetworkObject result = spawned.GetComponent<NetworkObject>();
            
            return Tuple.Create(spawned, result);
        }
    }
}
