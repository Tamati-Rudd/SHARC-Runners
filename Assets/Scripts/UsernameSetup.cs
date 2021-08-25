using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class UsernameSetup : MonoBehaviourPunCallbacks
{
    public TMPro.TMP_InputField username;

    public void Setup()
    {
        if (string.IsNullOrEmpty(username.text))
        {
            PlayerPrefs.SetString("username", "Guest-Player");
        }
        else
        {
            PlayerPrefs.SetString("username", username.text);
        }

        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
           Debug.Log(players[i].ToString());
        }
       
        /// PhotonNetwork.NickName;


    }
}
