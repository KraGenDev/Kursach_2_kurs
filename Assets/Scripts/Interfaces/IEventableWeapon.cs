using System;

namespace Interfaces
{
    public interface IEventableWeapon
    {
        public event Action UseEvent;
        public event Action ReloadEvent;
    }
}