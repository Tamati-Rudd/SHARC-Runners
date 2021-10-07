using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class NodeShiftingAbility : MonoBehaviour
{
    //Setting Up Variables

    //Stores the node locations
    Dictionary<int, Vector2> nodeLocation = new Dictionary<int, Vector2>();

    //Spawnpoints for the nodes
    private Vector2 SpawnPoint, SpawnPoint1, SpawnPoint2, SpawnPoint3, SpawnPoint4, SpawnPoint5;
    private GameObject[] nodes;
    public GameObject nodePrefab;
    private int recentNode = 0;
    private PlayerController playerController;

    private ArrayList recentCollision = new ArrayList();


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Start()
    {        
        InstantiateNodes();
    }

    //This method adds the node points/teleport points depending upon the map
    void InstantiateNodes()
    {
        int sceneNo = SceneManager.GetActiveScene().buildIndex;

        //check which game map we are in
        if (sceneNo == 1)
        {
            SpawnPoint.x = 106.66f;
            SpawnPoint.y = -9.6f;

            SpawnPoint1.x = 110.9f;
            SpawnPoint1.y = 5.72f;

            SpawnPoint2.x = 15.1f;
            SpawnPoint2.y = 22.51f;

            SpawnPoint3.x = 113.35f;
            SpawnPoint3.y = 50.16f;

            SpawnPoint4.x = 55.4f;
            SpawnPoint4.y = 62.33f;


            //set up the node points
            Instantiate(nodePrefab, SpawnPoint, Quaternion.identity);
            //add points to dictionary
            nodeLocation.Add(1, SpawnPoint);

            Instantiate(nodePrefab, SpawnPoint1, Quaternion.identity);
            nodeLocation.Add(2, SpawnPoint1);

            Instantiate(nodePrefab, SpawnPoint2, Quaternion.identity);
            nodeLocation.Add(3, SpawnPoint2);

            Instantiate(nodePrefab, SpawnPoint3, Quaternion.identity);
            nodeLocation.Add(4, SpawnPoint3);

            Instantiate(nodePrefab, SpawnPoint4, Quaternion.identity);
            nodeLocation.Add(5, SpawnPoint4);

        }

    }

    //This method keep track of the next node point
    public void OnTriggerEnter2D(Collider2D collision)
    {       

        if (collision.tag == "Node" && !recentCollision.Contains(collision.GetInstanceID().ToString()))
        {
            recentCollision.Add(collision.GetInstanceID().ToString());
            recentNode++;
            Debug.Log(recentNode);
        }
    }

    //This method changes the location of the player to the next node available
    public void teleport()
    {
        Debug.Log("Recent: " + recentNode);
        try
        {
            //keep track of the future node
            int nextNode = recentNode + 1;

            Vector2 location = nodeLocation[nextNode];

            //change position
            playerController.transform.position = location;
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }

    }

}
