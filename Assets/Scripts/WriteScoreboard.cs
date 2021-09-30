using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using Photon.Pun;

//This class writes the scoreboard in the post game. Attach this script to the canvas in the post game screen.
public class WriteScoreboard : MonoBehaviour
{
    public TextMeshProUGUI scoreboardText;

    // Start is called before the first frame update
    void Start()
    {   
        PlacementManager placementManager = GameObject.FindGameObjectWithTag("PlacementManager").GetComponent<PlacementManager>();

        //Build a string containing all FinishRecord information from the PlacementManager queue
        string scoreboardString = "";
        for (int i = 0; i < placementManager.playerCount; i++)
        {
            FinishRecord fr = placementManager.placements.Dequeue();
            int placement = fr.getPlacement();

            string ordinal = "";
            if (placement == 1)
                ordinal = "st";
            else if (placement == 2)
                ordinal = "nd";
            else if (placement == 3)
                ordinal = "rd";
            else
                ordinal = "th";

            scoreboardString += ""+placement+ordinal+": "+fr.getName()+" in "+fr.getTime()+"\n";
        }

        //Display the scoreboard
        scoreboardText.text = scoreboardString;
    }
}