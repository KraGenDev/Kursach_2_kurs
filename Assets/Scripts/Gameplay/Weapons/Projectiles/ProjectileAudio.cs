using UnityEngine;

namespace Gameplay.Weapons.Projectiles
{
    public class ProjectileAudio : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _hitClip;

        private void Awake()
        {
            _projectile.HitEvent += OnHit;
        }

        private void OnDestroy()
        {
            _projectile.HitEvent -= OnHit;
        }

        private void OnHit()
        {
            _audioSource.clip = _hitClip;
            _audioSource.Play();
        }
    }
}