using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//This class declares the winner of the race in the post game scene. Attach this script to the canvas in the post game screen.

public class DeclareWinner : MonoBehaviour
{
    TextMeshProUGUI WinnerText;

    // Start is called before the first frame update
    void Start()
    {
        //Get reference to the TextMeshProUGUI component
        Transform child = transform.Find("WinnerText");
        WinnerText = child.GetComponent<TextMeshProUGUI>();

        //Update the text to declare the winner of the race
        WinnerText.text = FinishPoint.winnerName + " Wins!";
    }
}
