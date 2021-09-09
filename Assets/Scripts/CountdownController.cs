using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public TMP_Text countdownDisplay;
    public PlayerController player;
    public Stopwatch timer;

    private void Start()
    {
        StartCoroutine(CountdownStart());
    }

    IEnumerator CountdownStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        player.isDisabled = false;
        player.startedRace = true;
        //player.PV.RPC("EnablePlayerRPC", RpcTarget.All);

        timer.StartStopwatch();

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
    }
}
