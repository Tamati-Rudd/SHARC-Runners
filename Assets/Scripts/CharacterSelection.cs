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


    //Unit Testing
    public bool IsChanged = false;
    public bool denied = false;


    //red: 1, yellow: 2 on the array
    //true: 1, false: 0

    private void Start()
    {
        //retrieve locked/unlocked info
        IsUnlockedRed = PlayerPrefs.GetInt("Red");
        IsUnlockedYellow = PlayerPrefs.GetInt("Yellow");

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
            changeBuyBtn(false);
            price.gameObject.SetActive(true);
            priceBG.gameObject.SetActive(true);
        }
        else
        {
            //Set UI elements to false
            changeBuyBtn(true);
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
            if(tokenstext != null)
            {
                tokenstext.text = "Not Enough!";   
                sr.color = Color.red;
            }

            denied = true;

            return;
        }


        //buying process
        if ((selectedCharacter == 1 && IsUnlockedRed == 0) )
        {
            //if red
            Deduct(200);
            

            changeBuyBtn(true);

            PlayerPrefs.SetInt("Red", 1);
            IsUnlockedRed = PlayerPrefs.GetInt("Red");
        }
        else if((selectedCharacter == 2 && IsUnlockedYellow == 0))
        {
            //if yellow
            Deduct(200);
            

            changeBuyBtn(true);
            PlayerPrefs.SetInt("Yellow", 1);
            IsUnlockedYellow = PlayerPrefs.GetInt("Yellow");
        }

    }

    //Storing the player's selection using PlayerPrefs
    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);

    }

    public void changeBuyBtn(bool isBought)
    {
        if (isBought)
        {
            if(select_Btn != null && buy_Btn != null)
            {
                select_Btn.SetActive(true);
                buy_Btn.SetActive(false);
            }
            IsChanged = true;
        }
        else if(!isBought)
        {
            if (select_Btn != null && buy_Btn != null)
            {
                select_Btn.SetActive(false);
                buy_Btn.SetActive(true);
            }
            IsChanged = true;
        }
    }

    public void Deduct(int amount)
    {        
        //take the price away and save it
        tokens = tokens - amount;
        PlayerPrefs.SetInt("Tokens", tokens);

        if (tokenstext != null)
        {
            tokenstext.text = "Your Tokens: " + tokens.ToString();
        }

    }

}


