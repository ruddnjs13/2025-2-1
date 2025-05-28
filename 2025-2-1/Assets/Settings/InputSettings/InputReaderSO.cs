using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Settings.InputSettings
{
    [CreateAssetMenu(fileName = "InputReaderSO", menuName = "SO/InputReader", order = 0)]
    public class InputReaderSO : ScriptableObject, Controls.IPlayerActions
    {
        public Action<bool> OnSelectEvent;
        
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

        public void OnSelect(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSelectEvent?.Invoke(true);
            else if(context.canceled)
                OnSelectEvent?.Invoke(false);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            MousePosition = context.ReadValue<Vector2>();
        }
        
        
    }
}