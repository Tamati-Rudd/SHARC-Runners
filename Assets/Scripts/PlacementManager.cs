using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class records how many players have finished in order to assign placements
public class PlacementManager : MonoBehaviour
{
    private int playersFinished;

    // Start is called before the first frame update
    void Start()
    {
        playersFinished = 0;
    }

    //This method registers that a player finished and returns their placement as an integer
    public int registerFinish()
    {
        playersFinished++;
        return playersFinished;
    }
}
