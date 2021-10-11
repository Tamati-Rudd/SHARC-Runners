using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

//This class instantiates controllers into the game which are necessary. This script manages
//the game
public class Roommanager : MonoBehaviourPunCallbacks
{
    public static Roommanager Instance;
    
    private void Awake()
    {
        //check to see if another RoomManager exists
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        //make sure there is only one RoomManager
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1 || scene.buildIndex == 4)//we are in the game scene
        {
            //instantiate controllers/managers  
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector2.zero, Quaternion.identity);
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "EnemyManager"), Vector2.zero, Quaternion.identity);
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "SabotageController"), Vector2.zero, Quaternion.identity);
        }
    }

}
