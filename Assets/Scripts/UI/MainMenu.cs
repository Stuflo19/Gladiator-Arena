using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private PlayerData data;

    void Awake()
    {
        data = SaveSystem.LoadPlayer();
    }

    //Function to play game when button is pressed
    public void Play()
    {
        //Moves to next scene
        if(data != null)
        {
            SceneManager.LoadScene(data.playerLevel);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void Upgrades()
    {
        if(data != null)
        {
            SceneManager.LoadScene(6);
        }
        else
        {
            
        }
    }

    //Function to view how to play when button pressed
    public void HowToPlay()
    {
        SceneManager.LoadScene(7);
    }

    //Function to view how to play when button pressed
    public void Controls()
    {
        SceneManager.LoadScene(8);
    }

    //Function to view credits when button pressed
    public void Credits()
    {
        SceneManager.LoadScene(9);
    }

    //Function to quit game when quit button is pressed
    public void Quit()
    {
        //Quits application
        Application.Quit();
    }

}
