using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerRecord : MonoBehaviour
{
    public string winnerName;
    public string winnerTime;

    public void updateWinnerName(string winner)
    {
        winnerName = winner;
    }

    public void updateWinnerTime(string time)
    {
        winnerTime = time;
    }
}
