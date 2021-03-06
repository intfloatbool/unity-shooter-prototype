using UnityEngine;

namespace _Scripts.Common
{
    public class TransformRotationCopier : MonoBehaviour
    {
        [SerializeField] private bool _isInstant = true;
        [SerializeField] private float _followSpeed = 7f;
        
        [SerializeField] private Transform _target;
        [SerializeField] private bool _isFreezeByX;
        [SerializeField] private bool _isFreezeByY;
        [SerializeField] private bool _isFreezeByZ;
        public Transform Target
        {
            get => _target;
            set => _target = value;
        }
        
        private void Update()
        {
            if (_target.Equals(null))
                return;

            Quaternion targetRot = _target.rotation;

            var targetEuler = _target.eulerAngles;
            if (_isFreezeByX)
            {
                targetEuler.x = 0;
                targetRot = Quaternion.Euler(targetEuler);
            }

            if (_isFreezeByY)
            {
                targetEuler.y = 0;
                targetRot = Quaternion.Euler(targetEuler);
            }
            
            if (_isFreezeByZ)
            {
                targetEuler.z = 0;
                targetRot = Quaternion.Euler(targetEuler);
            }

            if (_isInstant)
            {
                transform.rotation = targetRot;   
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,  targetRot, _followSpeed * Time.deltaTime);
            }
        }
    }
}
