using UnityEngine;

namespace _Scripts.Battle.Bullets
{
    public class ProjectileDisabler : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _disableTime = 6f;
        [SerializeField] private float _collisionRadius = 0.2f;

        private void OnEnable()
        {
            Invoke(nameof(DisableOnTime), _disableTime);
        }

        private void DisableOnTime()
        {
            if(gameObject.activeInHierarchy)
                gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Physics.CheckSphere(transform.position, _collisionRadius, _layerMask))
            {
                CancelInvoke(nameof(DisableOnTime));
                gameObject.SetActive(false);
            }
        }
    }
}
