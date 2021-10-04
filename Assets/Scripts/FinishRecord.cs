using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//This class represents a single record of a player finishing the race, holding their name, time and placement
//Replaces the WinnerRecord in the race placement system
public class FinishRecord
{
    private string name;
    private string time;
    private int placement;

    //Construct a FinishRecord object
    public FinishRecord(string playerName, string playerTime, int playerPlacement)
    {
        setName(playerName);
        setTime(playerTime);
        setPlacement(playerPlacement);
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
