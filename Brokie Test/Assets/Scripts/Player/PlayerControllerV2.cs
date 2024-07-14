using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerV2 : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] float playerMovementSpeed;
    [SerializeField] float playerMaxMovementSpeed;
    private PlayerInputs inputActions;
    Rigidbody rb;
    Vector2 playerDirection;

    bool canJump = false;

    void Awake()
    {
        inputActions = new PlayerInputs();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        InputHandlerV2.Instance.OnJump += Jump;

        InputHandlerV2.Instance.OnMovePerformed += MovementPerformed;
        InputHandlerV2.Instance.OnMoveCancelled += MovementCancelled;
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(playerDirection.x, 0, playerDirection.y);
        if (canJump) rb.AddForce(movement * playerMovementSpeed);
        else rb.AddForce(movement * (playerMovementSpeed / 2.5f));

        if (rb.velocity.magnitude > playerMaxMovementSpeed) rb.velocity = rb.velocity.normalized * playerMaxMovementSpeed;
    }

    private void MovementStarted(InputAction.CallbackContext context)
    {
    }

    private void MovementPerformed(Vector2 context)
    {
        playerDirection = context;
    }

    private void MovementCancelled(Vector2 context)
    {
        playerDirection = context;
    }

    private void Jump()
    {
        if (canJump)
        {
            canJump = false;
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("HALLO :D");
            ContactPoint contact = collision.GetContact(0);
            Vector3 normal = contact.normal;
            if (Vector3.Dot(normal, Vector3.up) > 0.5f)
            {
                canJump = true;
            }
        }
    }
}
