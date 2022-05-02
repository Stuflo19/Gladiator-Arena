using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script used to load all of the players information from their save
public class LoadPlayer : MonoBehaviour
{
    //Calls as soon as the object appears in its scene
    void Awake()
    {
        Balance bal = gameObject.GetComponent<Balance>(); // Gets the balance script attached to the player
        PlayerAttack attack = gameObject.GetComponent<PlayerAttack>(); // Gets the attack script attached to the player
        PlayerHealth health = gameObject.GetComponent<PlayerHealth>(); // Gets the health script attached to the player
        PlayerMovement speed = gameObject.GetComponent<PlayerMovement>(); // Gets the movement script attached to the player
        SpawnWave playerLevel = GameObject.FindGameObjectWithTag("WaveController").GetComponent<SpawnWave>(); //Gets the wavecontroller game object to set the players map/difficulty level

        //Loads in the player data fom their save file
        PlayerData data = SaveSystem.LoadPlayer();

        //if there is no save file set defualt values
        if(data == null)
        {
            bal.balance = 0;
            attack.attackLevel = 1;
            health.healthLevel = 1;
            speed.speedLevel = 1;
            playerLevel.level = 1;
        }
        //else if there is save files set the values to the ones from the players save
        else
        {
            bal.balance = data.balance;
            attack.attackLevel = data.attackLevel;    
            health.healthLevel = data.healthLevel;
            speed.speedLevel = data.speedLevel;
            playerLevel.level = data.playerLevel;
        }
    }
}
