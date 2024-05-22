using Interfaces;
using UnityEngine;
using Zenject;

namespace Gameplay.Character
{
    public class CharacterRotation : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _minAngle;
        [SerializeField] private float _maxAngle;
        [Inject] private IInput _input;
                        
        private float _vertical;

        private void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            var direction = new Vector2(_input.RotationHorizontal, _input.RotationVertical);

            _vertical += direction.y * _rotateSpeed;
            _vertical = Mathf.Clamp(_vertical, _minAngle, _maxAngle);
            _camera.localRotation = Quaternion.Euler(-_vertical, 0, 0);

            transform.Rotate(Vector3.up, direction.x * _rotateSpeed);
        }
    }
}