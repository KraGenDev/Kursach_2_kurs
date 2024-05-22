using Enums;
using Interfaces;
using Unity.Mathematics;
using UnityEngine;

namespace Gameplay.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class DroppableWeapon : MonoBehaviour,IDroppable,IPickable
    {
        private Rigidbody _rigidbody;
        private Animator _animator;
        
        [field: SerializeField] public Sprite Image { get; set; }
        [field: SerializeField] public InteractionType Action { get; set; }

        private void Awake()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            _animator = gameObject.GetComponent<Animator>();
        }
        
        public void Drop()
        {
            _rigidbody.isKinematic = false;
            if(_animator != null) _animator.enabled = false;
            transform.SetParent(null);
        }
        
        public void Interact() { }

        public void Pick(Transform character)
        {
            _rigidbody.isKinematic = true;
            
            if(_animator != null) _animator.enabled = true;
            
            transform.SetParent(character);
            transform.SetLocalPositionAndRotation(Vector3.zero, quaternion.Euler(Vector3.zero));
        }
    }
}