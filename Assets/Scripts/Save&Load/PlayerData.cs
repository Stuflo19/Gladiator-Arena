using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Based on tutorial found at https://www.youtube.com/watch?v=XOjd_qU2Ido 
[System.Serializable]
public class PlayerData
{
    public int playerLevel; //Stores the players level
    public int balance; //Stores the players balance
    public int attackLevel; //Stores the players attack upgrade level
    public int speedLevel; //Stores the players speed upgrade level
    public int healthLevel; //Stores the players health upgrade level

    //Constructor used to initialise all the data to be loaded from a save file
    public PlayerData(int bal, int attack, int health, int speed, int level)
    {
        balance = bal;
        attackLevel = attack;
        speedLevel = speed;
        healthLevel = health;
        playerLevel = level;
    }
}
