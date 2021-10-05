using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script contains how the tutorial texts are displayed on screen
public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialPopUps; //Array of text for the tutorial heads up
    private int popUpIndex; //Current index of pop ups element in array of pop ups

    private void Update()
    {
        //Displays current tutorial task for player
        for (int i = 0; i < tutorialPopUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                tutorialPopUps[popUpIndex].SetActive(true);
            }
            else
            {
                 tutorialPopUps[popUpIndex].SetActive(false);
            }
        }

        if (popUpIndex == 0)
        {
            //Display how to move left and right 
           if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
           {
               popUpIndex++;
           }
        }
        else if(popUpIndex == 1)
        {
            //Display how to Jump, Double jump and Wall Jump
            if(Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;
            }
        }
        else if(popUpIndex == 3)
        {
            //Display how to Wall Swing
            if(Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if(popUpIndex == 4)
        {
            //Pick up Sabotage
            /*if(Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }*/
        }
        else if(popUpIndex == 6)
        {
            //Display how to kill and shoot enemy
            if(Input.GetMouseButtonDown(1))
            {
                popUpIndex++;
            }
        }
        else if(popUpIndex == 7)
        {
            //Display how to Power Up
            if(Input.GetKeyDown("R"))
            {
                popUpIndex++;
            }
            
        }
 
    }
}
