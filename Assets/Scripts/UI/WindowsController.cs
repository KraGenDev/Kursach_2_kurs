using System;
using System.Linq;
using Enums;
using UnityEngine;

namespace UI
{
    public sealed class WindowsController : MonoBehaviour
    {
        [SerializeField] private Window[] _windows;

        private Window _currentWindow;
        private Window _previousWindow;
        

        public void ShowWindow(WindowType windowType)
        {
            if (_windows == null || _windows.Length == 0)
                return;

            var window = _windows.FirstOrDefault(item => item.Type == windowType);

            if (window != null)
            {
                window.Show();
                
                _previousWindow = _currentWindow;
                _currentWindow = window;
            }
            else
            {
                Debug.Log($"Can't open window with type {windowType}");
            }
        }

        public Window GetWindowByType(WindowType type)
        {
           return _windows.FirstOrDefault(item => item.Type == type) 
                  ?? throw new Exception($"No required window with type {type}");
        }

        public void RestorePreviousWindow()
        {
            if (_previousWindow == null) 
                return;
            
            _previousWindow.Show();
            var currentWindow = _currentWindow;
            _currentWindow = _previousWindow;
            _previousWindow = currentWindow;
        }
    }
}