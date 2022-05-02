using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Animator anim; //Holds enemy animator
    public float range = 0.15f; //Sets enemy attack range
    public int damage; //Sets enemy attack damage
    public LayerMask playerlayer; //sets the layer the enemy attacks
    private float attackcooldown = 2f; //sets enemy attack cooldown
    private float attackspeed = 2f; //sets the enemy's attack speed

    void Update()
    {
        attackcooldown += Time.deltaTime; //Increments the enemy's cooldown time by the time since last frame
    }

    //Function based on tutorial found at: https://www.youtube.com/watch?v=VOdYtqV_meo&ab_channel=MuddyWolf 
    private void OnCollisionStay2D(Collision2D other)
    {
        //if the collider is the Players
        if(other.gameObject.tag == "Player")
        {
            //if the attack speed is greater than the time since last attack
            if(attackspeed <= attackcooldown)
            {
                StartCoroutine(attack());
                attackcooldown = 0f; //reset attack cooldown
            }
            
        }
    }

    //Function based on turorial found at: https://www.youtube.com/watch?v=sPiVz1k-fEs 
    IEnumerator attack()
    {
        //play attack animation
        anim.SetTrigger("Attack");

        //Reads in if players collider is in the enemy's attack range
        Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, range, playerlayer);
    
        //Get the player from the array
        foreach (Collider2D p in player)
        {
            if(p.tag == "Player")
            {
                //Flash player red for half a second
                p.GetComponent<Renderer>().material.color = Color.red;
                yield return new WaitForSeconds(0.5f);
                p.GetComponent<Renderer>().material.color = Color.white;

                //Get players health script and take damage
                p.GetComponent<PlayerHealth>().takeDamage(damage);
            }
        }
    }
}
