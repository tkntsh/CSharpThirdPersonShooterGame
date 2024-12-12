using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //variables on how a bullet works
    public float damage = 10f;
    public float lifeTime = 3f;

    //run method when application starts
    void Start()
    {
        //destroying bullet gameobject after 3 seconds if no collision is detected
        Destroy(gameObject, lifeTime);
    }

    //when a bullet collides with another box collider the following happens
    void OnCollisionEnter(Collision collision)
    {
        //storing player health to manipulate it according to a collision
        healthManager targetHealth = collision.gameObject.GetComponent<healthManager>();
        
        //if the health isnt null, take damage from the player
        if(targetHealth != null)
        {
            targetHealth.takeDamage(damage);
        }
        //destroying bullet gameobject after collision with player box collider
        Destroy(gameObject);
    }
}
