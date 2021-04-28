using UnityEngine;

namespace _Scripts
{
    public class CameraLooker : MonoBehaviour
    {
        [SerializeField] private Vector3 _worldUp = Vector3.up;
        [SerializeField] private Camera _specificCamera;
        
        [Space]
        [Header("Runtime")]
        [SerializeField]private Camera _camera;
        private void Awake()
        {
            if (_specificCamera != null)
            {
                _camera = _specificCamera;
            }
            else
            {
                _camera = Camera.main;
            }
        }

        private void Update()
        {
            if (_camera == null)
                return;
            transform.LookAt(_camera.transform, _worldUp);
        }
    }
}
