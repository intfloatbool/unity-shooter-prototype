using UnityEngine;

namespace _Scripts.Battle.Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        protected HitData? _lastHitData;

        public void UpdateHitData(HitData hitData)
        {
            _lastHitData = hitData;
        }

        public virtual void Shot()
        {
            if (!_lastHitData.HasValue)
            {
                Debug.LogError($"{nameof(HitData)} is not setup!");
                return;
            }
            
            OnShotStart();
        }

        protected abstract void OnShotStart();
    }
}
