using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Collectable : MonoBehaviour
{
    public MeterScript abilityMeter;
    private int currentcoin;
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

       // MeterScript meter = Instantiate(abilityMeter, meterlocation, Quaternion.identity);
       // meter.transform.SetParent(canvas.transform);

        //Text counter = Instantiate(Counter, meterlocation, Quaternion.identity);
       // counter.transform.SetParent(canvas.transform);


        resetcoin = 0;
        currentcoin = 0;

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable" && !(pMovement.raceFinished))
        {
            
                int viewID = collision.GetComponent<PhotonView>().ViewID;

                PV.RPC("DestroyCrystal", RpcTarget.MasterClient, viewID);

                //Destroy(collision.gameObject);// destroy the object

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
}
