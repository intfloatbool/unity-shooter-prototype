using _Scripts.Battle.Abilities;
using _Scripts.Static;

namespace _Scripts.Battle.Commands.Concrete
{
    public class ClearAbilityCommand : UnitCommandBase
    {
        private readonly UnitAbilityStatusController _abilityStatusController;

        public ClearAbilityCommand(BattleUnit unit) : base(unit)
        {
            _abilityStatusController = unit.GetComponentInChildren<UnitAbilityStatusController>(true);
            GameHelper.CheckForNull(_abilityStatusController, nameof(RandomAbilityCommand));
        }

        public override void Execute()
        {
            if (_abilityStatusController != null)
            {
                _abilityStatusController.ClearAbilities();
            }
        }
    }
}