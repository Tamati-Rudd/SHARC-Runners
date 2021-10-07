using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class NodeShiftingAbility : MonoBehaviour
{
    Dictionary<int, Vector2> nodeLocation = new Dictionary<int, Vector2>();
    private Vector2 SpawnPoint, SpawnPoint1, SpawnPoint2, SpawnPoint3, SpawnPoint4, SpawnPoint5;
    private GameObject[] nodes;
    public GameObject nodePrefab;
    private int recentNode = 0;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Start()
    {        
        InstantiateNodes();
    }


    void InstantiateNodes()
    {
        int sceneNo = SceneManager.GetActiveScene().buildIndex;

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



            Instantiate(nodePrefab, SpawnPoint, Quaternion.identity);
            nodeLocation.Add(1, SpawnPoint);

            Instantiate(nodePrefab, SpawnPoint1, Quaternion.identity);
            nodeLocation.Add(2, SpawnPoint2);

            Instantiate(nodePrefab, SpawnPoint2, Quaternion.identity);
            nodeLocation.Add(3, SpawnPoint2);

            Instantiate(nodePrefab, SpawnPoint3, Quaternion.identity);
            nodeLocation.Add(4, SpawnPoint3);

            Instantiate(nodePrefab, SpawnPoint4, Quaternion.identity);
            nodeLocation.Add(5, SpawnPoint4);

        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Node")
        {
            recentNode++;
        }
    }

    public void teleport()
    {
        try
        {
            int nextNode = recentNode + 1;

            Vector2 location = nodeLocation[nextNode];

            playerController.transform.position = location;
        }
        catch (Exception ex)
        {
            return;
        }

    }

}
