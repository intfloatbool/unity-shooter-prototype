using _Scripts.Static;
using UnityEngine;

namespace _Scripts.Grenade
{
    public struct LaunchData
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
    public class GrenadeTrajectory : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        
        [SerializeField] private int _resolution;
        [SerializeField] private float _height;

        [Space]
        [Header("Runtime")]
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _targetPos;
        [SerializeField] private float _gravity = -9.5f;
        
        private Vector3 _basicGravity;

        private Vector3[] _positionsBuffer;
        private bool _isChangedGravity;

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
            _positionsBuffer = new Vector3[_resolution + 1];
            _lineRenderer.positionCount = _positionsBuffer.Length;

            InitGravity();
        }
        
        private void InitGravity()
        {
            var neededGravity = Vector3.up * _gravity;
            if (!Physics.gravity.Equals(neededGravity))
            {
                _basicGravity = Physics.gravity;
                Physics.gravity = neededGravity;
                
                Debug.Log($"Gravity changed by {transform.root.name} to: " + neededGravity);
                _isChangedGravity = true;
            }
        }

        private void OnDestroy()
        {
            if (_isChangedGravity)
            {
                if (!Physics.gravity.Equals(_basicGravity))
                {
                    Physics.gravity = _basicGravity;
                    Debug.Log($"Gravity reseted by {transform.root.name} to: " + _basicGravity);
                }
            }
        }


        private void JumpCarByTrajectory()
        {
            //var launchData = CalculateLaunchData(_carRb.transform, _target);
            //_carRb.velocity = launchData.initalVelocity;
            //Debug.Log("Start launch!: " + launchData.initalVelocity);
        }

        public Vector3 CalculateVelocityForGrenade()
        {
            var launchData = CalculateLaunchData(transform, _target);
            return launchData.initalVelocity;
        }

        public LaunchData CalculateLaunchData(Transform source, Vector3 target)
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

            float time = Mathf.Sqrt(-2 * height / _gravity)
                         + Mathf.Sqrt(2 * (displacementY - height) / _gravity);
            
            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * _gravity * height);
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

            DrawTrajectoryGizmo();
        }

        private void DrawTrajectoryGizmo()
        {
            var launchData = _target != null 
                ? CalculateLaunchData(transform, _target) 
                : CalculateLaunchData(transform, _targetPos);

            Vector3 prevPoint = transform.position;
            for (int i = 1; i <= _resolution; i++)
            {
                float simulationTime = i / (float) _resolution * launchData.timeToTarget;
                Vector3 displacement = launchData.initalVelocity * simulationTime + Vector3.up *
                    _gravity * simulationTime * simulationTime / 2f;
                Vector3 drawPoint = transform.position + displacement;
                
                Gizmos.DrawLine(prevPoint, drawPoint);
                prevPoint = drawPoint;
            }
        }

        private void Update()
        {
            DrawTrajectoryLineRenderer();
        }

        private void DrawTrajectoryLineRenderer()
        {
            var launchData = _target != null 
                ? CalculateLaunchData(transform, _target) 
                : CalculateLaunchData(transform, _targetPos);

            Vector3 prevPoint = transform.position;
            int bufferIndex = 1;
            for (int i = 0; i < _resolution; i++)
            {
                float simulationTime = i / (float) _resolution * launchData.timeToTarget;
                Vector3 displacement = launchData.initalVelocity * simulationTime + Vector3.up *
                    _gravity * simulationTime * simulationTime / 2f;
                Vector3 drawPoint = transform.position + displacement;
                
                _positionsBuffer[i] = prevPoint;
                _positionsBuffer[bufferIndex] = drawPoint;
                
                prevPoint = drawPoint;

                bufferIndex++;
            }
            
            _lineRenderer.SetPositions(_positionsBuffer);
            
        }
    }
}
