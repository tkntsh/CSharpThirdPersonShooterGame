using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //getting player movement speed
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //storing movement speed
        speed = new Vector3(horizontal, 0, vertical).magnitude;

        //updating animator parameters according to current player speed
        animator.SetFloat("Speed", speed);
    }
}
