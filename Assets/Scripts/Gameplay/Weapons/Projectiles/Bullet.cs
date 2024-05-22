using Gameplay.Weapon.Projectiles;
using Interfaces;
using UnityEngine;

namespace Gameplay.Weapons.Projectiles
{
    public class Bullet : TimedProjectile
    {
        [SerializeField] private int _damage;
        [SerializeField] private TrailRenderer _trailRenderer;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody ??= GetComponent<Rigidbody>();
        }

        public override void Launch(Vector3 direction)
        {
            _trailRenderer?.Clear();
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(direction,ForceMode.Impulse);
            StartCoroutine(DeactivateAfterTime());
        }

        protected override void OnCollisionEnter(Collision other)
        {
            CallHitEvent();
            if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(_damage);
            }

            gameObject.SetActive(false);
        }
    }
}