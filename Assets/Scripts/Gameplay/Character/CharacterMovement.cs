using Interfaces;
using UnityEngine;
using Zenject;

namespace Gameplay.Character
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float _walkSpeed = 100f;
        [SerializeField] private float _runSpeed = 500f;
        [Inject] private IInput _input;
        
        private bool _hasInput;
        private CharacterController _characterController;
        

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _hasInput = _input != null;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (!_hasInput)
                return;
            
            var inputDirection = new Vector3(_input.Horizontal,0,_input.Vertical);
            var direction = Vector3.zero;

            direction += inputDirection * (_input.Run 
                ? _runSpeed 
                : _walkSpeed);
            
            direction = transform.TransformDirection(direction);

            _characterController.SimpleMove(direction * Time.fixedDeltaTime);
        }
    }
}