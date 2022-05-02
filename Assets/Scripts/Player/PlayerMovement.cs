using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D player; //Holds the players rigidbody
    public Animator playAnimation; //Holds the players animator
    public int speedLevel; //Holds players speed upgrade level

    private float speed = 2f; //Sets the speed of the player
    private Vector2 playerInput; //Takes in players Horizonal and Veritcal inputs
    private RigidbodyConstraints2D RBconstraints;

    void Start()
    {
        //Saves the original RigidBody constriants
        RBconstraints = gameObject.GetComponent<Rigidbody2D>().constraints;

        if(speedLevel > 1)
        {
            speed = speed + (speedLevel/5); //Scales players speed with their level
        }
    }

    // Update is called once per frame
    //Based on tutorial found at: https://www.youtube.com/watch?v=whzomFgjT50&ab_channel=Brackeys 
    void Update()
    {
        playerInput.x = Input.GetAxisRaw("Horizontal"); //Checks for horizontal input
        playerInput.y = Input.GetAxisRaw("Vertical"); //Checks for vertical input
    
        //if the player tries to move, unfreeze the rigidbody so that the player can move
        if(playerInput.x > 0 || playerInput.x < 0){gameObject.GetComponent<Rigidbody2D>().constraints = RBconstraints;};
        if(playerInput.y > 0 || playerInput.y < 0){gameObject.GetComponent<Rigidbody2D>().constraints = RBconstraints;};

        //Sets the player animation values to the current input and speed
        playAnimation.SetFloat("Horizontal", playerInput.x);
        playAnimation.SetFloat("Vertical", playerInput.y);
        playAnimation.SetFloat("Speed", playerInput.sqrMagnitude);
    }

    void FixedUpdate()
    {
        //Moves the player using their input
        player.MovePosition(player.position + playerInput * speed * Time.fixedDeltaTime);
    }

    //Function used to stop enemies pushing the player
    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            //Freezes the players position if an enemy is trying to push them
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

}
