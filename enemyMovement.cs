using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    //variables for player speed, and shooting range
    private Transform player;
    public float speed = 0.5f;
    public float shootingRange = 10f;
    
    //needed for calling a shooting method when enemy is close to player
    private enemyRangeAttack rangedAttack;

    //creating rigidbody reference
    private Rigidbody rb;
    private Animator animator;

    //when application starts run method
    void Start()
    {
        //finding gameobject with 'Player' tag
        GameObject playerOG = GameObject.FindGameObjectWithTag("Player");

        //checking if player gameobject isnt null
        if(playerOG != null)
        {
            player = playerOG.transform;
        }
        else
        {
            Debug.LogWarning("Player GameObject not found! Make sure it has the 'Player' tag.");
        }

        //store rigidbody component in rb
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

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
                animator.SetTrigger("triggerShoot");
            }
        }

        //checking if player gameobject isnt null
        if(player != null)
        {
            moveTowardsPlayer();
            updateAnimation();
        }
    }

    //method to move towards player position (transform)
    private void moveTowardsPlayer()
    {
        //calc direction of enemy
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 movement = direction * speed * Time.deltaTime;

        //moving enemy towards player gameobject
        rb.MovePosition(rb.position + movement);

        //rotating the enemy to face the player
        if(direction.magnitude > 0.1f)
        {
            //locking on target to face/rotate enemy gameobjec to face player
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    //method to stop enemy movement towards player
    private void stopMoving()
    {
        //stop enemy's movement
        rb.velocity = Vector3.zero;
    }

    //method to handle updating animation parameters
    private void updateAnimation()
    {
        //calc distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //if enemy isnt close to player, run towards player
        if(distanceToPlayer > 0.1f)
        {
            animator.SetTrigger("triggerRun");
            animator.ResetTrigger("triggerIdle");
        }
        //otherwise stand still
        else
        {
            animator.ResetTrigger("triggerRun");
            animator.SetTrigger("triggerIdle");
        }
    }
}
