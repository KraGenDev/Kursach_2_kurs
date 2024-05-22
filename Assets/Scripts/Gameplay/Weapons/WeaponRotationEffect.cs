using Interfaces;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay.Weapons
{
    public class WeaponRotationEffect : MonoBehaviour
    {
        [SerializeField] private Transform _weapon;
        [SerializeField] private Vector2 _angles;
        [SerializeField] private Vector2 _idleMovementForce;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _idleMovementUpdateTime = 1f;

        [Inject] private IInput _input;
        private Vector2 _axis;
        private Vector3 _defaultWeaponPosition;
        private Vector2 _randomOffset;
        private float _timer;

        private void Start()
        {
            _defaultWeaponPosition = _weapon.transform.localPosition;
        }

        private void Update()
        {
            _axis.x = _input.Horizontal;
            _axis.y = _input.Vertical;
            
            Rotate();
            Move();
            UpdateIdleMovementOffset();
        }

        private void UpdateIdleMovementOffset()
        {
            _timer += Time.deltaTime;

            if (_timer >= _idleMovementUpdateTime)
            {
                _timer = 0;
                _randomOffset.x = Random.Range(-_idleMovementForce.x, _idleMovementForce.x);
                _randomOffset.y = Random.Range(-_idleMovementForce.y, _idleMovementForce.y);
            }
        }

        private void Rotate()
        {
            var current = _weapon.transform.localRotation;
            var target = Quaternion.Euler(new Vector3(_angles.x * _axis.y, 180 + (_angles.y * _axis.x), 0));
            _weapon.transform.localRotation = Quaternion.Lerp(current, target,_rotationSpeed * Time.deltaTime);
        }

        private void Move()
        {
            const float divider = 10f;
            var current = _weapon.transform.localPosition;
            var targetPosition = new Vector3
            {
                x = _axis.x / divider + _randomOffset.x, 
                y = -Mathf.Abs(_axis.y / divider) + _randomOffset.y, 
                z = -_axis.y / divider
            };
            var target = _defaultWeaponPosition + targetPosition;
            _weapon.transform.localPosition = Vector3.Lerp(current, target,_movementSpeed * Time.deltaTime);
        }
    }
}