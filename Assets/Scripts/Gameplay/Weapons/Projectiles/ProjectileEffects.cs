using Unity.Mathematics;
using UnityEngine;

namespace Gameplay.Weapons.Projectiles
{
    public class ProjectileEffects : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private ParticleSystem _hitEffect;

        private void Awake()
        {
            if (_projectile != null)
            {
                _projectile.HitEvent += OnHit;
            }
        }

        private void OnDestroy()
        {
            if (_projectile != null)
            {
                _projectile.HitEvent -= OnHit;
            }
        }

        private void OnHit()
        {
            _hitEffect.transform.SetParent(transform);
            _hitEffect.transform.SetLocalPositionAndRotation(Vector3.zero, quaternion.Euler(Vector3.zero));
            _hitEffect.transform.SetParent(null);
            _hitEffect.Play();
        }
    }
}