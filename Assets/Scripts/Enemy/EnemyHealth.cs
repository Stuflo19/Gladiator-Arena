using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 100; //Sets players health
    private GameObject wavecontroller; //Holds the wavecontroller
    public GameObject coin; //holds coin prefab
    public Animator anim; //holds the enemy animator
    private GameObject player; //holds player to access their attack script
    private float cooldown; //holds how long before the enemy can be hit again

    void Start()
    {
        //Finds the wavecontroller
        wavecontroller = GameObject.FindGameObjectWithTag("WaveController");

        //finds the player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //increments the cooldown if it is greater than 0
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    //Function to attack the enemy
    private IEnumerator TakeDamage(int damage)
    {
        Renderer render = GetComponent<Renderer>(); //gets the components renderer

        health -= damage; //Reduces health by damage taken

        //change their colour for half a second
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        gameObject.GetComponent<Renderer>().material.color = Color.white;

        //if the health is below or is 0
        if(health <= 0)
        {
            Destroy(gameObject.GetComponent<CapsuleCollider2D>()); //Destroy collider
            Destroy(gameObject.GetComponent<EnemyMovement>()); //Destroy movement script

            StartCoroutine(die()); //Call death function
        }
    }

    //Function to kill enemy
    private IEnumerator die()
    {
        StartCoroutine(wavecontroller.GetComponent<SpawnWave>().EnemyKilled()); //Calls to the wave that an enemy has died

        anim.SetTrigger("Die"); //Trigger death animation

        int chance = Random.Range(0,2); //Decide 50% chance for coin to drop

        //if chance rolled 1
        if(chance == 1)
        {
            //create coin object on enemy position
            GameObject coincopy = Instantiate(coin, transform.position, Quaternion.identity);
        }

        //wait for animation to play
        yield return new WaitForSeconds(0.5f);

        //Destroy enemy gameobject
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PlayerSword" && cooldown <= 0)
        {
            StartCoroutine(TakeDamage(player.GetComponent<PlayerAttack>().damage)); //Damage the enemy with the players damage

            player.GetComponent<PlayerAttack>().Knockback(gameObject); //Knockback the enemy

            cooldown = 1.0f; //set cooldown till enemy can be hit again
        }
    }

}
