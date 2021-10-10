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
        SetName(playerName);
        SetTime(playerTime);
        SetPlacement(playerPlacement);
    }

    //Get method for name
    public string GetName()
    {
        return name;
    }

    //Get method for time
    public string GetTime()
    {
        return time;
    }

    //Get method for placement
    public int GetPlacement()
    {
        return placement;
    }
    
    //Set method for name
    public void SetName(string playerName)
    {
        name = playerName;
    }

    //Set method for time
    public void SetTime(string playerTime)
    {
        time = playerTime;
    }

    //Set method for placement
    public void SetPlacement(int playerPlacement)
    {
        placement = playerPlacement;
    }
}
