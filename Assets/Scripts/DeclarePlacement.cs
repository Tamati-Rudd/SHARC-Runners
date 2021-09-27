using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

//This class declares a players race placement in the post game scene. Attach this script to the canvas in the post game screen.
public class DeclarePlacement : MonoBehaviour
{
    TextMeshProUGUI placementText;
    TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        //Get player data
        //WinnerRecord win = GameObject.FindGameObjectWithTag("WinRecord").GetComponent<WinnerRecord>();
        FinishRecord fr = GameObject.FindGameObjectWithTag("FinishRecord").GetComponent<FinishRecord>();
        string playerName = fr.getName();
        string timeTaken = fr.getTime();
        int playerPlacement = fr.getPlacement();
        string ordinal = "";
        if (playerPlacement == 1)
            ordinal = "st";
        else if (playerPlacement == 2)
            ordinal = "nd";
        else if (playerPlacement == 3)
            ordinal = "rd";
        else
            ordinal = "th";
        Debug.Log(playerName + " came in place " + playerPlacement + " in " + timeTaken);

        //Get reference to the TextMeshProUGUI components
        Transform placementChild = transform.Find("PlacementText");
        placementText = placementChild.GetComponent<TextMeshProUGUI>();
        Transform timeChild = transform.Find("TimeTaken");
        timerText = timeChild.GetComponent<TextMeshProUGUI>();

        //Update the text to declare the winner of the race and state their time
        placementText.text = playerName + " Came " + playerPlacement + ordinal + "!";
        timerText.text = "Time Taken: " + timeTaken;
    }
}