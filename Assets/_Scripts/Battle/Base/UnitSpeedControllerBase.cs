using UnityEngine;

namespace _Scripts.Battle.Base
{
    public abstract class UnitSpeedControllerBase : MonoBehaviour
    {
        [SerializeField] protected float _defaultSpeedMultipler = 1f;
        
        [Space]
        [Header("Runtime")]
        [SerializeField] protected float _currentSpeedMultipler = 1f;

        public void SetDefaultSpeedMultipler(float defaultSpeedMultipler)
        {
            if (defaultSpeedMultipler < 0)
            {
                defaultSpeedMultipler = 0;
            }
            
            _defaultSpeedMultipler = defaultSpeedMultipler;
        }
        
        public void SetCurrentSpeedMultipler(float speedMultipler)
        {
            if (speedMultipler < 0)
            {
                speedMultipler = 0;
            }

            _currentSpeedMultipler = speedMultipler;
        }

        public void ResetSpeedToDefault()
        {
            SetCurrentSpeedMultipler(_defaultSpeedMultipler);
        }

        private void Update()
        {
            ControlSpeedLoop();
        }

        protected abstract void ControlSpeedLoop();
        
    }
}