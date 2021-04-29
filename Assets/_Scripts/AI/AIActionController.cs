using _Scripts.Battle;
using _Scripts.Battle.Weapons;
using _Scripts.Settings;
using _Scripts.Static;
using Invector.vCharacterController;
using UnityEngine;

namespace _Scripts.AI
{
    public class AIActionController : MonoBehaviour
    {
        [SerializeField] private BattleUnit _battleUnit;
        [SerializeField] private AISettings _aiSettings;
        [SerializeField] private AIStateController _stateController;
        [SerializeField] private vThirdPersonController _personController;
        [SerializeField] private Transform _weaponHolderAnchor;
        
        [Space]
        [Header("Runtime")]
        [SerializeField] private AIAction _currentAction;
        [SerializeField] private BattleUnit _closestEnemy;
        
        private UnitsHolder _unitsHolder;


        private float _attackTimer;

        private void Awake()
        {
            GameHelper.CheckForNull(_stateController);
            GameHelper.CheckForNull(_battleUnit);
            GameHelper.CheckForNull(_aiSettings);
            GameHelper.CheckForNull(_personController);
            GameHelper.CheckForNull(_weaponHolderAnchor);
        }

        public void DoAction(AIAction aiAction)
        {
            var currentState = _stateController.CurrentState;
            switch (currentState)
            {
                case AIState.STAYING:
                {
                    if (aiAction == AIAction.WALK_TO_ENEMY)
                    {
                        _stateController.SetState(AIState.WALKING);
                    }
                    else if (aiAction == AIAction.ATTACK_ENEMY)
                    {
                        _stateController.SetState(AIState.ATTACK_TARGET);
                    }
                    break;
                }
                case AIState.WALKING:
                {
                    if (aiAction == AIAction.ATTACK_ENEMY)
                    {
                        _stateController.SetState(AIState.ATTACK_TARGET);
                    }
                    else if (aiAction == AIAction.STOP)
                    {
                        _stateController.SetState(AIState.STAYING);
                    }
                    break;
                }
                case AIState.ATTACK_TARGET:
                {
                    if (aiAction == AIAction.STOP)
                    {
                        _stateController.SetState(AIState.STAYING);
                    }
                    break;
                }
            }

            _currentAction = aiAction;
        }

        private void Start()
        {
            _unitsHolder = _battleUnit?.BattleResources?.UnitsHolder;
            GameHelper.CheckForNull(_unitsHolder);
        }

        private void Update()
        {
            AiBehaviourLoop();
        }

        protected virtual void AiBehaviourLoop()
        {
            if (_unitsHolder == null)
            {
                return;
            }

            LookForEnemy();
            
            if (_closestEnemy == null)
            {
                DoAction(AIAction.STOP);
                Stop();
                return;
            }

            var offsetFromTarget = _closestEnemy.transform.position - transform.position;
            var sqrLen = offsetFromTarget.sqrMagnitude;
            var sqrDistance = _aiSettings.DistanceToStartAttack * _aiSettings.DistanceToStartAttack;
            
            if (sqrLen < sqrDistance)
            {
                DoAction(AIAction.ATTACK_ENEMY);
            }
            else
            {
                DoAction(AIAction.WALK_TO_ENEMY);
            }

            if (_currentAction == AIAction.WALK_TO_ENEMY)
            {
                HandleWalkLoop();
            }
            else if (_currentAction == AIAction.ATTACK_ENEMY)
            {
                HandleAttackLoop();
            }

        }

        private void Stop()
        {
            _personController.input = Vector3.zero;
        }

        private void HandleWalkLoop()
        {
            //TODO: Fix corrupted moving
            var offsetFromTarget = _closestEnemy.transform.position - transform.position;
            var directionToTarget =
                new Vector3(
                    Mathf.Clamp(offsetFromTarget.x, -1f, 1f),
                    Mathf.Clamp(offsetFromTarget.y, -1f, 1f),
                    Mathf.Clamp(offsetFromTarget.z, -1f, 1f)
                );

            _personController.input.x = directionToTarget.x;
            _personController.input.z = directionToTarget.z;
        }

        private void HandleAttackLoop()
        {
            
            if (_attackTimer > _aiSettings.FireDelay)
            {
                TryShot();
                _attackTimer = 0;
            }
            
            _attackTimer += Time.deltaTime;
        }

        private void TryShot()
        {
            var weapon = _battleUnit.WeaponController.CurrentWeapon;
            if (weapon is FirearmWeapon firearmWeapon)
            {
                if (firearmWeapon.currentMagazine <= 0 && !firearmWeapon.IsOnReloadProecess)
                {
                    firearmWeapon.ReloadWeapon();
                }
            }
            _battleUnit.WeaponController.Shot();
            
            if (_weaponHolderAnchor != null)
            {
                var offset = _aiSettings.CalculateFireOffset();
                var targetPos = _closestEnemy.transform.position + offset;
                
                _weaponHolderAnchor.LookAt(targetPos);
            }
        }

        private void LookForEnemy()
        {
            float closestDistance = Mathf.Infinity;

            foreach (var aliveUnit in _unitsHolder.AliveUnitsBuffer)
            {
                if(aliveUnit == null)
                    continue;
                
                if (aliveUnit.TeamController.IsTeammate(_battleUnit))
                {
                    continue;
                }

                var distance = Vector3.Distance(transform.position, aliveUnit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _closestEnemy = aliveUnit;
                }

            }
        }
    }
}
