using System;
using Enums;
using Interfaces;
using UnityEngine;

namespace Gameplay.World.Items
{
    public class Radio : MonoBehaviour,IInteractable
    {
        [field: SerializeField] public Sprite Image { get; set; }
        [field: SerializeField] public InteractionType Action { get; set; }

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private bool _activeOnStart = false;

        private void Start()
        {
            if (_activeOnStart)
            {
                _audioSource.Play();
            }
        }

        public void Interact()
        {
            var radioActive = _audioSource.isPlaying;

            if (radioActive)
            {
                _audioSource.Stop();
            }
            else
            {
                _audioSource.Play();   
            }
        }
    }
}