using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameCore
{
    public sealed class GameplayInput : MonoBehaviour
    {
        public event Action<Vector2> OnMoveDirection;

        public event Action<Vector3> OnMovePosition;

        private Camera _camera;

        //a field of input and isPlaying listener to switch input action maps

        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _playerInput.onActionTriggered += OnClickPosition;
            _playerInput.onActionTriggered += OnMoveUp;
            _playerInput.onActionTriggered += OnMoveDown;
            _playerInput.onActionTriggered += OnMoveLeft;
            _playerInput.onActionTriggered += OnMoveRight;
        }

        private void OnDisable()
        {
            _playerInput.onActionTriggered -= OnClickPosition;
            _playerInput.onActionTriggered -= OnMoveUp;
            _playerInput.onActionTriggered -= OnMoveDown;
            _playerInput.onActionTriggered -= OnMoveLeft;
            _playerInput.onActionTriggered -= OnMoveRight;
        }


        private void OnClickPosition(InputAction.CallbackContext context)
        {
            if (context.action.name == "PositionMove" && context.performed)
            {
                var clickPositionScreen = context.ReadValue<Vector2>();

                var clickPos = _camera.ScreenToWorldPoint(clickPositionScreen);

                OnMovePosition?.Invoke(clickPos);
            }
        }

        private void OnMoveUp(InputAction.CallbackContext context)
        {
            if (context.action.name == "MoveUp" && context.performed)
            {
                OnMoveDirection?.Invoke(Vector2.up);
            }
        }

        private void OnMoveDown(InputAction.CallbackContext context)
        {
            if (context.action.name == "MoveDown" && context.performed)
            {
                OnMoveDirection?.Invoke(Vector2.down);
            }
        }

        private void OnMoveLeft(InputAction.CallbackContext context)
        {
            if (context.action.name == "MoveLeft" && context.performed)
            {
                OnMoveDirection?.Invoke(Vector2.left);
            }
        }

        private void OnMoveRight(InputAction.CallbackContext context)
        {
            if (context.action.name == "MoveRight" && context.performed)
            {
                OnMoveDirection?.Invoke(Vector2.right);
            }
        }
    }
}