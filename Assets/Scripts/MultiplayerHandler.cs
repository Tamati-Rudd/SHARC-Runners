using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

/*
 *This class deals with handling the lobby and room functionality
 *
 */

public class MultiplayerHandler : MonoBehaviourPunCallbacks
{
    public static MultiplayerHandler Instance;

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] public Transform roomListContent;
    [SerializeField] public GameObject roomListItemPrefab;
    [SerializeField] public Transform PlayerListContent;
    [SerializeField] public GameObject PlayerListItemPrefab;
    [SerializeField] GameObject StartGameBtn;
    [SerializeField] GameObject ReadyBtn;
    [SerializeField] GameObject Map1Btn;
    [SerializeField] GameObject Map2Btn;
    private int readyCounter = 1;
    private int playerCount = 0;
    private PhotonView PV;
    Player[] players;
    private int mapIndex = 1;


    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    private void Start()
    {
        Map1Btn.SetActive(false);
        Map2Btn.SetActive(false);

        Debug.Log("Connected to Master");
        //automatically load scene for all the clients in room when hosts switches scene
        PhotonNetwork.AutomaticallySyncScene = true;

        //Establishes Connection set out in the Photon Settings in Resource Folder
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();
        else //When the player is returning to the menu scene from the post game scene
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Returned to Menu Scene");
        }
    }

    //Once connected to the master server
    public override void OnConnectedToMaster()
    {
        //connecting to a lobby
        PhotonNetwork.JoinLobby();
    }

    //Once the lobby is joined
    public override void OnJoinedLobby()
    {
     
        MenuManager.Instance.OpenMenu("Title");
        Debug.Log("Joined Lobby");
        
    }

    //Create Room
    public void CreateRoom()
    {
        readyCounter = 1;
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("Loading");

   }

    //if create room was successful, OnJoinedRoom will be called
    public override void OnJoinedRoom()
    {
        StartGameBtn.SetActive(false);

        if (!PhotonNetwork.IsMasterClient)
        {
            PV.RPC("disableStart", RpcTarget.MasterClient);
        }
        else
        {
            ReadyBtn.SetActive(false);
            Map1Btn.SetActive(true);
            Map2Btn.SetActive(true);
        }

        Debug.Log("Joined room!");
        MenuManager.Instance.OpenMenu("Room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
                
        players = PhotonNetwork.PlayerList;
        playerCount = players.Length;

        //destroy all the players that existed before joining the room
        foreach (Transform child in PlayerListContent)
        {
            Destroy(child.gameObject);
        }

        //create the players
        for (int i = 0; i < players.Length; i++)
        {

            Instantiate(PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        //if it is the host, set the button to active
        if (players.Length == 1)
        {
            //If only one player is in the lobby then set start button to active
            StartGameBtn.SetActive(true);

        }




    }

    //host migration if host leaves the room
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        //set the button active to the new host
        StartGameBtn.SetActive(PhotonNetwork.IsMasterClient);
    }

    //if create room was unsuccessful, OnCreateRoomFailed will be called
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //errorText.text = "Room Creation Failed" + message;
        
    }
    public void ChangeMap1()
    {
        mapIndex = 1;
        Debug.Log("Map 1 Selected!");
    }

    public void ChangeMap2()
    {
        mapIndex = 4;
        Debug.Log("Map 2 Selected!");

    }

    public void StartGame()
    {
        MenuManager.Instance.CloseMenu("Room");
        // makes room close 
        PhotonNetwork.CurrentRoom.IsOpen = false;
        // makes room invisible to random match making
        PhotonNetwork.CurrentRoom.IsVisible = false; 
        //all players in lobby load into the level
        PhotonNetwork.LoadLevel(mapIndex);
    }

    


    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        ReadyBtn.SetActive(true);
        readyCounter = 1;
        PV.RPC("decreaseCounter", RpcTarget.MasterClient);
        PV.RPC("ReadyReset", RpcTarget.AllBuffered);
        MenuManager.Instance.OpenMenu("Loading");
       
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("Title2");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //destroy all the buttons on screen evertime it is updated
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            //check to see if a room has been removed, if yes, then dont instantiate it again
            if (roomList[i].RemovedFromList)
            {
                continue;
            }

            //instantiate the button for how many rooms there are
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }

    }

    //once players join a room, their username needs to be istantiated for everyone
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    private void Update()
    {
       //ReadyUp();
    }

    public void ReadyUp()
    {
        ReadyBtn.SetActive(false);
        PV.RPC("increaseCounter", RpcTarget.MasterClient); 
        
    }


    [PunRPC]
    void increaseCounter()
    {
        readyCounter++;
        players = PhotonNetwork.PlayerList;

        Debug.Log("Ready Counter: " + readyCounter);
        Debug.Log("Player in lobby: " + players.Length);

        if (readyCounter == players.Length)
        {
            //If the amount of ready players is equal to the amount of players in lobby then set start button to active
            StartGameBtn.SetActive(true);
            //StartGameBtn.SetActive(PhotonNetwork.IsMasterClient);
        }
        else
        {
            StartGameBtn.SetActive(false);
        }
    }

    [PunRPC]
    void decreaseCounter()
    {
        readyCounter--;
        players = PhotonNetwork.PlayerList;
        if (readyCounter == players.Length)
        {
            //If the amount of ready players is equal to the amount of players in lobby then set start button to active
            StartGameBtn.SetActive(false);
            //StartGameBtn.SetActive(PhotonNetwork.IsMasterClient);
        }
        else
        {
            StartGameBtn.SetActive(true);
        }
    }

    [PunRPC]
    void disableStart()
    {
        StartGameBtn.SetActive(false);
    }

    [PunRPC]
    void ReadyReset()
    {
        StartGameBtn.SetActive(false);
        ReadyBtn.SetActive(false);
        readyCounter = 0;
    }
}
