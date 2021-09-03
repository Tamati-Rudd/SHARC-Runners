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
    
    public GameObject HUDContainer;
    public Stopwatch Timer;
    public bool isCreated = false;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");



       
    }

    // Start is called before the first frame update
    void Start()
    {

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
            
            CreateCountdown(prefab);
            CreateMeter(prefab);

        }
        if (selectedCharacter == 1)
        {
            //Spawn the Player
            GameObject prefab = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerRed"), Vector2.zero, Quaternion.identity);

            CreateCountdown(prefab);
            CreateMeter(prefab);
            CreateMeter(prefab);
            
        }
        if (selectedCharacter == 2)
        {
            //Spawn the Player
            GameObject prefab = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerYellow"), Vector2.zero, Quaternion.identity);

            CreateCountdown(prefab);
            CreateMeter(prefab);
            CreateMeter(prefab);

        }


    }

    void CreateMeter(GameObject prefab)
    {
        MeterScript meter = Instantiate(meterScript, canvas.transform);        
        prefab.GetComponent<Collectable>().abilityMeter = meter;

        Text counterclone = Instantiate(Counter, canvas.transform);
        prefab.GetComponent<Collectable>().Counter = counterclone;
        
    }

    void CreateCountdown(GameObject prefab)
    {
        //instantiating the countdown
        GameObject HUD = Instantiate(HUDContainer, canvas.transform);
        HUD.GetComponent<CountdownController>().player = prefab.GetComponent<PlayerController>();

        //instantiating the timer
        Stopwatch Timerclone = Instantiate(Timer, canvas.transform);
        HUD.GetComponent<CountdownController>().timer = Timerclone;
    }

    


}
