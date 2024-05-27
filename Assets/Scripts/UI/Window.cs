using System;
using DG.Tweening;
using Enums;
using UnityEngine;

namespace UI
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _window;
        [SerializeField] private float _animationDuration = 0.25f;
        [SerializeField] private bool _blockRaycastIfOpen = true;
        [SerializeField] private bool _showOnStart = false;
        
        private bool _isShown;
        
        public virtual WindowType Type => WindowType.None;

        private void Start()
        {
            if (_showOnStart)
            {
                Show();
            }
        }

        public virtual void Show()
        {
            if (_isShown) 
                return;
            
            if (_window != null)
            {
                _window.alpha = 0;
                _window.blocksRaycasts = _blockRaycastIfOpen;
                _window.DOFade(1, _animationDuration);
                _isShown = true;
            }
        }

        public virtual void Hide()
        {
            if (!_isShown) 
                return;
            
            if (_window != null)
            {
                _window.blocksRaycasts = false;
                _window.DOFade(0, _animationDuration);
                _isShown = false;
            }
        }
    }
}