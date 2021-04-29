using _Scripts.Battle.Weapons;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Battle.Setupable
{
    public class BotPlayerCharacterSettingsAccepter : CharacterSettingsAccepterBase
    {
        public override void AcceptSettings()
        {
            if (_battleUnit.TryGetComponent(out NavMeshAgent navMeshAgent))
            {
                navMeshAgent.speed *= _characterSettings.moveSpeedMultipler;
            }
            else
            {
                Debug.LogError($"{nameof(NavMeshAgent)} is missing!");
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