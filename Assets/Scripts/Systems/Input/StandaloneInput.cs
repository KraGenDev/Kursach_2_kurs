using System;
using Interfaces;
using UnityEngine;

namespace Systems.Input
{
    public sealed class StandaloneInput : MonoBehaviour, IInput, IWeaponInput
    {
        public float Horizontal => Cursor.visible ? 0 : UnityEngine.Input.GetAxis("Horizontal");
        public float Vertical => Cursor.visible ? 0 : UnityEngine.Input.GetAxis("Vertical");
        public float RotationHorizontal => Cursor.visible ? 0 : UnityEngine.Input.GetAxis("Mouse X");
        public float RotationVertical => Cursor.visible ? 0 : UnityEngine.Input.GetAxis("Mouse Y");
        public bool Run => UnityEngine.Input.GetKey(KeyCode.LeftShift);

        public event Action EndWeaponUsing;
        public event Action Interact;
        public event Action StartWeaponUsing;
        public event Action ReloadWeapon;
        public event Action DropWeapon;
        

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.K))
            {
                Cursor.visible = !Cursor.visible;
                Cursor.lockState = Cursor.visible 
                    ? CursorLockMode.None 
                    : CursorLockMode.Locked;
            }
            
            if (Cursor.visible)
                return;

            if (UnityEngine.Input.GetKeyDown(KeyCode.E))
            {
                Interact?.Invoke();
            }

            if (UnityEngine.Input.GetButtonDown("Fire1"))
            {
                StartWeaponUsing?.Invoke();
            }

            if (UnityEngine.Input.GetButtonUp("Fire1"))
            {
                EndWeaponUsing?.Invoke();
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
            {
                ReloadWeapon?.Invoke();
            }
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.G))
            {
                DropWeapon?.Invoke();
            }
        }
    }
}