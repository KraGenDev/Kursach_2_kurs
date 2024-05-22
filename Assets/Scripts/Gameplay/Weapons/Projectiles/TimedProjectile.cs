using System.Collections;
using Gameplay.Weapons.Projectiles;
using UnityEngine;

namespace Gameplay.Weapon.Projectiles
{
    public class TimedProjectile : Projectile
    {
        [SerializeField] protected float _timeToDestroy;
        
        public override void Launch(Vector3 direction) { }

        protected override void OnCollisionEnter(Collision other) { }

        protected virtual IEnumerator DeactivateAfterTime()
        {
            yield return new WaitForSeconds(_timeToDestroy);
            gameObject.SetActive(false);
        }
    }
}