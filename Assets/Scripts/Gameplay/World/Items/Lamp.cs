using DG.Tweening;
using Enums;
using Interfaces;
using UnityEngine;

namespace Gameplay.World.Items
{
    public class Lamp : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public Sprite Image { get; set; }
        [field: SerializeField] public InteractionType Action { get; set; }
        
        [SerializeField] private Light _light;
        [SerializeField] private GameObject _lightModel;
        [SerializeField] private float _activeIntensity;
        [SerializeField] private float _intensityChangingDuration = 1;

        private bool _active = false;
        
        public void Interact()
        {
            var targetIntensity = _active 
                ? 0 
                : _activeIntensity;
            
            _active = !_active;
            _lightModel.SetActive(true);
            _light.DOIntensity(targetIntensity,_intensityChangingDuration).OnComplete((() =>
            {
                if (!_active)
                {
                    _lightModel.SetActive(false);
                }
            }));
        }
    }
}