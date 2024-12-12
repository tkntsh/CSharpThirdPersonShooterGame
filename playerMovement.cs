using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    //variables for movement speed and rigidbody
    public float speed = 10f;
    private Rigidbody rb;

    //movement keyboard inputs
    private Vector2 movementInput;

    //shooting script reference
    private playerShooting shootingScript;

    //when game starts, the Rigidbody of the player object is collected and stored
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        //getting the player shooting script
        shootingScript = GetComponent<playerShooting>();

        //checking if shootingScript is null
        if(shootingScript == null)
        {
            Debug.LogWarning("playerShooting script not found on this GameObject!");
        }
    }

    private void Update()
    {
        //WASD input and move the player
        movePlayer();
        rotatePlayer();

        //when 'K' is pressed, call the shoot method
        if(Input.GetKeyDown(KeyCode.K))
        {
            shoot();
        }
    }

    //method to detect what type of movement should happen using input from keyboard
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    //method to handle moving player
    private void movePlayer()
    {
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y) * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    //method to handle shooting at enemy
    private void shoot()
    {
        if(shootingScript != null)
        {
            shootingScript.shoot();
        }
        else
        {
            Debug.LogWarning("playerShooting script is null!");
        }
    }

    //method to handle player rotation to face movement direction
    private void rotatePlayer()
    {
        //checking for input
        if(movementInput.sqrMagnitude > 0.01f)
        {
            //getting and setting direction and rotation for gameobject
            Vector3 direction = new Vector3(movementInput.x, 0, movementInput.y);
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            //rotation smoothness
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}
