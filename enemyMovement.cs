using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    //variables for player speed, and shooting range
    public Transform player;
    public float moveSpeed = 3f;
    public float shootingRange = 5f; 
    
    //needed for calling a shooting method when enemy is close to player
    private enemyRangeAttack rangedAttack;

    //creating rigidbody reference
    private Rigidbody rb;

    //when application starts run method
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //store rigidbody component in rb
        rb = GetComponent<Rigidbody>();

        //getting enemyrangeattack script on start
        rangedAttack = GetComponent<enemyRangeAttack>();

        //if rangedattack is null, script wasnt added on enemy gameobject
        if(rangedAttack == null)
        {
            Debug.LogWarning("enemyRangeAttack script not found on this GameObject!");
        }
    }

    //method running per update frame
    void Update()
    {
        //checking if player gameobject is null
        if(player == null)
        {
            Debug.LogWarning("Player not assigned!");
            return;
        }

        //calc the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //if enemy isnt within shooting range
        if(distanceToPlayer > shootingRange)
        {
            //move towards the player if out of shooting range
            moveTowardsPlayer();
        }
        //if player is within shooting range enemy stops moving towards player
        else
        {
            //stop moving when within shooting range
            stopMoving();
            
            //call shooting logic from the EnemyRangedAttack script
            if(rangedAttack != null)
            {
                rangedAttack.attackPlayer(player);
            }
        }
    }

    //method to move towards player position (transform)
    void moveTowardsPlayer()
    {
        //calc the direction to player
        Vector3 direction = (player.position - transform.position).normalized;

        //move enemy using rigidbody
        Vector3 newPosition = rb.position + direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(newPosition);
    }

    //method to stop enemy movement towards player
    void stopMoving()
    {
        //stop enemy's movement
        rb.velocity = Vector3.zero;
    }
}
