using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class records the winner of a race, and their completion time
public class WinnerRecord : MonoBehaviour
{
    public string winnerName;
    public string winnerTime;

    //Update the winner name record
    public void updateWinnerName(string winner)
    {
        winnerName = winner;
    }

    //Update the winner time record
    public void updateWinnerTime(string time)
    {
        winnerTime = time;
    }
}
