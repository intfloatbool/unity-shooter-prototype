using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Battle
{
    public abstract class OnDieRespawnableBase : MonoBehaviour, IRespawnableUnit
    {
        [SerializeField] protected BattleUnit _battleUnit;
        [SerializeField] protected HittableObject _hittableObject;
        protected UnitSpawnData? _spawnData;
        
        protected virtual void OnValidate()
        {
            if (_hittableObject == null)
            {
                _hittableObject = GetComponentInChildren<HittableObject>(true);
            }
            
            if (_battleUnit == null)
            {
                _battleUnit = GetComponentInChildren<BattleUnit>(true);
            }
        }

        protected virtual void Awake()
        {
            Assert.IsNotNull(_hittableObject, "_hittableObject != null");
            Assert.IsNotNull(_battleUnit, "_battleUnit != null");
            _hittableObject.OnDied += HittableObjectOnDied;
        }

        private void OnDestroy()
        {
            if(_hittableObject != null)
                _hittableObject.OnDied -= HittableObjectOnDied;
        }
        

        protected abstract void HittableObjectOnDied();

        public void InitRespawnBehaviour(UnitSpawnData unitSpawnData)
        {
            _spawnData = unitSpawnData;
        }
    }
}
