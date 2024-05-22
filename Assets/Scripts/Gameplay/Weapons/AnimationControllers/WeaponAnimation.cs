using Interfaces;
using UnityEngine;

namespace Gameplay.Weapons.AnimationControllers
{
    public class WeaponAnimation : MonoBehaviour
    {
        [SerializeField] protected Weapon _weapon;
        [SerializeField] protected Animator _animator;

        protected IEventableWeapon _eventableWeapon;
        
        private readonly int _shoot = Animator.StringToHash(ShootAnimationName);
        private readonly int _reload = Animator.StringToHash(ReloadAnimationName);

        protected const string ShootAnimationName = "Shoot";
        protected const string ReloadAnimationName = "Reload";

        protected virtual void Awake()
        {
            _eventableWeapon ??= _weapon.GetComponent<IEventableWeapon>();

            if (_eventableWeapon != null)
            {
                _eventableWeapon.UseEvent += OnShoot;
                _eventableWeapon.ReloadEvent += OnReload;
            }
        }

        protected virtual void OnDisable()
        {
            if (_eventableWeapon != null)
            {
                _eventableWeapon.UseEvent -= OnShoot;
                _eventableWeapon.ReloadEvent -= OnReload;
            }
        }

        protected virtual void OnShoot()
        {
            _animator.SetTrigger(_shoot);
        }

        protected virtual void OnReload()
        {
            _animator.SetTrigger(_reload);
        }
    }
}