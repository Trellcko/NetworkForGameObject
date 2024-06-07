using System.Collections.Generic;
using System.Linq;
using Trellcko.DefenseFromMonster.GamePlay.Data;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.GamePlay.Character
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private List<CharacterData> _characterData;

        public CharacterData SpawnCharacterWithID(int id)
        {
            return _characterData.Where(x => x.ID == id).FirstOrDefault();
        }
    }
}
