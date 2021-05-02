using _Scripts.Battle.Commands;
using _Scripts.Battle.Commands.Concrete;
using _Scripts.Settings;
using _Scripts.Static;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Battle
{
    public class UnitActionsController : MonoBehaviour
    {
        [SerializeField] private PassiveAbilityParams[] _possibleAbilities;
        [SerializeField] private BattleUnit _battleUnit;
        [SerializeField] private UnitCommandInvoker _commandInvoker;
        
        /// Commands is very useful to setup custom controls in future!
        
        private UnitCommandBase _shotCommand;
        private UnitCommandBase _swapWeaponsCommand;
        private UnitCommandBase _reloadWeaponCommand;
        private UnitCommandBase _selfKillCommand;
        private UnitCommandBase _grenadeThrowCommand;
        private UnitCommandBase _randomAbilityCommand;
        private UnitCommandBase _clearAbilityCommand;

        private void OnValidate()
        {
            if (_battleUnit == null)
            {
                _battleUnit = GetComponent<BattleUnit>();
            }
            
            if (_commandInvoker == null)
            {
                _commandInvoker = GetComponent<UnitCommandInvoker>();
            }
        }

        private void Awake()
        {
            Assert.IsNotNull(_battleUnit, "_battleUnit != null");
            Assert.IsNotNull(_commandInvoker, "_commandInvoker != null");

            InitActionCommands();
        }

        private void InitActionCommands()
        {
            _shotCommand = new UseWeaponCommand(_battleUnit);
            _swapWeaponsCommand = new SwapWeaponsCommand(_battleUnit);
            _reloadWeaponCommand = new ReloadWeaponCommand(_battleUnit);
            _selfKillCommand = new SelfKillCommand(_battleUnit);
            _grenadeThrowCommand = new GrenadeControllerSwitchCommand(_battleUnit);
            _randomAbilityCommand = new RandomAbilityCommand(_battleUnit, _possibleAbilities);
            _clearAbilityCommand = new ClearAbilityCommand(_battleUnit);
        }

        private void Update()
        {
            HandleActionsLoop();
        }

        private void HandleActionsLoop()
        {
            if (Input.GetMouseButton(GameHelper.InputConstants.LEFT_MOUSE))
            {
                _commandInvoker.RunCommand(_shotCommand);
            }
            
            if (Input.GetMouseButtonDown(GameHelper.InputConstants.MIDDLE_MOUSE))
            {
                _commandInvoker.RunCommand(_swapWeaponsCommand);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                _commandInvoker.RunCommand(_reloadWeaponCommand);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                _commandInvoker.RunCommand(_selfKillCommand);
            }
            
            if (Input.GetKeyDown(KeyCode.L))
            {
                _commandInvoker.RunCommand(new SelfDamageCommand(_battleUnit, 15));
            }
            
            if (Input.GetKeyDown(KeyCode.G))
            {
                _commandInvoker.RunCommand(_grenadeThrowCommand);
            }
            
            if (Input.GetKeyDown(KeyCode.N))
            {
                _commandInvoker.RunCommand(_randomAbilityCommand);
            }
            
            if (Input.GetKeyDown(KeyCode.M))
            {
                _commandInvoker.RunCommand(_clearAbilityCommand);
            }
        }
    }
}
