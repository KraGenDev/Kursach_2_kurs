using System.Collections;
using Interfaces;
using UnityEngine;

namespace Gameplay.Weapons
{
    public abstract class AutomaticWeapon : Weapon,ICyclicalWeapon
    {
        private Coroutine _shootingCoroutine;
        
        public virtual void StopUsing()
        {
            StopCoroutine(_shootingCoroutine);
        }

        IEnumerator ICyclicalWeapon.CyclicalUsing()
        {
            throw new System.NotImplementedException();
        }

        protected virtual void Shoot() { }
    }
}