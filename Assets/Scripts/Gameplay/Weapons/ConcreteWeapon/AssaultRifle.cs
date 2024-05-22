using System;
using System.Collections;
using Gameplay.Weapons.Projectiles;
using Interfaces;
using UnityEngine;

namespace Gameplay.Weapons.ConcreteWeapon
{
    public class AssaultRifle: AutomaticWeapon,IReloadable,IEventableWeapon
    {
        [SerializeField] private Projectile _bulletPrefab;
        [SerializeField] private int _ammoCount;
        [SerializeField] private float _shootingDelay;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _shootForce;

        private PoolMono<Projectile> _bulletPool;
        private int _actualAmmoCount;
        private bool _reloadActive = false;
        private Coroutine _shootingCoroutine;

        public event Action UseEvent;
        public event Action ReloadEvent;
        
        
        private void Awake()
        {
            FillPool();
        }

        private void FillPool()
        {
            _bulletPool = new PoolMono<Projectile>(_bulletPrefab, _ammoCount)
            {
                AutoExpand = true
            };

            _actualAmmoCount = _ammoCount;
        }
        
        public override void Use()
        {
            _shootingCoroutine = StartCoroutine(CyclicalUsing());
        }

        public override void StopUsing()
        {
            if (_shootingCoroutine == null)
                return;
            
            StopCoroutine(_shootingCoroutine);
        }

        public void Reload()
        {
            if(_reloadActive)
                return;

            _reloadActive = true;
            ReloadEvent?.Invoke();
        }

        private void ResetAmmoCount()
        {
            _actualAmmoCount = _ammoCount;
            _reloadActive = false;
        }

        protected override void Shoot()
        {
            if (_actualAmmoCount <= 0)
            {
                Debug.Log("No Ammo",gameObject);
                return;
            }

            var bullet = _bulletPool.GetFreeElement();
            var firePointTransform = _firePoint.transform;
            
            bullet.transform.SetPositionAndRotation(firePointTransform.position,firePointTransform.rotation);
            bullet.Launch(_firePoint.forward * _shootForce);
            _actualAmmoCount--;
            
            UseEvent?.Invoke();
        }

        private IEnumerator CyclicalUsing()
        {
            while (!_reloadActive)
            {
                Shoot();
                
                if(_actualAmmoCount <= 0) 
                    break;
                
                yield return new WaitForSeconds(_shootingDelay);
            }
        }
    }
}