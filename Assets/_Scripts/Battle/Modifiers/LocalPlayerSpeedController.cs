using _Scripts.Battle.Base;
using _Scripts.Static;
using Invector.vCharacterController;
using UnityEngine;

namespace _Scripts.Battle.Modifiers
{
    public class LocalPlayerSpeedController : UnitSpeedControllerBase
    {
        [SerializeField] private vThirdPersonController _thirdPersonController;

        private float _initialFreeSpeed;
        private float _initialRunningSpeed;
        
        private void Awake()
        {
            GameHelper.CheckForNull(_thirdPersonController);

            _initialFreeSpeed = _thirdPersonController.freeSpeed.walkSpeed;
            _initialRunningSpeed = _thirdPersonController.freeSpeed.runningSpeed;
        }
        

        protected override void ControlSpeedLoop()
        {
            if (_thirdPersonController != null)
            {
                _thirdPersonController.freeSpeed.walkSpeed = _initialFreeSpeed * _currentSpeedMultipler;
                _thirdPersonController.freeSpeed.runningSpeed = _initialRunningSpeed * _currentSpeedMultipler;
            }
        }
    }
}