using System;
using _Scripts.Battle;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts
{

    public class PlayerCamera : MonoBehaviour, IOwnerable
    {

        [SerializeField] private vThirdPersonCamera _thirdPersonCamera;
        public vThirdPersonCamera ThirdPersonCamera => _thirdPersonCamera;
        
        private void Awake()
        {
            Assert.IsNotNull(_thirdPersonCamera, "_thirdPersonCamera != null");
        }

        public BattleUnit Owner { get; private set; }
        public void InitOwner(BattleUnit owner)
        {
            this.Owner = owner;
            _thirdPersonCamera.target = owner.transform;
        }

        public void SwitchCamera()
        {
            
        }
    }
}
