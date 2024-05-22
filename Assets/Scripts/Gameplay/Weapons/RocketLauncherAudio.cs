using Interfaces;

namespace Gameplay.Weapons
{
    public class RocketLauncherAudio : WeaponAudio
    {
        protected override void Awake()
        {
            _eventableWeapon = _weapon.GetComponent<IEventableWeapon>();
            if (_eventableWeapon != null)
            {
                _eventableWeapon.UseEvent += OnWeaponUse;
            }
        }

        protected override void OnDestroy()
        {
            if (_eventableWeapon != null)
            {
                _eventableWeapon.UseEvent -= OnWeaponUse;
            }
        }
    }
}