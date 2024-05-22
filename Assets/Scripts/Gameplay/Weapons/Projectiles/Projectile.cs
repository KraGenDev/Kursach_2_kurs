using System;
using UnityEngine;

namespace Gameplay.Weapons.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        public event Action HitEvent;


        public abstract void Launch(Vector3 direction);
        
        protected void CallHitEvent() => HitEvent?.Invoke();
        
        protected abstract void OnCollisionEnter(Collision other);
    }
}