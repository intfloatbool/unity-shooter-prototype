using _Scripts.Battle;
using _Scripts.Common;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts
{
    public class PlayerCameraAimer : MonoBehaviour, IOwnerable
    {
        [SerializeField] private TransformRotationCopier _rotationCopier;
        
        public BattleUnit Owner { get; private set; }

        private void Awake()
        {
            Assert.IsNotNull(_rotationCopier, "_rotationCopier != null");
        }

        public void InitOwner(BattleUnit owner)
        {
            if (!owner.IsLocalPlayer)
                return;
            
            this.Owner = owner;

            if (Camera.main != null)
            {
                _rotationCopier.Target = Camera.main.transform;   
            }
        }
    }
}
