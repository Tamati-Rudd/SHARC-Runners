using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    PhotonView PV;
    int selectedCharacter = 0;

    //ability
    public MeterScript meterScript;
    public Text Counter;

    //Canvas
    private Canvas canvas;
    private GameObject container;
    
    //HUD
    public GameObject HUDContainer;
    public Stopwatch Timer;
    public bool isCreated = false;

    //Finish Point
    public FinishPoint finish;

    private void Awake()
    {
        //Get PhotonView and get the selected character
        PV = GetComponent<PhotonView>();
        selectedCharacter = PlayerPrefs.GetInt("selectedCharacter"); 
    }

    // Start is called before the first frame update
    void Start()
    {
        //create controller for just the playe
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    //This functions creates neccessary controllers related to the player
    void CreateController()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();   

        //blue character
        if (selectedCharacter == 0)
        {
            //Spawn the Player
            GameObject prefab = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerBlue"), Vector2.zero, Quaternion.identity);

            //Creating HUD
            CreateCountdown(prefab);

            //Creating Meter for ability
            CreateMeter(prefab);
        }
        if (selectedCharacter == 1)
        {
            //Spawn the Player
            GameObject prefab = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerRed"), Vector2.zero, Quaternion.identity);

            CreateCountdown(prefab);
            CreateMeter(prefab);
        }
        if (selectedCharacter == 2)
        {
            //Spawn the Player
            GameObject prefab = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerYellow"), Vector2.zero, Quaternion.identity);

            CreateCountdown(prefab);
            CreateMeter(prefab);
        }


    }

    //This function creates the meter for the player
    void CreateMeter(GameObject prefab)
    {
        //instantiating inside a canvas
        MeterScript meter = Instantiate(meterScript, canvas.transform);     
        
        //settings for the ability for the specific player
        prefab.GetComponent<Collectable>().abilityMeter = meter;
        prefab.GetComponent<Collectable>().abilityMeter.SetMaxAbility(8);
        prefab.GetComponent<Collectable>().abilityMeter.SetAbility(0);

 
        Text counterclone = Instantiate(Counter, canvas.transform);
        prefab.GetComponent<Collectable>().Counter = counterclone;
        
    }

    public void OnPhotonPlayerConnected(Player player)
    {
        Debug.Log("Player Connected " + player.NickName);
    }


    //this function create the countdown once the player instantiates into the game
    void CreateCountdown(GameObject prefab)
    {
        //instantiating the countdown
        GameObject HUD = Instantiate(HUDContainer, canvas.transform);
        HUD.GetComponent<CountdownController>().player = prefab.GetComponent<PlayerController>();

        //instantiating the timer
        Stopwatch Timerclone = Instantiate(Timer, canvas.transform);
        HUD.GetComponent<CountdownController>().timer = Timerclone;

        //start the timer
        HUD.GetComponent<CountdownController>().StartCoroutine(HUD.GetComponent<CountdownController>().CountdownStart());

        //Create fnish point once countdown ends
        CreateFinishPoint(Timerclone);
    }

    //Create the finish point
    void CreateFinishPoint(Stopwatch Timerclone)
    {
        Vector2 finishPointLocation;
        finishPointLocation.x = (float)137.5;
        finishPointLocation.y = (float)-12.35;
        FinishPoint FinishPointClone = Instantiate(finish, finishPointLocation, Quaternion.identity);
        FinishPointClone.GetComponent<FinishPoint>().timer = Timerclone;
    }
}
