using _Scripts.Battle.Base;
using _Scripts.Battle.Weapons;
using _Scripts.Settings;
using _Scripts.Static;

namespace _Scripts.Battle.Setupable
{
    public class DefaultUnitCharacterSettingsAccepter : CharacterSettingsAccepterBase
    {
        public override void AcceptSettings(PlayerCharacteristicsData data)
        {
            var speedController = GetComponentInChildren<UnitSpeedControllerBase>();
            GameHelper.CheckForNull(speedController);
            if (speedController != null)
            {
                speedController.SetDefaultSpeedMultipler(data.moveSpeedMultipler);
            }

            _battleUnit.WeaponController.DamageMultipler = data.damageDealingMultipler;
            _battleUnit.WeaponController.DamageScatterMultipler = data.damageScatterMultipler;

            foreach (var weapon in _battleUnit.WeaponController.PossibleWeapons)
            {
                if (weapon is FirearmWeapon firearmWeapon)
                {
                    firearmWeapon.ShotDelayMultipler = data.attackSpeedMultipler;
                }
            }

            _battleUnit.HittableObject.DamageTakingMultipler = data.damageTakingMultipler;
        }
    }
}