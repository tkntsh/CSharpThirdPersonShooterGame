using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class animationControls : MonoBehaviour
{
    //variables for animator gameobjects
    private Animator animator;
    private float speed;

    //runs when application starts
    void Start()
    {
        //getting animations in animator component
        animator = GetComponent<Animator>();
    }

    //method runs per frame
    void Update()
    {
        //getting player movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //calc movement speed
        speed = new Vector3(horizontal, 0, vertical).magnitude;

        //check if the player is moving
        if(speed > 0.1f)
        {
            //triggering run animation
            animator.SetTrigger("triggerRun");
            animator.ResetTrigger("triggerIdle");
        }
        else
        {
            //triggering standing animation
            animator.ResetTrigger("triggerRun");
            animator.SetTrigger("triggerIdle");
        }

        //checking if the shoot key is pressed
        if(Input.GetKeyDown(KeyCode.K))
        {
            //triggering shoot animation
            animator.SetTrigger("triggerShoot");
        }
    }
}
