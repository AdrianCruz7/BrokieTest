using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    PlayerInputs playerInputs;
    InputAction moveAction;
    InputAction jumpAction;

    public Vector2 moveValue { get; private set; }
    public bool jumpValue { get; private set; }
    public static InputHandler Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        InstantiatePlayerInputActionsAndAssignThem();
        ReadInputActions();
    }

    void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    void ReadInputActions()
    {
        moveAction.performed += context => moveValue = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveValue = Vector2.zero;

        jumpAction.performed += context => jumpValue = true;
        jumpAction.canceled += context => jumpValue = false;
    }

    void InstantiatePlayerInputActionsAndAssignThem()
    {
        playerInputs = new PlayerInputs();
        moveAction = playerInputs.Movement.Move;
        jumpAction = playerInputs.Movement.Jump;
    }
}
