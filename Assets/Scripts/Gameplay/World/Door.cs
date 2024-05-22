using Enums;
using Interfaces;
using UnityEngine;

namespace Gameplay.World
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private HingeJoint _doorObject;

        private bool _isOpen = false;

        [field: SerializeField] public Sprite Image { get; set; }
        [field: SerializeField] public InteractionType Action { get; set; }

        public void Interact()
        {
            var jointMotor = _doorObject.motor;
            
            jointMotor.targetVelocity = _isOpen 
                ? 90
                : -90;

            _doorObject.motor = jointMotor;
            _isOpen = !_isOpen;
        }
    }
}