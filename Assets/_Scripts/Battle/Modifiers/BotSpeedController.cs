using _Scripts.Battle.Base;
using _Scripts.Static;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Battle.Modifiers
{
    public class BotSpeedController : UnitSpeedControllerBase
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private float _initialFreeSpeed;

        private void Awake()
        {
            GameHelper.CheckForNull(_navMeshAgent);

            _initialFreeSpeed = _navMeshAgent.speed;
        }
        

        protected override void ControlSpeedLoop()
        {
            if (_navMeshAgent != null)
            {
                _navMeshAgent.speed = _initialFreeSpeed * _currentSpeedMultipler;
            }
        }
    }
}