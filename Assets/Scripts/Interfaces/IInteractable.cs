using Enums;
using UnityEngine;

namespace Interfaces
{
    public interface IInteractable
    {
        public Sprite Image { get; set; } // add [field: SerializeField] before 'public' for showing in inspector
        public InteractionType Action { get; set; } // here too 

        public void Interact();
    }
}