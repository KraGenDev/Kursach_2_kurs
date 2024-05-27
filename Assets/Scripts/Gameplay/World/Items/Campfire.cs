using Enums;
using Interfaces;
using UnityEngine;

namespace Gameplay.World.Items
{
    public class Campfire : MonoBehaviour,IInteractable
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private bool _active = false;

        [field: SerializeField] public Sprite Image { get; set; }
        [field: SerializeField] public InteractionType Action { get; set; }

        private void Start()
        {
            if (_active)
            {
                _particleSystem.Play();
            }
        }

        public void Interact()
        {
            if (_active)
            {
                _particleSystem.Stop();
            }
            else
            {
                _particleSystem.Play();
            }

            _active = !_active;
        }
    }
}