using Interfaces;
using UnityEngine;
using Zenject;

namespace Gameplay.Character
{
    public class CharacterWeapon : MonoBehaviour
    {
        [SerializeField] private Weapons.Weapon _weapon;
        [SerializeField] private Transform _weaponContainer;
        
        [Inject] private IWeaponInput _weaponInput;

        private void OnEnable()
        {
            _weaponInput.StartWeaponUsing += (() => SetWeaponUsingStatus(true));
            _weaponInput.EndWeaponUsing += (() => SetWeaponUsingStatus(false));
            _weaponInput.ReloadWeapon += ReloadWeapon;
            _weaponInput.DropWeapon += DropWeapon;
        }

        private void OnDisable()
        {
            _weaponInput.ReloadWeapon -= ReloadWeapon;
            _weaponInput.DropWeapon -= DropWeapon;
        }

        public void SetWeapon(Weapons.Weapon weapon)
        {
            if (_weapon != null)
            {
                DropWeapon();
            }
            
            weapon.GetComponent<IPickable>()?.Pick(_weaponContainer);
            _weapon = weapon;
        }

        private void ReloadWeapon()
        {
            if (_weapon != null)
            {
                _weapon.GetComponent<IReloadable>()?.Reload();
            }
        }

        private void DropWeapon()
        {
            if (_weapon == null) 
                return;

            if (_weapon.TryGetComponent<IDroppable>(out var droppableWeapon))
            {
                droppableWeapon.Drop();
                _weapon.GetComponent<ICyclicalWeapon>()?.StopUsing();
                _weapon = null;
            }
        }

        private void SetWeaponUsingStatus(bool status)
        {
            if (status)
            {
                _weapon?.Use();
            }
            else
            {
                _weapon?.GetComponent<ICyclicalWeapon>()?.StopUsing();
            }
        }
    }
}