using _Scripts.Battle.Base;
using _Scripts.Battle.Weapons;
using _Scripts.Static;

namespace _Scripts.Battle.Setupable
{
    public class DefaultUnitCharacterSettingsAccepter : CharacterSettingsAccepterBase
    {
        public override void AcceptSettings()
        {
            var speedController = GetComponentInChildren<UnitSpeedControllerBase>();
            GameHelper.CheckForNull(speedController);
            if (speedController != null)
            {
                speedController.SetDefaultSpeedMultipler(_characterSettings.moveSpeedMultipler);
            }

            _battleUnit.WeaponController.DamageMultipler = _characterSettings.damageDealingMultipler;
            _battleUnit.WeaponController.DamageScatterMultipler = _characterSettings.damageScatterMultipler;

            foreach (var weapon in _battleUnit.WeaponController.PossibleWeapons)
            {
                if (weapon is FirearmWeapon firearmWeapon)
                {
                    firearmWeapon.ShotDelayMultipler = _characterSettings.attackSpeedMultipler;
                }
            }

            _battleUnit.HittableObject.DamageTakingMultipler = _characterSettings.damageTakingMultipler;
        }
    }
}