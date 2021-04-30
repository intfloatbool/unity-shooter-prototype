using _Scripts.Battle;
using _Scripts.Common;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts
{
    public class PlayerCameraAimer : MonoBehaviour, IOwnerable
    {
        [SerializeField] private TransformRotationCopier[] _rotationCopiers;
        
        public BattleUnit Owner { get; private set; }
        

        public void InitOwner(BattleUnit owner)
        {
            if (!owner.IsLocalPlayer)
                return;
            
            this.Owner = owner;

            if (Camera.main != null)
            {
                foreach (var rotationCopier in _rotationCopiers)
                {
                    rotationCopier.Target = Camera.main.transform;   
                }
            }
        }
    }
}
