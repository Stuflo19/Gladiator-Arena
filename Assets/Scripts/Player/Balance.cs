using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{

    public int balance;
    public TMPro.TextMeshProUGUI balancedisplay;

    void Start()
    {
        UpdateDisplay();
    }

    //Function used to update balance display
    void UpdateDisplay()
    {
        balancedisplay.text = ": " + balance; //Update players balance display
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if player enters the coins collider
        if(other.gameObject.tag == "Coin")
        {
            balance += 1; //Increments player balance

            UpdateDisplay();

            Destroy(other.gameObject); //Destroys the placed coin
        }
    }

}
