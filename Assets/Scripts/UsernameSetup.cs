using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class UsernameSetup : MonoBehaviourPunCallbacks
{
    public TMPro.TMP_InputField usernameInput;
    //public static string username;
    public void Setup()
    {
        
        PlayerPrefs.SetString("username", usernameInput.text);
        PlayerPrefs.Save();




    }
}
