using Interfaces;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class WeaponEffects : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _shootEffect;
        [SerializeField] private Weapon _weapon;

        private IEventableWeapon _eventableWeapon;

        
        private void OnEnable()
        {
            _eventableWeapon ??= _weapon.GetComponent<IEventableWeapon>();

            if (_eventableWeapon != null)
            {
                _eventableWeapon.UseEvent += OnShoot;
            }
        }

        private void OnDisable()
        {
            if (_eventableWeapon != null)
            {
                _eventableWeapon.UseEvent -= OnShoot;
            }
        }

        private void OnShoot()
        {
            _shootEffect?.Play();
        }
    }
}