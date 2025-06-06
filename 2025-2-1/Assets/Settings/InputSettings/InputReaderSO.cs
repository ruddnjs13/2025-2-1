using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Settings.InputSettings
{
    [CreateAssetMenu(fileName = "InputReaderSO", menuName = "SO/InputReader", order = 0)]
    public class InputReaderSO : ScriptableObject, Controls.IPlayerActions
    {
        public Action<bool> OnDragEvent;
        
        
        private Controls _controls;

        public Vector2 MousePosition { get; private set; }
        
        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
                _controls.Player.Enable();
            }
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public void OnDrag(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnDragEvent?.Invoke(true);
            else if(context.canceled)
                OnDragEvent?.Invoke(false);
            
                
        }

        public void OnDragPosition(InputAction.CallbackContext context)
        {
            MousePosition = context.ReadValue<Vector2>();
        }
    }
}