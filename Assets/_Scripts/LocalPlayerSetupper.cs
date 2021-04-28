using _Scripts.Battle;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts
{
    public class LocalPlayerSetupper : MonoBehaviour
    {
        [SerializeField] private UnitSpawner _unitSpawner;
        private IOwnerable[] _localOwnerable;
        
        private bool _isLocalPlayerSpawned;

        private BattleUnit _lastLocalPlayer;

        private void Awake()
        {
            Assert.IsNotNull(_unitSpawner, "_unitSpawner != null");

            _localOwnerable = GetComponentsInChildren<IOwnerable>(true);

            _unitSpawner.OnSpawned += SpawnerOnSpawned;
        }

        private void OnDestroy()
        {
            if (_unitSpawner != null)
                _unitSpawner.OnSpawned -= SpawnerOnSpawned;
        }

        private void SpawnerOnSpawned(BattleUnit unit)
        {
            if (!unit.IsLocalPlayer)
                return;

            if (_isLocalPlayerSpawned)
            {
                Debug.LogError($"Some local player already spawned!" );
                return;
            }
            
            if (_lastLocalPlayer != null)
            {
                _lastLocalPlayer.HittableObject.OnDied -= HittableObjectOnDied;
            }
            
            unit.HittableObject.OnDied += HittableObjectOnDied;
            _lastLocalPlayer = unit;
            

            foreach (var ownerable in _localOwnerable)
            {
                ownerable.InitOwner(unit);
            }
            
            _isLocalPlayerSpawned = true;
        }

        private void HittableObjectOnDied()
        {
            _isLocalPlayerSpawned = false;
        }
    }
}
