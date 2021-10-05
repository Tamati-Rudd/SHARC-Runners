using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEditor;
using Photon.Pun;
using Photon.Chat;

public class ShairylTests
{ 
   
    //This Test checks to see if a the buy/select button changes when something is bought
    //Needed so when the player buys a character, he can use it in game
    [Test]
    public void testButtonChange()
    {
        //setting up the characterSelection script
        var go = new GameObject();
        go.AddComponent<CharacterSelection>();
        CharacterSelection script = go.GetComponent<CharacterSelection>();

        //simulating buying a character
        script.changeBuyBtn(true);

        bool expected = true;

        bool actual = script.IsChanged;

        Assert.AreEqual(expected, actual);

    }

    //This Test checks to see if the tokens are deducted once a character is bought
    //Needed so when the player buys a character, they cannot abuse the game and buy as many as they want

    [Test]
    public void testMoneyDeduction()
    {
        //setting up the characterSelection script
        var go = new GameObject();
        go.AddComponent<CharacterSelection>();
        CharacterSelection script = go.GetComponent<CharacterSelection>();

        //setting up base tokens for testing
        PlayerPrefs.SetInt("Tokens", 200);        

        script.Deduct(200);

        //-200 because the Deduct function also sets the Playerpref
        int expected = -200;

        int actual = PlayerPrefs.GetInt("Tokens");

        Assert.AreEqual(expected, actual);

    }


    //This Test checks to see if the player is blocked from buying a character if he doesn't have the required tokens
    //Needed so when the player tries to buy a character, they should have the required funds to do so
    [Test]
    public void testBuyDenial()
    {
        //setting up the characterSelection script
        var go = new GameObject();
        go.AddComponent<CharacterSelection>();
        CharacterSelection script = go.GetComponent<CharacterSelection>();

        //setting insufficient tokens
        PlayerPrefs.SetInt("Tokens", 0);

        //simulate buying
        script.buy();

        bool expected = true;

        bool actual = script.denied;

        Assert.AreEqual(expected, actual);
    }



}
