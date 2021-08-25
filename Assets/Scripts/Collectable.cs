using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Collectable : MonoBehaviour
{
    public MeterScript abilityMeter;
    private int currentcoin;
    private int resetcoin = 0;
    private PlayerController pMovement;
    [SerializeField] private Text Counter;//Access the text 
  
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))//Run when an object is tagged Collectable 
        {
            Destroy(collision.gameObject);// destroy the object
            //abilityMeter.SetMaxAbility(maxcoin);
            if (currentcoin < 8)//Run statement if the coins is less then 8
            {
                Increase();
                abilityMeter.SetAbility(currentcoin);//updates the meter bar

                if (currentcoin <= 7)//Run statement if the coins is less then 8
                    Counter.text = currentcoin + "/8";//Print this text
                else
                    SetSpeed();
            }
        }
       
    }

    public bool SetSpeed()
    {
        if (currentcoin >= 8)
        {
            Counter.text = " ";//print nothing
            return true;
        }
        else
        {
            return false;
        }
       
    }

    //This resets the coins meter 
    public void UpdateCoins()
    {
        abilityMeter.SetMaxAbility(resetcoin);
        currentcoin = resetcoin;
        Counter.text = currentcoin + "/8";
    }

    public void Increase()
    {
        currentcoin++; //increases the variable's value by 10
    }
}