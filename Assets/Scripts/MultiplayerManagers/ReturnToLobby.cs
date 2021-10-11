using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

//This class handles a press of the Return to Lobby button in the PostGame screen
public class ReturnToLobby : MonoBehaviourPunCallbacks
{
    public Button button;

    void Start()
    {
        button.onClick.AddListener(ReturnPlayerToLobby);
    }

    //Returns the player to lobby after cleaning up don't destroy GameObjects
    public void ReturnPlayerToLobby()
    {
        GameObject placementManager = GameObject.FindGameObjectWithTag("PlacementManager");
        Destroy(placementManager);
        Destroy(Roommanager.Instance.gameObject);
        LeaveRoom();
    }

    //Leave the room
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    //When the room is left
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);

        base.OnLeftRoom();
    }
}
