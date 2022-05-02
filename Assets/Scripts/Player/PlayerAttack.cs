using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim; //Holds player animator
    public LayerMask enemylayer; //Sets the layer that the player will be attacking
    public int attackLevel; //Holds the players attack level
    public AudioSource source; //Holds the players attack SFX
    public int damage; //Sets the attack damage of the player
    private float attackspeed = 1f; //Sets the players attack speed
    private float attackCD = 2f; //Holds the players attack cooldown

    void Start()
    {
        damage = 15 + (attackLevel * 5); //Calculates players damage depending on their damage level
    }

    // Update is called once per frame
    void Update()
    {
        attackCD += Time.deltaTime; //Increments the cooldown by the change in time since the last frame

        //if player presses left click
        if (Input.GetMouseButtonDown(0))
        {
            //if the players attack cooldown has reset over the players attack speed
            if(attackCD >= attackspeed)
            {
                attack();
            }
        }
    }

    void attack()
    {
        attackCD = 0; //reset attack cooldown

        anim.SetTrigger("Attack"); //Trigger attack animation
        source.Play(); //play the swing sound
    }

    // Function based on tutorial found at https://www.youtube.com/watch?v=ahadN8aGvXg 
    public void Knockback(GameObject enemy)
    {
        float timer = 0;

        while(timer < 0.5f)
        {
            timer += Time.deltaTime; //increments the timer to the change in time since last frame

            //takes in the difference in position between enemy and player to set direction and amount of knockback
            Vector2 direction = (enemy.transform.position - transform.position); 

            //normalises the values inside of the vector to whole numbers/tenths
            direction.Normalize(); 

            enemy.GetComponent<Rigidbody2D>().AddForce(direction * 15); //Applies a force to the rigidbody
        }
    }
}
