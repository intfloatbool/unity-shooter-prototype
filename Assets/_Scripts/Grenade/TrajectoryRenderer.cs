using System;
using _Scripts.Static;
using UnityEngine;

namespace _Scripts.Grenade
{
    struct LaunchData
    {
        public readonly Vector3 initalVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initalVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
    
    [RequireComponent(typeof(LineRenderer))]
    public class TrajectoryRenderer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        
        [SerializeField] private int _resolution;
        [SerializeField] private float _height;

        [Space]
        [Header("Runtime")]
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _targetPos;
        [SerializeField] private Vector3 _gravity;   

        private void OnValidate()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Awake()
        {
            GameHelper.CheckForNull(_lineRenderer);
            
        }

        private void Start()
        {
            _gravity = Physics.gravity;
        }


        private void JumpCarByTrajectory()
        {
            //var launchData = CalculateLaunchData(_carRb.transform, _target);
            //_carRb.velocity = launchData.initalVelocity;
            //Debug.Log("Start launch!: " + launchData.initalVelocity);
        }

        private LaunchData CalculateLaunchData(Transform source, Vector3 target)
        {
            var sourcePos = source.position;
            var targetPos = target;
            
            float displacementY = target.y - sourcePos.y;
            Vector3 displacementXZ = new Vector3(
                targetPos.x - sourcePos.x,
                0,
                targetPos.z - sourcePos.z
            );

            float height = _height;

            float time = Mathf.Sqrt(-2 * height / _gravity.y)
                         + Mathf.Sqrt(2 * (displacementY - height) / _gravity.y);
            
            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * _gravity.y * height);
            Vector3 velocityXZ = displacementXZ / time;
            return new LaunchData(velocityXZ + velocityY * Mathf.Sign(height), time);
        }
        
        private LaunchData CalculateLaunchData(Transform source, Transform target)
        {
            return CalculateLaunchData(source, target.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            DrawTrajectory();
        }

        private void DrawTrajectory()
        {
            var launchData = _target != null 
                ? CalculateLaunchData(transform, _target) 
                : CalculateLaunchData(transform, _targetPos);

            Vector3 prevPoint = transform.position;
            for (int i = 1; i <= _resolution; i++)
            {
                float simulationTime = i / (float) _resolution * launchData.timeToTarget;
                Vector3 displacement = launchData.initalVelocity * simulationTime + Vector3.up *
                    _gravity.y * simulationTime * simulationTime / 2f;
                Vector3 drawPoint = transform.position + displacement;
                
                Gizmos.DrawLine(prevPoint, drawPoint);
                prevPoint = drawPoint;
            }
        }
    }
}
