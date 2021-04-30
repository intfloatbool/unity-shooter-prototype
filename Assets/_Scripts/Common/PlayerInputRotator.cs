using UnityEngine;

namespace _Scripts.Common
{
    public class PlayerInputRotator : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private string _xAxisName = "Mouse X";
        [SerializeField] private string _yAxisName = "Mouse Y";
        
        private Quaternion _basicRotation;

        private void Awake()
        {
            _basicRotation = transform.localRotation;
        }

        private void OnEnable()
        {
            ResetRotation();
        }

        private void ResetRotation()
        {
            transform.localRotation = _basicRotation;
        }

        private void Update()
        {
            var rotDir = new Vector3(
                -Input.GetAxis(_yAxisName),
                Input.GetAxis(_xAxisName),
                0
            );
            
            
            transform.Rotate(
                rotDir * _rotationSpeed * Time.deltaTime    
                );
        }
    }
}
