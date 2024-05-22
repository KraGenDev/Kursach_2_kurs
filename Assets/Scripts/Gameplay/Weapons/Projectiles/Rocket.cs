using System.Collections;
using Gameplay.Weapon.Projectiles;
using Interfaces;
using UnityEngine;

namespace Gameplay.Weapons.Projectiles
{
    public class Rocket : TimedProjectile
    {
        [SerializeField] private int _damage;
        [SerializeField] private ParticleSystem _effect;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody ??= GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _rigidbody.isKinematic = true;
            _effect.Stop();
        }

        public override void Launch(Vector3 direction)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(direction,ForceMode.Impulse);
            _effect.Play();
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
        
        protected override IEnumerator DeactivateAfterTime()
        {
            yield return new WaitForSeconds(_timeToDestroy);
            CallHitEvent();
            gameObject.SetActive(false);
        }
    }
}