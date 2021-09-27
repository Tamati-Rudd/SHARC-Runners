using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//This class represents a single record of a player finishing the race, holding their name, time and placement
public class FinishRecord : MonoBehaviour
{
    private string name;
    private string time;
    private int placement;
    PhotonView PV;

    void start()
    {
        Debug.Log("Making Finish Record");
        if (!PV.IsMine)
        {
            Debug.Log("NOT MINE");
            Destroy(this);
        }
    }

    //Get method for name
    public string getName()
    {
        return name;
    }

    //Get method for time
    public string getTime()
    {
        return time;
    }

    //Get method for placement
    public int getPlacement()
    {
        return placement;
    }
    
    //Set method for name
    public void setName(string playerName)
    {
        name = playerName;
    }

    //Set method for time
    public void setTime(string playerTime)
    {
        time = playerTime;
    }

    //Set method for placement
    public void setPlacement(int playerPlacement)
    {
        placement = playerPlacement;
    }
}
