using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Roommanager : MonoBehaviourPunCallbacks
{
    public static Roommanager Instance;

    // Start is called before the first frame update
    void Start()
    {

    }

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
        if (scene.buildIndex == 1)//we are in the game scene
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector2.zero, Quaternion.identity);
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "EnemyManager"), Vector2.zero, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
