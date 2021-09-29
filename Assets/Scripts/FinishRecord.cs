using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//This class represents a single record of a player finishing the race, holding their name, time and placement
public class FinishRecord : MonoBehaviour
{
    public string name;
    public string time;
    public int placement;
    public PhotonView PV;

    [PunRPC]
    public void clearIfNotMine()
    {
        if (PV == null)
            Debug.Log("Broken");
        if (!PV.IsMine)
        {
            Debug.Log("NOT MINE");
            PhotonNetwork.Destroy(PhotonView.Find(PV.ViewID));
        }
        else
            Debug.Log("MINE");
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
