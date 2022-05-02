using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script based on tutorial found at https://www.youtube.com/watch?v=R6scxu1BHhs
//editted to work in my context

public class CameraEdges : MonoBehaviour
{
    private GameObject[] edges; //Stores the locations for the top and bottom corner of the map
    private Camera cam; //Stores the camera game object
    private GameObject player; //Stores the player game object

    private float maxX; //Stores the maximum X coordinate of the map
    private float minX; //Stores the minimum X coordinate of the map
    private float maxY; //Stores the maximum Y coordinate of the map
    private float minY; //Stores the minimum Y coordinate of the map

    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>(); //Finds the Camera component of the gameObject the script it attached to

        player = GameObject.FindGameObjectWithTag("Player"); //finds the Player
        edges = GameObject.FindGameObjectsWithTag("MapEdge"); //finds all the map edges as an array

        //loops through each edge to find the minimum and maximum Y
        foreach(GameObject edge in edges)
        {
            //if it is top left then we have min X and max Y
            if(edge.gameObject.name == "TopLeft")
            {
                minX = edge.transform.position.x + (cam.orthographicSize * cam.aspect);
                maxY = edge.transform.position.y - cam.orthographicSize;
            }
            //else we have bottom right which is max X and min Y
            else
            {
                maxX = edge.transform.position.x - (cam.orthographicSize * cam.aspect);
                minY = edge.transform.position.y + cam.orthographicSize;
            }
        }
    }

    void Update()
    {
        //Restricts the camera X to be between the min and max and returns new value
        float newX = Mathf.Clamp(cam.transform.position.x, minX, maxX);

        //Restricts the camera Y to be between the min and max and returns new value
        float newY = Mathf.Clamp(cam.transform.position.y, minY, maxY);

        cam.transform.position = new Vector3(newX, newY, cam.transform.position.z); //Sets the camera to the new position
    }
}
