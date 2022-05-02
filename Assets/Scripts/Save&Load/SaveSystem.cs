using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//Based on tutorial found at: https://www.youtube.com/watch?v=XOjd_qU2Ido 
public static class SaveSystem
{
    //Function used to save data about the player
    //passed in variables are used to store as PlayerData to be saved
    public static void SavePlayer(int bal, int attack, int health, int speed, int level)
    {
        BinaryFormatter formatter = new BinaryFormatter(); //Creates a new binary formatter

        string path = Application.persistentDataPath + "/player.sav"; //Used to store a path to the player save that works across different operating systems

        FileStream stream = new FileStream(path, FileMode.Create); //Opens a new filestream to create a new file at the path

        PlayerData data = new PlayerData(bal, attack, health, speed, level); //Creates a new Playerdata full of the data to save

        formatter.Serialize(stream, data); //saves the playerdata at the path
        stream.Close(); //Closes the file stream
    }

    //Function used to load the player from a save file
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.sav"; //Stores path the file to load is held at

        //if the file exists at the path
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter(); //Creates a new binary formatter
            FileStream stream = new FileStream(path, FileMode.Open); //Opens a new filestream at path to open the file

            PlayerData data = formatter.Deserialize(stream) as PlayerData; //reads in the file at path and output the data read into a PlayerData variable
        
            stream.Close(); //Closes the filestream

            return data; //returns the players data that was read in
        }
        //if there was no file found at the path
        else
        {
            return null; //returns that no file was found
        }
    }

}
