using _Scripts.Battle.Commands;
using _Scripts.Battle.Commands.Concrete;
using _Scripts.Static;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Battle
{
    public class UnitActionsController : MonoBehaviour
    {
        [SerializeField] private BattleUnit _battleUnit;
        [SerializeField] private UnitCommandInvoker _commandInvoker;

        private UnitCommandBase _shotCommand;
        private UnitCommandBase _swapWeaponsCommand;

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
            
            if (Input.GetMouseButtonDown(GameHelper.InputConstants.RIGHT_MOUSE))
            {
                _commandInvoker.RunCommand(_swapWeaponsCommand);
            }
        }
    }
}
