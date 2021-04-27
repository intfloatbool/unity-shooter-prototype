using System;
using _Scripts.Settings;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Battle.Weapons
{
    public class FirearmWeapon : WeaponBase
    {
        [SerializeField] private WeaponParams _weaponParams;

        private void Awake()
        {
            Assert.IsNotNull(_weaponParams, "_weaponParams != null");
        }

        protected override void OnShotStart()
        {
            throw new NotImplementedException();
        }
    }
}
