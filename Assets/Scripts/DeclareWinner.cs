using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

//This class declares the winner of the race in the post game scene. Attach this script to the canvas in the post game screen.

public class DeclareWinner : MonoBehaviour
{
    TextMeshProUGUI WinnerText;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject canvas = PhotonNetwork.Instantiate("Canvas", new Vector3(0,0,0), Quaternion.identity, 0);

        WinnerRecord win = GameObject.FindGameObjectWithTag("WinRecord").GetComponent<WinnerRecord>();
        string winnerName = win.winnerName;
        Debug.Log("Winner: " + winnerName);
        //Get reference to the TextMeshProUGUI component
        Transform child = transform.Find("WinnerText");
        WinnerText = child.GetComponent<TextMeshProUGUI>();

        //Update the text to declare the winner of the race
        WinnerText.text = winnerName + " Wins!";
    }
}
