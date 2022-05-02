using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SpawnWave : MonoBehaviour
{
    private int wavenum = 1; //Stores current wave number
    private int numenemies; //Stores the number of enemies to be spawned this wave
    private int enemytype; //Stores which enemy should be spawned
    private GameObject[] spawnpoints; //Stores the spawnpoints for enemies
    private float waveCD = 5f; //Holds the cooldown time between waves

    //Passing in game objects to be spawned within waves
    public GameObject skeleton; //Holds the skeleton prefab to copy
    public GameObject goblin; //Holds the goblin prefab to copy
    public GameObject imp; //Holds the imp goblin prefab to copy
    public GameObject zombie; //Holds the imp prefab to copy
    public GameObject mage; //Holds the mage prefab to copy
    public GameObject boss; //Holds the boss enemy to be spawned
    public GameObject potion; //Holds the potion prefab to copy

    public GameObject completeMenu; //Holds the menu for the user once the level is complete

    public int level;
    public TMPro.TextMeshProUGUI wavedisplay; //Holds the text displaying the current wave number

    void Start()
    {
        //Gets all the gameobjects with the tag SpawnPoint
        spawnpoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        //starts the first wave
        StartCoroutine(Wave(wavenum));
    }

    // Update is called once per frame
    void Update()
    {   
        //If the waveCD is less than 0 stop decrementing
        if(waveCD > 0)
        {
            waveCD -= Time.deltaTime;
        }
        

        //if the number of enemies left is 0 or the wavenumber if less than 5
        if(numenemies == 0 && wavenum <= 5 && waveCD <= 0)
        {
            StartCoroutine(Wave(wavenum)); //Calls the wave with current wave number
        }
        else if(numenemies == 0 && wavenum > 5 && waveCD <= 0)
        {
            BossWave();
        }
    }

    //Function used to spawn the waves of enemies
    IEnumerator Wave(int wavenum)
    {
        int potionthiswave = Random.Range(0,2); // 50% chance of potion spawning this wave

        wavedisplay.text = "Wave: " + wavenum; //Update wave text

        //if potion to be spawned this wave
        if(potionthiswave == 1)
        {
            //instantiate potion at a random location on the map
            GameObject potioncopy = Instantiate(potion, spawnpoints[Random.Range(0,4)].transform.position, Quaternion.identity);
        }

        numenemies = wavenum * level; //set the number of enemies that will be in this wave

        for(int i = 0; i < wavenum * level; i++)
        {
            enemytype = Random.Range(1,wavenum+1); //random to select mobtype to be spawned

            //Adding enemy to game world
            if(enemytype == 1)
            {
                GameObject skeletonCopy = Instantiate(skeleton, spawnpoints[Random.Range(0,4)].transform.position, Quaternion.identity);
            }
            else if(enemytype == 2)
            {
                GameObject goblinCopy = Instantiate(goblin, spawnpoints[Random.Range(0,4)].transform.position, Quaternion.identity);
            }
            else if(enemytype == 3)
            {
                GameObject impCopy = Instantiate(imp, spawnpoints[Random.Range(0,4)].transform.position, Quaternion.identity);
            }
            else if(enemytype == 4)
            {
                GameObject zombieCopy = Instantiate(zombie, spawnpoints[Random.Range(0,4)].transform.position, Quaternion.identity);
            }
            else if(enemytype == 5)
            {
                GameObject mageCopy = Instantiate(mage, spawnpoints[Random.Range(0,4)].transform.position, Quaternion.identity);
            }

            //wait for a second before spawning next enemy
            yield return new WaitForSeconds(1f);
        }
    }

    //Funciton used to call the final wave and spawn only the boss
    void BossWave()
    {
        numenemies = 1;

        GameObject bossCopy = Instantiate(boss, spawnpoints[4].transform.position, Quaternion.identity);
    }

    //Function used to track enemies alive in the wave
    public void EnemyKilled()
    {
        numenemies--; //Decrement number of enemies

        //if the numenemies is 0 and its the end of the boss wave
        if(numenemies == 0 && wavenum == 6)
        {
            //Increment player level if < num maps
            if(level < 5)
            {
                level++;
                Debug.Log(level);
            }

            completeMenu.SetActive(true);
            //Bring up menu
            Time.timeScale = 0f;
        }

        //if end of wave, initiate cooldown between waves
        if(numenemies == 0)
        {
            wavenum++; //increase wave reached to increase difficulty
            waveCD = 5f;
        }

    }

}
