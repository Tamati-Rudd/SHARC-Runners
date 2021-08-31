using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    PhotonView PV;
    int selectedCharacter = 0;
    public MeterScript meterScript;
    public Text Counter;
    private Canvas canvas;
    private GameObject container;
    public GameObject enemy;
    public GameObject gem;
    public Vector2 SpawnPoint;
    public GameObject HUDContainer;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");

    }

    // Start is called before the first frame update
    void Start()
    {

        SpawnPoint.x = (float)21.8;
        SpawnPoint.y = (float)-1.9;

        if (PV.IsMine)
        {
            CreateController();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateController()
    {

        
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        

        if (selectedCharacter == 0)
        {
            //Spawn the Player
            GameObject prefab = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerBlue"), Vector2.zero, Quaternion.identity);

            CreateEnemy(prefab, SpawnPoint);

            GameObject HUD = Instantiate(HUDContainer, canvas.transform);                  
            HUD.GetComponent<CountdownController>().player = prefab.GetComponent<PlayerController>();

            CreateMeter(prefab);

        }
        if (selectedCharacter == 1)
        {
            //Spawn the Player
            GameObject prefab = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerRed"), Vector2.zero, Quaternion.identity);

            CreateEnemy(prefab, SpawnPoint);

            GameObject HUD = Instantiate(HUDContainer, canvas.transform);
            HUD.GetComponent<CountdownController>().player = prefab.GetComponent<PlayerController>();

            CreateMeter(prefab);


        }
        if (selectedCharacter == 2)
        {
            //Spawn the Player
            GameObject prefab = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerYellow"), Vector2.zero, Quaternion.identity);

            CreateEnemy(prefab, SpawnPoint);

            GameObject HUD = Instantiate(HUDContainer, canvas.transform);
            HUD.GetComponent<CountdownController>().player = prefab.GetComponent<PlayerController>();

            CreateMeter(prefab);

        }


    }

    void CreateMeter(GameObject prefab)
    {
        Vector2 meterlocation;
        meterlocation.x = 90;
        meterlocation.y = 90;
               
        MeterScript meter = Instantiate(meterScript, meterlocation, Quaternion.identity);
        meter.transform.SetParent(canvas.transform);
        prefab.GetComponent<Collectable>().abilityMeter = meter;

        Text counterclone = Instantiate(Counter, meterlocation, Quaternion.identity);
        counterclone.transform.SetParent(canvas.transform);
        prefab.GetComponent<Collectable>().Counter = counterclone;
        
    }

    void CreateEnemy(GameObject prefab, Vector2 SpawnPoint)
    {
        GameObject enemyclone = Instantiate(enemy, SpawnPoint, Quaternion.identity);
        enemyclone.GetComponent<EnemyLife>().gemPrefab = gem;
        enemyclone.GetComponent<EnemyAI>().player = prefab.GetComponent<Transform>();
    }
}
