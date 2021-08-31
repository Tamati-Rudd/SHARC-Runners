using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Stopwatch : MonoBehaviour
{
    bool stopWatchActive;
    private float currentTime;
    public Text currentTimeText;

    void Start()
    {
        currentTime = 0;
        StartStopwatch();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopWatchActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text =  time.ToString(@"mm\:ss\:ff");
    }

    public void StartStopwatch()
    {
        stopWatchActive = true;
    }

    public void StopStopwatch()
    {   
        stopWatchActive = false;
    }

}
