using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandlerV2 : MonoBehaviour
{
    public static InputHandlerV2 Instance { get; private set; }
    private PlayerInputs inputActions;

    public event Action OnPause;
    public event Action<Vector2> OnMovePerformed;
    public event Action<Vector2> OnMoveCancelled;
    public event Action OnJump;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        inputActions = new PlayerInputs();

        inputActions.UI.Pause.performed += SignalPause;
        inputActions.Movement.Move.performed += SignalMovePerformed;
        inputActions.Movement.Move.canceled += SignalMoveCancelled;
        inputActions.Movement.Jump.performed += SignalJump;
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void SignalPause(InputAction.CallbackContext context)
    {
        OnPause?.Invoke();
    }

    void SignalMovePerformed(InputAction.CallbackContext context)
    {
        OnMovePerformed?.Invoke(context.ReadValue<Vector2>());
    }

    void SignalMoveCancelled(InputAction.CallbackContext context)
    {
        OnMoveCancelled?.Invoke(Vector2.zero);
    }

    void SignalJump(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }
}
