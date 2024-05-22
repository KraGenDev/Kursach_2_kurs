using System;
using System.Collections;
using Interfaces;
using UnityEngine;

namespace Gameplay.Weapons.ConcreteWeapon
{
    public class Lightsaber : Weapon,IReloadable, IEventableWeapon, ICyclicalWeapon
    {
        [SerializeField] private float _requiredTimeForCyclicalUsing = 1;
        [SerializeField] private float _attackDistance = 2;
        [SerializeField] private int _damage = 10;
        [SerializeField] private LayerMask _layerMask;

        private float _timeReminded = 0;
        private bool _used = false;
        private bool _cyclicalUsingCall = false;

        public event Action UseEvent;
        public event Action ReloadEvent;
        public event Action CyclicallyUsingEvent;
        public event Action StopCyclicalUsingEvent;

        
        private void Update()
        {
            if (!_used || _cyclicalUsingCall)
                return;
            
            _timeReminded += Time.deltaTime;

            if (_timeReminded >= _requiredTimeForCyclicalUsing)
            {
                CyclicallyUsingEvent?.Invoke();
                _cyclicalUsingCall = true;
            }
        }

        public override void Use()
        {
            _used = true;
            UseEvent?.Invoke();
        }

        public void Reload()
        {
            ReloadEvent?.Invoke();
        }

        private void LaunchRay()
        {
            var camera = Camera.main;
            var rayOrigin = camera.transform.position;
            var rayDirection = camera.transform.forward;
                
            if (Physics.Raycast(rayOrigin, rayDirection, out var hit, _attackDistance, _layerMask))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent(out IDamageable damageable))
                    {
                        damageable.TakeDamage(_damage);
                    }
                }
            }
        }

        public void StopUsing()
        {
            _used = false;
            _cyclicalUsingCall = false;
            _timeReminded = 0;
            StopCyclicalUsingEvent?.Invoke();
        }

        IEnumerator ICyclicalWeapon.CyclicalUsing()
        {
            throw new NotImplementedException();
        }
    }
}