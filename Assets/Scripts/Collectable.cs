using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Collectable : MonoBehaviour
{
    public MeterScript abilityMeter;
    private int currentcoin;
    private int resetcoin;
    private PlayerController pMovement;//Access player movement
    public Text Counter;//Access the text 
    private Canvas canvas;
    public PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        //Change the meter location
        Vector2 meterlocation;
        meterlocation.x = 50;
        meterlocation.y = 50;

        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

        resetcoin = 0;
        currentcoin = 0;

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Run if the plaayer collides with the collectable
        if (collision.tag == "Collectable")
        {
            int viewID = collision.GetComponent<PhotonView>().ViewID;
           
            PV.RPC("DestroyCrystal", RpcTarget.MasterClient, viewID);// destroy the object


            if (currentcoin < 8)//Run statement if the coins is less then 8
            {
                Increase();
                abilityMeter.SetAbility(currentcoin);//updates the meter bar

                if (currentcoin <= 7)//Run statement if the coins is less then 8
                    Counter.text = currentcoin + "/8";//Print this text
                else
                    SetSpeed();//change speed
            }

        }
    }

    public bool SetSpeed()
    {
        //If player has collected less or equal to 8
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
        abilityMeter.SetAbility(resetcoin);
        currentcoin = resetcoin;
        Counter.text = currentcoin + "/8";
    }

    public void Increase()
    {
        currentcoin++; //increases the variable's value by 10
    }

    [PunRPC]
    public void DestroyCrystal(int viewID)
    {
            PhotonNetwork.Destroy(PhotonView.Find(viewID).gameObject);
    }
}
