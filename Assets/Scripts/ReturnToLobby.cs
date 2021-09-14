using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ReturnToLobby : MonoBehaviourPunCallbacks
{
    public Button button;

   void Start()
    {
        button.onClick.AddListener(ReturnPlayerToLobby);
    }

    
    public void ReturnPlayerToLobby()
    {
       GameObject winnerRecord = GameObject.FindGameObjectWithTag("WinRecord");
       Destroy(winnerRecord);
       Destroy(Roommanager.Instance.gameObject);
       LeaveRoom();
       //SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);

        base.OnLeftRoom();
    }
}
