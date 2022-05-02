using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    //takes in the shops sliders
    public Slider attackSlider;
    public Slider healthSlider; 
    public Slider speedSlider; 

    public TMPro.TextMeshProUGUI balanceDisplay; //Takes in the text display for the players balance

    //Takes in the text display for the cost of upgrades
    public TMPro.TextMeshProUGUI attackCostDisplay; 
    public TMPro.TextMeshProUGUI healthCostDisplay; 
    public TMPro.TextMeshProUGUI speedCostDisplay; 

    private PlayerData data; //Takes in the players data

    //Holds the cost of the upgrades
    private int attackCost; 
    private int healthCost;
    private int speedCost;

    //holds the players data variables
    private int playerBalance;
    private int attackLevel;
    private int healthLevel;
    private int speedLevel;

    // Start is called before the first frame update
    void Awake()
    {   
        //sets the players variables
        data = SaveSystem.LoadPlayer();
        playerBalance = data.balance;
        attackLevel = data.attackLevel;
        healthLevel = data.healthLevel;
        speedLevel = data.speedLevel;

        //sets up all the shops and displays players balance
        UpdateBalanceDisplay();

        //Calculates starting costs for all the shops
        attackCost = CalculateCost(attackLevel);
        healthCost = CalculateCost(healthLevel);
        speedCost = CalculateCost(speedLevel);

        UpdateShop("attack", attackCost, attackLevel, attackSlider, attackCostDisplay);
        UpdateShop("health", healthCost, healthLevel, healthSlider, healthCostDisplay);
        UpdateShop("speed", speedCost, speedLevel, speedSlider, speedCostDisplay);
    }   

    //function to update the attack shop
    void UpdateShop(string type, int cost, int level, Slider slider, TMPro.TextMeshProUGUI costDisplay)
    {
        //sets slider value to players level
        slider.value = level;

        //displays cost or if player is already max level
        if(cost == 999)
        {
            //if statements used to turn off the coin on button display when max level
            if(type == "attack")
            {
                GameObject.FindGameObjectWithTag("AttackCostCoin").SetActive(false);
            }
            else if(type == "health")
            {
                GameObject.FindGameObjectWithTag("HealthCostCoin").SetActive(false);
            }
            else if(type == "speed")
            {
                GameObject.FindGameObjectWithTag("SpeedCostCoin").SetActive(false);
            }
            costDisplay.text = "Max Level!";
        }
        else
        {
            costDisplay.text = "" + cost;
        }
    }

    //function to upgrade players attack stat
    public void UpgradeAttack()
    {
        //if the players balance is greater than or equal to the cost and they are not max level
        if(playerBalance >= attackCost && attackCost != 999)
        {
            //increment the players attack level
            attackLevel += 1;

            //decrements the players balance by the value they just spent on the upgrade
            playerBalance -= attackCost;

            //updates the shop and the balance display of the user
            attackCost = CalculateCost(attackLevel);
            UpdateShop("attack", attackCost, attackLevel, attackSlider, attackCostDisplay);
            UpdateBalanceDisplay();
        }
    }

    //function to upgrade the players health stat
    public void UpgradeHealth()
    {
        //if the players balance is greater than or equal to the cost and they are not max level
        if(playerBalance >= healthCost && healthCost != 999)
        {
            //increment the players attack level
            healthLevel += 1;

            //decrements the players balance by the value they just spent on the upgrade
            playerBalance -= healthCost;

            //updates the shop and the balance display of the user
            healthCost = CalculateCost(healthLevel);
            UpdateShop("health", healthCost, healthLevel, healthSlider, healthCostDisplay);
            UpdateBalanceDisplay();
        }
    }

    //function to upgrade the players speed stat
    public void UpgradeSpeed()
    {
        //if the players balance is greater than or equal to the cost and they are not max level
        if(playerBalance >= speedCost && speedCost != 999)
        {
            //increment the players attack level
            speedLevel += 1;

            //decrements the players balance by the value they just spent on the upgrade
            playerBalance -= speedCost;

            //updates the shop and the balance display of the user
            speedCost = CalculateCost(speedLevel);
            UpdateShop("speed", speedCost, speedLevel, speedSlider, speedCostDisplay);
            UpdateBalanceDisplay();
        }
    }

    //function to return to the main menu
    public void ReturnToGame()
    {
        //saves the player
        SaveSystem.SavePlayer(playerBalance, attackLevel, healthLevel, speedLevel, data.playerLevel);

        //loads main menu
        SceneManager.LoadScene(0);
    }

    //function to update the display of the players balance
    void UpdateBalanceDisplay()
    {
        balanceDisplay.text = "" + playerBalance;
    }

    //function used to calculate the cost of the upgrade
    int CalculateCost(int level)
    {
        int cost = 0;

        //switch function to set the cost of the upgrade depending on the level
        switch(level)
        {
            case 1:
                cost = 20;
                break;
            case 2:
                cost = 50;
                break;
            case 3:
                cost = 100;
                break;
            case 4:
                cost = 200;
                break;
            case 5:
                cost = 500;
                break;
            default:
                cost = 999;
                break;
        }

        return cost; //returns the cost of the upgrade
    }



}
