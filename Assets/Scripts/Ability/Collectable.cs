using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Collectable : MonoBehaviour
{
    public MeterScript abilityMeter;
    public int currentcoin;
    private int resetcoin = 0;
    private PlayerController pMovement;
    public Text Counter;//Access the text 
    private Canvas canvas;
    public PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        pMovement = PV.GetComponent<PlayerController>();
    }

    void Start()
    {
        Vector2 meterlocation;
        meterlocation.x = 50;
        meterlocation.y = 50;

        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

        resetcoin = 0;
        currentcoin = 0;

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable" && !(pMovement.raceFinished))
        {
            
                int viewID = collision.GetComponent<PhotonView>().ViewID;

                PV.RPC("DestroyCrystal", RpcTarget.MasterClient, viewID);//destroy the object

                if (currentcoin < 8)//Run statement if the coins is less then 8
                {
                    Increase();
                    abilityMeter.SetAbility(currentcoin);//updates the meter bar

                    if (currentcoin <= 7)//Run statement if the coins is less then 8
                        Counter.text = currentcoin + "/8";//Print this text
                    else
                        SetSpeed(false);
                }
        }
    }

    public bool SetSpeed(bool testing)
    {
        if (currentcoin >= 8 && testing)
        {
            return true;
        }
        else if(currentcoin >= 8 )
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
        while (PhotonView.Find(viewID) != null)
        {
            PhotonNetwork.Destroy(PhotonView.Find(viewID));
        }          
    }

    //Unit Testing
    public bool CollectCrystal(int n)
    {
        //Reset the current crystal
        currentcoin = 0;
        
        //create a loop variable
        int loop = 0;

        //mimic if the playerr has collected a crystal
        for (; loop < n; loop++)
        {
            Increase();
        }

        //if the player has collected 8 or more than crystals
        if (loop <= 8)
        {
            return true;
        }

        //if the player didnt collect any crystals
        else
            return false;
    }
}
