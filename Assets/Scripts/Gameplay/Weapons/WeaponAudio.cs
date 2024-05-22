using Interfaces;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class WeaponAudio : MonoBehaviour
    {
        [SerializeField] protected Weapon _weapon;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _useWeaponClip;
        [SerializeField] private AudioClip _reloadWeaponClip;

        protected IEventableWeapon _eventableWeapon;

        protected virtual void Awake()
        {
            _eventableWeapon = _weapon.GetComponent<IEventableWeapon>();
            if (_eventableWeapon != null)
            {
                _eventableWeapon.UseEvent += OnWeaponUse;
                _eventableWeapon.ReloadEvent += OnWeaponReload;
            }
        }

        protected virtual void OnDestroy()
        {
            if (_eventableWeapon != null)
            {
                _eventableWeapon.UseEvent -= OnWeaponUse;
                _eventableWeapon.ReloadEvent -= OnWeaponReload;
            }
        }

        protected void OnWeaponReload()
        {
            _audioSource.clip = _reloadWeaponClip;
            _audioSource.Play();
        }

        protected void OnWeaponUse()
        {
            _audioSource.clip = _useWeaponClip;
            _audioSource.Play();
        }
    }
}