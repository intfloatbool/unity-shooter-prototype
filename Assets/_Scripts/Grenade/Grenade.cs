using _Scripts.Static;
using UnityEngine;

namespace _Scripts.Grenade
{
    public class Grenade : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        public Rigidbody Rb => _rigidbody;

        private bool _isActivated;
        
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Awake()
        {
            GameHelper.CheckForNull(_rigidbody, this);
        }

        public void Activate()
        {
            Invoke(nameof(ActivateByTime), 1f);
        }

        private void ActivateByTime()
        {
            _isActivated = true;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!_isActivated)
                return;
            
            Explode();
        }

        public void ResetGrenade()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _isActivated = false;
            CancelInvoke(nameof(ActivateByTime));
        }

        private void Explode()
        {
            _isActivated = false;
            gameObject.SetActive(false);
        }
    }
}
