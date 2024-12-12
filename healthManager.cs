using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthManager : MonoBehaviour
{
    //using slider in this case, if you dont know how to use a slider,
    //you can use a text component to display players health percentage on an overlay canvas
    public Slider healthBar;
    //setting max health to 100
    public float maxHealth = 100f;
    public Transform respawnPoint;
    public float respawnDelay = 3f;
    private float currentHealth;

    //run when application starts
    void Start()
    {
        //setting the current health when application is first run to be max
        currentHealth = maxHealth;
        //setting slider to maxhealth
        healthBar.maxValue = maxHealth;
        //setting health bar value to be the current health instantiated above
        healthBar.value = currentHealth;
    }

    //method to handle taking damage and reducing health
    public void takeDamage(float damage)
    {
        //player health is updated by reducing health according to bullet damage
        currentHealth -= damage;
        //updating health bar according to new current health value
        healthBar.value = currentHealth;

        //checking if current health is above 0 to keep player alive
        if(currentHealth <= 0)
        {
            //if current health is = 0, run die method
            die();
        }
    }

    //method to handle player gameobject death
    void die()
    {
        Debug.Log("Player has died!");
        //deactivating player
        gameObject.SetActive(false);
        //calling respawn method after delay
        Invoke(nameof(respawn), respawnDelay);
    }

    void respawn()
    {
        //resetting player position and health back to 100%
        transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
        //setting gameobject back active
        gameObject.SetActive(true);
    }
}
