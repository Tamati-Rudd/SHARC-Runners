using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using Photon.Pun;

//This class writes the scoreboard and distrubutes tokens in the post game. Attach this script to the canvas in the post game screen.
public class WriteScoreboard : MonoBehaviour
{
    public TextMeshProUGUI scoreboardText;
    public TextMeshProUGUI tokensTXT;
    private int tokens;

    // Start is called before the first frame update
    void Start()
    {   
        PlacementManager placementManager = GameObject.FindGameObjectWithTag("PlacementManager").GetComponent<PlacementManager>();
        tokens = PlayerPrefs.GetInt("Tokens");

        //Build a string containing all FinishRecord information from the PlacementManager queue
        string scoreboardString = "";
        for (int i = 0; i < placementManager.playerCount; i++)
        {
            FinishRecord fr = placementManager.placements.Dequeue();
            int placement = fr.GetPlacement();

            //Distribute tokens
            string ordinal = "";
            if (placement == 1)
            {
                ordinal = "st";
                if (fr.GetName().Equals(PhotonNetwork.NickName))
                {
                    tokens = tokens + 100;
                    PlayerPrefs.SetInt("Tokens", tokens);
                    tokensTXT.text = "You won 100 Tokens!";
                }
            }
            else if (placement == 2)
            {
                ordinal = "nd";
                if (fr.GetName().Equals(PhotonNetwork.NickName))
                {
                    tokens = tokens + 80;
                    PlayerPrefs.SetInt("Tokens", tokens);
                    tokensTXT.text = "You won 80 Tokens!"; 
                }
            }     
            else if (placement == 3)
            {
                ordinal = "rd";
                if (fr.GetName().Equals(PhotonNetwork.NickName))
                {
                    tokens = tokens + 50;
                    PlayerPrefs.SetInt("Tokens", tokens);
                    tokensTXT.text = "You won 50 Tokens!";  
                }
            }
            else
            {
                ordinal = "th";
                if (fr.GetName().Equals(PhotonNetwork.NickName))
                {
                    tokens = tokens + 10;
                    PlayerPrefs.SetInt("Tokens", tokens);
                    tokensTXT.text = "You won 10 Tokens!";    
                }
            }

            scoreboardString += ""+placement+ordinal+": "+fr.GetName()+" in "+fr.GetTime()+"\n";
        }

        //Display the scoreboard
        scoreboardText.text = scoreboardString;
    }
}