using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityBtn : MonoBehaviour
{
    public PlayerController player;
    public bool activeSpeed;
    public Collectable collectable;

    // Start is called before the first frame update
    public void OnPointerDown()
    {

        //Call set speed to activate boost
        activeSpeed = collectable.SetSpeed();

        if (activeSpeed)
        {
            //Change the player speed
            player.SpeedAbility();

            //Reset player speed
            player.ResetSpeed(activeSpeed);
        }
            
    }
    
}
