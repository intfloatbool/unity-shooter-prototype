using System.Collections;
using UnityEngine;

namespace _Scripts.Battle
{
    public class OnDieRespawanbleByTimer : OnDieRespawnableBase
    {
        [SerializeField] private float _timeToRespawn = 5;

        protected override void HittableObjectOnDied()
        {
            if (!_spawnData.HasValue)
            {
                Debug.LogError($"{nameof(UnitSpawnData)} is missing!");
                return;
            }
            _spawnData.Value.UnitSpawner.StartRespawnProcess(_battleUnit, _spawnData.Value,
                _timeToRespawn);
        }
        
    }
}
