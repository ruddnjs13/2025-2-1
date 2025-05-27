using System;
using _01.Code.Tower.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Settings.InputSettings
{
    [CreateAssetMenu(fileName = "InputReaderSO", menuName = "SO/InputReader", order = 0)]
    public class InputReaderSO : ScriptableObject, Controls.IPlayerActions
    {
        public Action OnSelectEvent;
        
        private Controls _controls;

        private Vector2 _mousePosition;
        private Vector3 _worldPosition;
        
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
                OnSelectEvent?.Invoke();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            _mousePosition = context.ReadValue<Vector2>();
        }
        
        public ISelectable GetWorldPosition(out RaycastHit hit)
        {
            Camera mainCam = Camera.main;
            Ray camRay = mainCam.ScreenPointToRay(_mousePosition);
            bool isHit = Physics.Raycast(camRay, out hit, mainCam.farClipPlane);

            if (isHit)
            {
                _worldPosition = hit.point;
                if (hit.collider.TryGetComponent(out ISelectable selectable))
                {
                    return selectable;
                }
            }
            return default;
        }
    }
}