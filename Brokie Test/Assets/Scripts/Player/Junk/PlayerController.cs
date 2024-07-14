using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Health playerHealth;
    [SerializeField] float playerMovementSpeed;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Vector3 boxSize;
    [SerializeField] Vector3 boxOffset;

    Rigidbody rb;
    InputHandler inputHandler;
    bool playerJump;
    
    void Start()
    {
        inputHandler = InputHandler.Instance;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckGrounded();
        PlayerMovement();
        PlayerJump();
    }

    void PlayerMovement()
    {
        Vector2 playerDirection = inputHandler.moveValue;
        //transform.position += new Vector3(playerDirection.x, 0, playerDirection.y) * playerMovementSpeed * Time.deltaTime;    
        rb.velocity += new Vector3(playerDirection.x, 0, playerDirection.y) * playerMovementSpeed * Time.deltaTime;
    }

    void PlayerJump()
    {
        if(inputHandler.jumpValue && playerJump)
        {
            playerJump = false;
            rb.AddForce(transform.up * 2, ForceMode.Impulse);

            Debug.Log("Test Jump");
        }
    }

    bool CheckGrounded()
    {
        var offset = transform.position + boxOffset;
        bool grounded = Physics.CheckBox(transform.position, boxSize / 2, transform.rotation, layerMask);
        if(grounded) playerJump = true;
        
        //Debug.Log("Ground check at position: " + transform.position + " with result: " + test);
        return grounded;
    }

    void OnDrawGizmos()
    {
        // Visualize the ground check box
        Gizmos.color = Color.red;
        Vector3 checkPosition = transform.position + boxOffset;
        Gizmos.DrawWireCube(checkPosition, boxSize);
    }

}

