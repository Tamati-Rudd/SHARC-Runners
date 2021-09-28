using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//This Script manages the character selection in the menu
public class CharacterSelection : MonoBehaviour
{
    //store all the characters in an array
    public GameObject[] characters;
    public int selectedCharacter = 0;

    public GameObject select_Btn;
    public GameObject buy_Btn;
    int IsUnlockedRed;    
    int IsUnlockedYellow;

    [SerializeField]
    TMP_Text tokenstext;

    [SerializeField]
    TMP_Text price;


    [SerializeField]
    TMP_Text notEnough;

    [SerializeField]
    GameObject notEnoughBG;

    public int tokens;


    //red: 1, yellow: 2 on the array
    //true: 1, false: 0

    private void Start()
    {
        IsUnlockedRed = PlayerPrefs.GetInt("Red");
        IsUnlockedYellow = PlayerPrefs.GetInt("Yellow");

        PlayerPrefs.SetInt("Tokens", 500);



        tokens = PlayerPrefs.GetInt("Tokens");
        tokenstext.text = "Your Tokens: " + tokens.ToString();


    }

    private void Update()
    {

        if ( (selectedCharacter == 2 && IsUnlockedYellow == 0) || (selectedCharacter == 1 && IsUnlockedRed == 0) )
        {
            IsUnlockedRed = PlayerPrefs.GetInt("Red");
            IsUnlockedYellow = PlayerPrefs.GetInt("Yellow");

            select_Btn.SetActive(false);
            buy_Btn.SetActive(true);
            price.gameObject.SetActive(true);
        }
        else
        {            
            select_Btn.SetActive(true);
            buy_Btn.SetActive(false);
        }
    }


    //use this fuction when the next button is pressed
    public void NextCharacter()
    {
        //algorithm for cycling through the array and presenting the character
        characters[selectedCharacter].SetActive(false);
        notEnough.gameObject.SetActive(false);
        notEnoughBG.SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;



        characters[selectedCharacter].SetActive(true);
       
   }

    public void PreviousCharacter()
    {
        //algorithm for cycling through the array and presenting the character
        characters[selectedCharacter].SetActive(false);
        notEnough.gameObject.SetActive(false);
        notEnoughBG.SetActive(false);
        selectedCharacter = selectedCharacter - 1;
        
        if(selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }

    public void buy()
    {
        if(tokens < 200)
        {
            notEnough.gameObject.SetActive(true);
            notEnoughBG.SetActive(true);
            return;
        }
        tokens -= 200;
        PlayerPrefs.SetInt("Tokens", tokens);      
                
        tokenstext.text = "Your Tokens: " + tokens.ToString();

        if ((selectedCharacter == 1 && IsUnlockedRed == 0) )
        {
            Debug.Log("Red Bought!");
            select_Btn.SetActive(true);
            buy_Btn.SetActive(false);

            PlayerPrefs.SetInt("Red", 1);
            IsUnlockedRed = PlayerPrefs.GetInt("Red");
        }
        else if((selectedCharacter == 2 && IsUnlockedYellow == 0))
        {

            Debug.Log("Yellow Bought!");
            select_Btn.SetActive(true);
            buy_Btn.SetActive(false);

            PlayerPrefs.SetInt("Yellow", 1);
            IsUnlockedYellow = PlayerPrefs.GetInt("Yellow");
        }

    }

    //Storing the player's selection using PlayerPrefs
    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        int test = PlayerPrefs.GetInt("selectedCharacter");
        Debug.Log("Your selected Character is: " + test);

    }
}
