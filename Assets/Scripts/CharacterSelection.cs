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

    //Storing Buttons
    public GameObject select_Btn;
    public GameObject buy_Btn;

    //stores whether a character is locked or not
    int IsUnlockedRed;    
    int IsUnlockedYellow;

    //Text UI elements
    [SerializeField]
    TMP_Text tokenstext;

    [SerializeField]
    TMP_Text price;


    [SerializeField]
    TMP_Text notEnough;

    [SerializeField]
    GameObject notEnoughBG;

    [SerializeField]
    GameObject priceBG;

    public int tokens;
    SpriteRenderer sr;

    //red: 1, yellow: 2 on the array
    //true: 1, false: 0

    private void Start()
    {
        //retrieve locked/unlocked info
        IsUnlockedRed = PlayerPrefs.GetInt("Red");
        IsUnlockedYellow = PlayerPrefs.GetInt("Yellow");

        //retrive the tokens
        PlayerPrefs.SetInt("Tokens", 500);

        //chagne colour of UI element
        sr = notEnoughBG.GetComponent<SpriteRenderer>();
        sr.color = Color.blue;

        //present token
        tokens = PlayerPrefs.GetInt("Tokens");
        tokenstext.text = "Your Tokens: " + tokens.ToString();


    }

    private void Update()
    {

        //if the selected is locked or not
        if ( (selectedCharacter == 2 && IsUnlockedYellow == 0) || (selectedCharacter == 1 && IsUnlockedRed == 0) )
        {
            IsUnlockedRed = PlayerPrefs.GetInt("Red");
            IsUnlockedYellow = PlayerPrefs.GetInt("Yellow");

            //Set UI elements to active
            select_Btn.SetActive(false);
            buy_Btn.SetActive(true);
            price.gameObject.SetActive(true);
            priceBG.gameObject.SetActive(true);
        }
        else
        {
            //Set UI elements to false
            select_Btn.SetActive(true);
            buy_Btn.SetActive(false);
            price.gameObject.SetActive(false);
            priceBG.gameObject.SetActive(false);
        }
    }


    //use this fuction when the next button is pressed
    public void NextCharacter()
    {
        //algorithm for cycling through the array and presenting the character
        characters[selectedCharacter].SetActive(false);

        //Setting UI elements
        tokenstext.text = "Your Tokens: " + tokens.ToString();
        sr.color = Color.blue;

        selectedCharacter = (selectedCharacter + 1) % characters.Length;



        characters[selectedCharacter].SetActive(true);
       
   }

    public void PreviousCharacter()
    {
        //algorithm for cycling through the array and presenting the character
        characters[selectedCharacter].SetActive(false);

        //Setting UI elements
        tokenstext.text = "Your Tokens: " + tokens.ToString();
        sr.color = Color.blue;

        selectedCharacter = selectedCharacter - 1;
        
        if(selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }

    public void buy()
    {
        //if the player doesn't have enough tokens
        if(tokens < 200)
        {
            tokenstext.text = "Not Enough!";            
            
            sr.color = Color.red;
            return;
        }

        //take the price away and save it
        tokens -= 200;
        PlayerPrefs.SetInt("Tokens", tokens);      
                
        tokenstext.text = "Your Tokens: " + tokens.ToString();

        //buying process
        if ((selectedCharacter == 1 && IsUnlockedRed == 0) )
        {
            //if red
            Debug.Log("Red Bought!");
            select_Btn.SetActive(true);
            buy_Btn.SetActive(false);

            PlayerPrefs.SetInt("Red", 1);
            IsUnlockedRed = PlayerPrefs.GetInt("Red");
        }
        else if((selectedCharacter == 2 && IsUnlockedYellow == 0))
        {
            //if yellow
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
