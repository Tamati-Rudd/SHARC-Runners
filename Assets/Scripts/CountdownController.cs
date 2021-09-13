using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

//This class countsdown from 3 once the player has loaded into the game scene
public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    [HideInInspector]public TMP_Text countdownDisplay;
    public PlayerController player;
    public Stopwatch timer;

    //function to start the countdown
    public IEnumerator CountdownStart()
    {
        //while countdown still isn't 0
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        //once coutndown reaches 0
        countdownDisplay.text = "GO!";

        //allow players to move
        player.isDisabled = false;
        player.raceStarted = true;

        //Start the timer
        timer.StartStopwatch();

        yield return new WaitForSeconds(1f);

        //Deactivate the HUD
        countdownDisplay.gameObject.SetActive(false);
    }
}
