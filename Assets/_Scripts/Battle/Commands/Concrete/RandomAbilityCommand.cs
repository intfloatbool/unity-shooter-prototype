using System.Linq;
using _Scripts.Battle.Abilities;
using _Scripts.Settings;
using _Scripts.Static;
using UnityEngine;

namespace _Scripts.Battle.Commands.Concrete
{
    public class RandomAbilityCommand : UnitCommandBase
    {
        private PassiveAbilityParams[] _potentialAbilities;
        private UnitAbilityStatusController _abilityStatusController;
        
        public RandomAbilityCommand(BattleUnit unit, PassiveAbilityParams[] potentialAbilities) : base(unit)
        {
            _potentialAbilities = potentialAbilities.ToArray();

            _abilityStatusController = unit.GetComponentInChildren<UnitAbilityStatusController>(true);
            GameHelper.CheckForNull(_abilityStatusController, nameof(RandomAbilityCommand));
        }

        public override void Execute()
        {
            if (_abilityStatusController != null && _potentialAbilities.Length > 0)
            {
                _abilityStatusController.ApplyPassiveAbility(_potentialAbilities[Random.Range(0, _potentialAbilities.Length)]);
            }
        }
    }
}