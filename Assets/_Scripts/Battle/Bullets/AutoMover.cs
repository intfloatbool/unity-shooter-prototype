using UnityEngine;

namespace _Scripts.Battle.Bullets
{
    public class AutoMover : MonoBehaviour
    {
        [SerializeField] private Vector3 _direction;
        [SerializeField] private float _speed = 7f;

        private void Update()
        {
            transform.Translate(_direction * _speed * Time.deltaTime);
        }
    }
}
