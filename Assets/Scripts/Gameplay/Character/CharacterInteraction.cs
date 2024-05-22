using Enums;
using Gameplay.Weapons;
using Interfaces;
using UI;
using UnityEngine;
using Zenject;

namespace Gameplay.Character
{
    public class CharacterInteraction : MonoBehaviour
    {
        [SerializeField] private CharacterWeapon _characterWeapon;
        [SerializeField] private float _checkDelay = 0.1f;
        [SerializeField] private float _checkDistance = 1f;
        [SerializeField] private LayerMask _layerMasks;
        [SerializeField] private Camera _camera;
        [Inject] private IInput _input;
        [Inject] private WindowsController _windowsController;
        
        private float _timer;
        private IInteractable _interactableObject;
        private DroppableWeapon _droppableWeapon;
        private InteractionWindow _interactionWindow;


        private void OnEnable()
        {
            _input.Interact += OnInteract;
        }

        private void OnDisable()
        {
            _input.Interact -= OnInteract;
        }

        private void Start()
        {
            _interactionWindow = _windowsController.GetWindowByType(WindowType.Interaction) as InteractionWindow;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _checkDelay)
            {
                _timer = 0;
                LaunchRaycast();
            }
        }

        private void OnInteract()
        {
            _interactableObject?.Interact();

            if (_droppableWeapon != null)
            {
                var weapon = _droppableWeapon.GetComponent<Weapons.Weapon>();
                _characterWeapon.SetWeapon(weapon);
                _droppableWeapon = null;
            }
        }

        private void LaunchRaycast()
        {
            var rayOrigin = _camera.transform.position;
            var rayDirection = _camera.transform.forward;
                
            if (Physics.Raycast(rayOrigin, rayDirection, out var hit, _checkDistance, _layerMasks))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent(out _interactableObject))
                    {
                        _interactionWindow?.Show(_interactableObject);
                    }

                    hit.collider.TryGetComponent(out _droppableWeapon);
                }
                else
                {
                    _interactableObject = null;
                    _droppableWeapon = null;
                    _interactionWindow.Hide();
                }
            }
            else
            {
                _interactableObject = null;
                _droppableWeapon = null;
                _interactionWindow.Hide();
            }
        }
    }
}