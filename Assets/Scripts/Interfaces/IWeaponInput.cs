using System;

namespace Interfaces
{
    public interface IWeaponInput
    {
        public event Action StartWeaponUsing;
        public event Action EndWeaponUsing;
        public event Action ReloadWeapon;
        public event Action DropWeapon;
    }
}