using System;
using Gameplay.Weapons.Projectiles;
using Interfaces;
using UnityEngine;

namespace Gameplay.Weapons.ConcreteWeapon
{
    public class RocketLauncher : Weapon,IReloadable,IEventableWeapon
    {
        [SerializeField] private Projectile _rocketPrefab;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _shootForce;
        
        private PoolMono<Projectile> _rocketPool;
        private Projectile _currentRocket;
        private bool _reloadActive = false;
        private bool _readyToFire = false;
        
        public event Action UseEvent;
        public event Action ReloadEvent;


        private void Awake()
        {
            const int countRocketsInPool = 3;
            _rocketPool = new PoolMono<Projectile>(_rocketPrefab, countRocketsInPool)
            {
                AutoExpand = true
            };
        }

        public override void Use()
        {
            if (_reloadActive || _currentRocket == null)
                return;

            _currentRocket.transform.SetParent(null);
            _currentRocket.Launch(_firePoint.forward * _shootForce);
            _currentRocket = null;
            _readyToFire = false;
            UseEvent?.Invoke();
        }

        public void Reload()
        {
            if (_reloadActive || _readyToFire)
                return;
            
            _reloadActive = true;
            ReloadEvent?.Invoke();
        }

        private void SetCurrentRocket()
        {
            var freeRocket = _rocketPool.GetFreeElement();
            if (freeRocket == null)
                return;
            
            freeRocket.transform.SetParent(_firePoint);
            freeRocket.transform.SetLocalPositionAndRotation(Vector3.zero,Quaternion.Euler(0,180,0));
            
            _currentRocket = freeRocket;
            _reloadActive = false;
            _readyToFire = true;
        }
    }
}