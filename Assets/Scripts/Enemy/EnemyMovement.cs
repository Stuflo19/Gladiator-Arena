using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Based on tutorial found at: https://www.youtube.com/watch?v=dy8hkDmygRI 
public class EnemyMovement : MonoBehaviour
{
    public Animator anim; //Holds enemy animator
    public float speed = 0.5f; //Sets enemy movement speed
    private GameObject player; //Holds the player GameObject

    void Start()
    {
        //finds the player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        TrackPlayer();
    }

    private void TrackPlayer()
    {
        anim.SetBool("Moving", true); //sets moving to true for animator
        //sets animators X value to the players position - the enemys position
        anim.SetFloat("X", (player.transform.position.x - transform.position.x));
        //sets animators Y value to the players position - the enemys position
        anim.SetFloat("Y", (player.transform.position.y - transform.position.y));

        //Moves enemy towards player
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

}
