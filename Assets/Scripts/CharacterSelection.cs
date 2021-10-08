using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//This Script manages the character selection in the menu
public class CharacterSelection : MonoBehaviour
{
    //store all the characters in an array   
    public GameObject[] characters; //red: 1, yellow: 2 on the array
    public int SelectedCharacter = 0;

    //Storing Buttons
    public GameObject select_Btn;
    public GameObject buy_Btn;

    //stores whether a character is locked or not
    //true: 1, false: 0
    int IsUnlockedRed;
    int IsUnlockedYellow;
    int IsUnlockedGreen;

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

    //Storing tokens in a local variable
    public int tokens;
    SpriteRenderer sr;

    //Character Prices
    int RedPrice = 200;
    int YellowPrice = 200;
    
    //Unit Testing
    public bool IsChanged = false;
    public bool denied = false;

    private void Start()
    {
        //retrieve locked/unlocked info
        IsUnlockedRed = PlayerPrefs.GetInt("Red");
        IsUnlockedYellow = PlayerPrefs.GetInt("Yellow");
        IsUnlockedGreen = PlayerPrefs.GetInt("Green");

        //chagne colour of UI element
        sr = notEnoughBG.GetComponent<SpriteRenderer>();
        sr.color = Color.blue;

        //present token
        tokens = PlayerPrefs.GetInt("Tokens");
        tokenstext.text = "Your Tokens: " + tokens.ToString();
    }

    void Update()
    {
        //Checking the current character selected and checking whether it is unlocked or not
        switch (SelectedCharacter)
        {
            //If red is selected
            case 0:
                ChangeBuyBtn(true);
                price.gameObject.SetActive(false);
                priceBG.gameObject.SetActive(false);
                break;

            case 1:

                if (IsUnlockedRed == 0)
                {
                    //IsUnlockedRed = PlayerPrefs.GetInt("Red");
                   // IsUnlockedYellow = PlayerPrefs.GetInt("Yellow");

                    //Set UI elements to active
                    ChangeBuyBtn(false);
                    price.gameObject.SetActive(true);
                    priceBG.gameObject.SetActive(true);
                    break;
                }
                else
                {
                    ChangeBuyBtn(true);
                    price.gameObject.SetActive(false);
                    priceBG.gameObject.SetActive(false);
                    break;
                }
                

            //If yellow is selected
            case 2:
                if (IsUnlockedYellow == 0)
                {
                  //  IsUnlockedRed = PlayerPrefs.GetInt("Red");
                  //  IsUnlockedYellow = PlayerPrefs.GetInt("Yellow");

                    //Set UI elements to active
                    ChangeBuyBtn(false);
                    price.gameObject.SetActive(true);
                    priceBG.gameObject.SetActive(true);
                    break;
                }
                else
                {
                    ChangeBuyBtn(true);
                    price.gameObject.SetActive(false);
                    priceBG.gameObject.SetActive(false);
                    break;
                }

            case 3:
                if (IsUnlockedGreen == 0)
                {
                    //  IsUnlockedRed = PlayerPrefs.GetInt("Red");
                    //  IsUnlockedYellow = PlayerPrefs.GetInt("Yellow");

                    //Set UI elements to active
                    ChangeBuyBtn(false);
                    price.gameObject.SetActive(true);
                    priceBG.gameObject.SetActive(true);
                    break;
                }
                else
                {
                    ChangeBuyBtn(true);
                    price.gameObject.SetActive(false);
                    priceBG.gameObject.SetActive(false);
                    break;
                }

        }

    }
    
    //use this fuction when the next button is pressed
    public void NextCharacter()
    {
        //algorithm for cycling through the array and presenting the character
        characters[SelectedCharacter].SetActive(false);

        //Setting UI elements
        tokenstext.text = "Your Tokens: " + tokens.ToString();
        sr.color = Color.blue;

        //update the selected character
        SelectedCharacter = (SelectedCharacter + 1) % characters.Length;

        characters[SelectedCharacter].SetActive(true);
       
   }

    public void PreviousCharacter()
    {
        //algorithm for cycling through the array and presenting the character
        characters[SelectedCharacter].SetActive(false);

        //Setting UI elements
        tokenstext.text = "Your Tokens: " + tokens.ToString();
        sr.color = Color.blue;

        //update the selected character
        SelectedCharacter = SelectedCharacter - 1;
        
        if(SelectedCharacter < 0)
        {
            SelectedCharacter += characters.Length;
        }
        characters[SelectedCharacter].SetActive(true);
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

        IsUnlockedRed = PlayerPrefs.GetInt("Red");
        IsUnlockedYellow = PlayerPrefs.GetInt("Yellow");
        IsUnlockedGreen = PlayerPrefs.GetInt("Green");

        //Checking the current character selected and checking whether it is unlocked or not
        switch (SelectedCharacter)
        {

            //If red is selected
            case 1:

                if (IsUnlockedRed == 0)
                {
                    //if red
                    Deduct(RedPrice);

                    //change button from buy to select
                    ChangeBuyBtn(true);

                    PlayerPrefs.SetInt("Red", 1);
                    IsUnlockedRed = PlayerPrefs.GetInt("Red");
                    
                }

                break;

            //If yellow is selected
            case 2:
                if (IsUnlockedYellow == 0)
                {
                    //if yellow
                    Deduct(YellowPrice);

                    ChangeBuyBtn(true);

                    PlayerPrefs.SetInt("Yellow", 1);
                    IsUnlockedYellow = PlayerPrefs.GetInt("Yellow");
                    
                }
                break;
            case 3:
                if (IsUnlockedGreen == 0)
                {
                    //if yellow
                    Deduct(YellowPrice);

                    ChangeBuyBtn(true);

                    PlayerPrefs.SetInt("Green", 1);
                    IsUnlockedGreen = PlayerPrefs.GetInt("Green");

                }
                break;
        }
    }

    //Storing the player's selection using PlayerPrefs
    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharacter", SelectedCharacter);

    }

    //This function changes the buy button from select to buy or vice-versa
    public void ChangeBuyBtn(bool isBought)
    {
        //check if the character is bought
        if (isBought)
        {
            if(select_Btn != null && buy_Btn != null)
            {
                //make the select button visible while the latter false
                select_Btn.SetActive(true);
                buy_Btn.SetActive(false);
            }
            IsChanged = true;
        }

        //check if the character is not bought
        else if(!isBought)
        {
            if (select_Btn != null && buy_Btn != null)
            {
                //make the select button visible while the latter false
                select_Btn.SetActive(false);
                buy_Btn.SetActive(true);
            }
            IsChanged = true;
        }
    }

    //This function deducts amount from the tokens in the system
    public void Deduct(int amount)
    {        
        //take the price away and save it
        tokens = tokens - amount;
        PlayerPrefs.SetInt("Tokens", tokens);

        //Shwo the updated token on the screen
        if (tokenstext != null)
        {
            tokenstext.text = "Your Tokens: " + tokens.ToString();
        }

    }

}


