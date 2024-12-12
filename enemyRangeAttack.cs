using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRangeAttack : MonoBehaviour
{
    //variables for bullet prefab use
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public float shootCooldown = 1f;

    private float nextShootTime = 0f;

    //method to attack player and handle where bullet should move to
    public void attackPlayer(Transform player)
    {
        //checking if enough time has passed since the last bullet was shot
        if(Time.time >= nextShootTime)
        {
            //checking if bullet isnt null and shootpoint isnt null
            if(bulletPrefab != null && shootPoint != null)
            {
                //instantiate/create the bullet at the shoot point
                GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

                //add force, using rigidbody, to the bullet to make it move toward the player
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                if(bulletRb != null)
                {
                    //setting direction of bullet, where it needs to go to hit player
                    Vector3 directionToPlayer = (player.position - shootPoint.position).normalized;
                    //force: optional
                    bulletRb.AddForce(directionToPlayer * 20f, ForceMode.Impulse);
                }

                //updating the time for the next shot
                nextShootTime = Time.time + shootCooldown;
            }
            //error handling bullet not being found
            else
            {
                Debug.LogWarning("Bullet prefab or shoot point not assigned!");
            }
        }
    }
}
