using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.GamePlay.Enemy
{
    public class WaveController : NetworkBehaviour
    {
        [SerializeField] private float _timeToNextWave;
        [SerializeField] private List<Wave> _waves;
    }

    [Serializable]
    public class Wave
    {
        [field: SerializeField] public Vector2Int EnemyCount { get; private set; }

        [Tooltip("Easy, Normal, LittleHard, Hard, VeryHard, Impossible")]
        [SerializeField] private List<float> _enemyDifficultiesChange;

        public float GetPercent(EnemyDifficulties enemyDifficulties) => 
            _enemyDifficultiesChange[(int)enemyDifficulties];
    }

    public enum EnemyDifficulties
    {
        Easy,
        Normal,
        LittleHard,
        Hard,
        VeryHard,
        Impossible
    }
}
