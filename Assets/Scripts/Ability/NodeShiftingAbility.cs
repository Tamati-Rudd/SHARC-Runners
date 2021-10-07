using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.SceneManagement;

public class NodeShiftingAbility : MonoBehaviour
{
    Dictionary<int, Vector2> nodeLocation = new Dictionary<int, Vector2>();
    private Vector2 SpawnPoint, SpawnPoint1, SpawnPoint2, SpawnPoint3, SpawnPoint4, SpawnPoint5;
    private GameObject[] nodes;

    void Start()
    {        
        InstantiateNodes();
    }


    void InstantiateNodes()
    {
        int sceneNo = SceneManager.GetActiveScene().buildIndex;

        if(sceneNo == 1)
        {
            SpawnPoint.x = 78.62f;
            SpawnPoint.y = -9.6f;

            SpawnPoint1.x = 84.01f;
            SpawnPoint1.y = 5.72f;

            SpawnPoint2.x = -4.26f;
            SpawnPoint2.y = 22.51f;

            SpawnPoint3.x = 100.96f;
            SpawnPoint3.y = 50.16f;

            SpawnPoint4.x = 42.5f;
            SpawnPoint4.y = 62.33f;

            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Node"), SpawnPoint, Quaternion.identity);
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Node"), SpawnPoint1, Quaternion.identity);
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Node"), SpawnPoint2, Quaternion.identity);
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Node"), SpawnPoint3, Quaternion.identity);
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Node"), SpawnPoint4, Quaternion.identity);

        }
        
    }

}
