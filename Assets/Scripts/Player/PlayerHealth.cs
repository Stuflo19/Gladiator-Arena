using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthbar; //Holds the players healthbar
    public Gradient grad; //Holds gradient for healthbar
    public Image fillBar; //Holds the bar the displays the health
    private int maxHealth; //Holds the players maximum HP
    private int health; //Holds players health
    private int heal = 20; //Holds heal amount
    public int healthLevel; //Holds players health upgrade level
    public Animator anim; //Holds the players animator to trigger their death
    public GameObject deathMenu; //Holds the gameobject to show the deathmenu to the player
    public AudioSource potion; //Holds the potion sound to play when player heal

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100 + (healthLevel * 20); //Calculates players max health based on upgrades
        health = maxHealth; //sets the players health
        healthbar.maxValue = maxHealth;
        healthbar.value = maxHealth;
        fillBar.color = grad.Evaluate(1f); //sets the players health to the gradient at full
    }

    //Function to deal damage to the player
    public void takeDamage(int damage)
    {
        Renderer render = GetComponent<Renderer>(); //Get the players renderer

        health -= damage; //Reduces players health by damage taken

        healthbar.value = health; //Update healthbar

        //Update healthbar gradient colour
        fillBar.color = grad.Evaluate(healthbar.normalizedValue); 

        //if health less than 0
        if(health <= 0)
        {
            StartCoroutine(die()); //call players death
        }
    }

    //Function to detect if player enters a potion collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        //If the collider belongs to a potion
        if(other.gameObject.tag == "Potion")
        {
            potion.Play();

            health += heal; //heal player by heal amount

            //if players health is now greater than 100
            if(health > maxHealth)
            {
                health = maxHealth; //reset health to be the players max
            }

            healthbar.value = health; //Update healthbar

            //Update healthbar gradient colour
            fillBar.color = grad.Evaluate(healthbar.normalizedValue);

            Destroy(other.gameObject); //Destroy the potion
        }
    }

    //Function to kill the player
    IEnumerator die()
    {
        //turn off player scripts
        gameObject.GetComponent<PlayerAttack>().enabled = false;
        gameObject.GetComponent<PlayerMovement>().enabled = false;

        //play player death
        anim.SetTrigger("Die");

        //wait for death to play
        yield return new WaitForSeconds(0.5f);

        //show player the death menu
        deathMenu.SetActive(true);

        //turn off timescale so enemies still alive stop moving in the background
        Time.timeScale = 0f;
    }

}
