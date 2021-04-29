using System;
using UnityEngine;

namespace _Scripts.AI
{
    public class AIStateController : MonoBehaviour
    {
        [SerializeField] protected AIState _defaultState = AIState.STAYING;
        
        [Space]
        [Header("Runtime")]
        [SerializeField] protected AIState _currentState;

        public AIState CurrentState => _currentState;


        public event Action<AIState> OnStateChanged;

        protected virtual void Start()
        {
            if (_defaultState != AIState.NONE)
            {
                SetState(_defaultState);
            }
        }

        public void SetState(AIState state)
        {
            _currentState = state;
            OnStateChanged?.Invoke(_currentState);
        }
    }
}
