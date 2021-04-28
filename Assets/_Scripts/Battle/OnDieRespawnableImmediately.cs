using UnityEngine;

namespace _Scripts.Battle
{
    public class OnDieRespawnableImmediately : OnDieRespawnableBase
    {
        protected override void HittableObjectOnDied()
        {
            _battleUnit.gameObject.SetActive(false);
        }

        public void RespawnNow()
        {
            if (!_spawnData.HasValue)
            {
                Debug.LogError($"{nameof(UnitSpawnData)} is missing!");
                return;
            }
            _spawnData.Value.UnitSpawner.StartRespawnProcess(_battleUnit, _spawnData.Value);
        }
    }
}
