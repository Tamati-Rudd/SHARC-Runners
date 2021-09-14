using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

//This class declares the winner of the race in the post game scene. Attach this script to the canvas in the post game screen.

public class DeclareWinner : MonoBehaviour
{
    TextMeshProUGUI winnerText;
    TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        //Get winner data
        WinnerRecord win = GameObject.FindGameObjectWithTag("WinRecord").GetComponent<WinnerRecord>();
        string winnerName = win.winnerName;
        string timeTaken = win.winnerTime;
        Debug.Log(winnerName + " won in "+timeTaken);

        //Get reference to the TextMeshProUGUI components
        Transform winnerChild = transform.Find("WinnerText");
        winnerText = winnerChild.GetComponent<TextMeshProUGUI>();
        Transform timeChild = transform.Find("TimeTaken");
        timerText = timeChild.GetComponent<TextMeshProUGUI>();

        //Update the text to declare the winner of the race and state their time
        winnerText.text = winnerName + " Wins!";
        timerText.text = "Time Taken: " + timeTaken;
    }
}
