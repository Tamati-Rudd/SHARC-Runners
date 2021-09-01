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
    PhotonView canvasPV;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    [PunRPC]
    void StateWinner(string winnerName)
    {
        Debug.Log("Stating Winner: " + winnerName);
        //Get reference to the TextMeshProUGUI component
        Transform child = transform.Find("WinnerText");
        WinnerText = child.GetComponent<TextMeshProUGUI>();

        //Update the text to declare the winner of the race
        WinnerText.text = winnerName + " Wins!";
    }
}
