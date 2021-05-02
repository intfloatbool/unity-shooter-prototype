using _Scripts.Battle.Weapons;
using _Scripts.Grenade;
using _Scripts.Static;

namespace _Scripts.Battle.Commands.Concrete
{
    public class GrenadeControllerSwitchCommand : UnitCommandBase
    {
        private GrenadeController _grenadeController;
        private NullWeaponController _nullWeaponController;
        private WeaponControllerBase _defaultWeaponController;

        private bool _isGrenadeModeActivated;
        
        public GrenadeControllerSwitchCommand(BattleUnit unit) : base(unit)
        {
            _defaultWeaponController = unit.WeaponController;
            _nullWeaponController = unit.GetComponentInChildren<NullWeaponController>(true);
            _grenadeController = unit.GetComponentInChildren<GrenadeController>(true);
            
            GameHelper.CheckForNull(_defaultWeaponController, nameof(GrenadeControllerSwitchCommand));
            GameHelper.CheckForNull(_nullWeaponController, nameof(GrenadeControllerSwitchCommand));
            GameHelper.CheckForNull(_grenadeController, nameof(GrenadeControllerSwitchCommand));
            
            _grenadeController.OnGrenadeLaunchedCallback = OnGrenadeLaunchedCallback;
        }

        private void OnGrenadeLaunchedCallback()
        {
            _receiverUnit.SetWeaponController(_defaultWeaponController, 0.3f);
            _grenadeController.SetActiveGrenadeMode(false);
            _isGrenadeModeActivated = false;
        }
        
        public override void Execute()
        {
            _isGrenadeModeActivated = !_isGrenadeModeActivated;
            _grenadeController.SetActiveGrenadeMode(_isGrenadeModeActivated);
            if (_isGrenadeModeActivated == true)
            {
                _receiverUnit.SetWeaponController(_nullWeaponController);
            }
            else
            {
                _receiverUnit.SetWeaponController(_defaultWeaponController, 0.3f);
            }
        }
    }
}