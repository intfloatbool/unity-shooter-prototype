using _Scripts.Battle.Weapons;
using Invector.vCharacterController;
using UnityEngine;

namespace _Scripts.Battle.Setupable
{
    public class LocalPlayerCharacterSettingsAccepter : CharacterSettingsAccepterBase
    {
        public override void AcceptSettings()
        {
            if (_battleUnit.TryGetComponent(out vThirdPersonController thirdPersonController))
            {
                thirdPersonController.freeSpeed.walkSpeed *= _characterSettings.moveSpeedMultipler;
                thirdPersonController.freeSpeed.runningSpeed *= _characterSettings.moveSpeedMultipler;
            }
            else
            {
                Debug.LogError($"{nameof(vThirdPersonController)} is missing!");
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