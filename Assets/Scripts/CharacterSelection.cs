using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Script manages the character selection in the menu
public class CharacterSelection : MonoBehaviour
{
    //store all the characters in an array
    public GameObject[] characters;
    public int selectedCharacter = 0;

    //use this fuction when the next button is pressed
    public void NextCharacter()
    {
        //algorithm for cycling through the array and presenting the character
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        //algorithm for cycling through the array and presenting the character
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = selectedCharacter - 1;
        
        if(selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }

    //Storing the player's selection using PlayerPrefs
    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        int test = PlayerPrefs.GetInt("selectedCharacter");
        Debug.Log("Your selected Character is: " + test);

    }
}
