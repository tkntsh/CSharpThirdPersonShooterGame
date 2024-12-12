using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnManager : MonoBehaviour
{
    //gameobjects capturing respawn location and time delay
    public Transform respawnPoint;
    public float respawnDelay = 3f;

    //calling healthManager class to update player health
    private healthManager healthManager;

    //run when application starts
    void Start()
    {
        //storing healthManager data
        healthManager = GetComponent<healthManager>();
    }

    //run when player needs to respawn
    public void respawn()
    {
        //timer starting and lasting for 3 secs
        StartCoroutine(respawnCoroutine());
    }

    //method handling time and player health
    private IEnumerator respawnCoroutine()
    {
        //waiting for respawn delay time to be over (using respawnDelay instatiated above: 3f)
        yield return new WaitForSeconds(respawnDelay);
        //instatiating start position to be the respawn point
        transform.position = respawnPoint.position;
        //reseting player health
        healthManager.takeDamage(-healthManager.maxHealth);
    }
}
