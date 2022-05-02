using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool paused = false; //Holds if the game is currently paused or not
    public GameObject pause;
    public GameObject player;
    public GameObject waveController;

    // Update is called once per frame
    void Update()
    {
        //if esc is pressed and game is not pause then pause it else if it is already paused then resume
        if(Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            Pause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            UnPause();
        }
    }

    //Function used to pause the game 
    void Pause()
    {
        paused = true;
        Time.timeScale = 0f; //Turn off the game time
        pause.SetActive(paused); //Bring up pause menu
    }

    //Function used to unpause the game
    public void UnPause()
    {
        paused = false;
        Time.timeScale = 1f; //Turn on the game time
        pause.SetActive(paused); //Put pause menu away
    }

    //Function used to move the player to the next level or to replay the same one
    public void Continue()
    {
        //Save the player
        SaveSystem.SavePlayer(player.GetComponent<Balance>().balance, player.GetComponent<PlayerAttack>().attackLevel, player.GetComponent<PlayerHealth>().healthLevel, player.GetComponent<PlayerMovement>().speedLevel, waveController.GetComponent<SpawnWave>().level);

        UnPause();

        SceneManager.LoadScene(waveController.GetComponent<SpawnWave>().level); //loads the scene for the players current level
    }

    //Function used to save the players data and quit back to the main menu
    public void SaveQuit()
    {
        //Saves the player
        SaveSystem.SavePlayer(player.GetComponent<Balance>().balance, player.GetComponent<PlayerAttack>().attackLevel, player.GetComponent<PlayerHealth>().healthLevel, player.GetComponent<PlayerMovement>().speedLevel, waveController.GetComponent<SpawnWave>().level);

        UnPause();

        SceneManager.LoadScene(0); //Loads the main menu
    }


}
