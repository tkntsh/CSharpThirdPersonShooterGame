using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShooting : MonoBehaviour
{
    //variables needed to handle how bullets are shot at enemy
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletForce = 25f;
    public float shootingRange = 10f;
    public float shootCooldown = 0.5f;

    //tracking when player can shoot next
    private float nextShootTime = 0f;

    //method to run every frame update
    void Update()
    {
        //checking if 'k' key was pressed on keyboard to shoot at enemy
        if(Input.GetKeyDown(KeyCode.K))
        {
            shoot();
        }
    }

    public void shoot()
    {
        //checking first if player is allowed to shoot again
        if(Time.time >= nextShootTime)
        {
            //finding the nearest enemy within range
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, shootingRange);
            Transform targetEnemy = null;
            float closestDistance = Mathf.Infinity;

            //runs for every gameobject that has a collider
            foreach(Collider collider in hitColliders)
            {
                //checking if collider gameobject has 'enemy' tag
                if(collider.CompareTag("Enemy"))
                {
                    //locking onto nearest 'enemy' gameobject target
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    if(distance < closestDistance)
                    {
                        closestDistance = distance;
                        targetEnemy = collider.transform;
                    }
                }
            }

            //checking if enemy target isnt null
            if(targetEnemy != null)
            {
                //instantiate/create the bullet
                GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

                //calc direction to the enemy
                Vector3 directionToEnemy = (targetEnemy.position - shootPoint.position).normalized;

                //apply force to the bullet, using rigidbody
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                //checking if bullet isnt null
                if(bulletRb != null)
                {
                    bulletRb.AddForce(directionToEnemy * bulletForce, ForceMode.Impulse);
                }

                //attach a script to handle enemy destruction on collision
                BulletLogic bulletLogic = bullet.AddComponent<BulletLogic>();
                bulletLogic.targetEnemy = targetEnemy.gameObject;

                Debug.Log($"Shot fired at enemy: {targetEnemy.name}");

                //updating time for the next shot
                nextShootTime = Time.time + shootCooldown;
            }
            else
            {
                Debug.Log("No enemies within shooting range!");
            }
        }
    }

    //method to draw using gizmos
    void onDrawGizmosSelected()
    {
        //draw the shooting range in the scene view for debugging
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}

//class to handle what happens when bullet enters collision with enemy
public class BulletLogic : MonoBehaviour
{
    //reference to target enemy
    public GameObject targetEnemy;

    //method to run on collision
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == targetEnemy)
        {
            //destroying enemy and the bullet gameobjects
            Destroy(targetEnemy);
            Destroy(gameObject);
            Debug.Log($"Enemy {targetEnemy.name} destroyed!");
        }
    }
}
