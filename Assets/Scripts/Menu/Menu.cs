using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the script of for the individual menu gameobjects
public class Menu : MonoBehaviour
{

    public string menuName;
    public bool open;

    //sets certain menu to open
    public void Open()
    {
        open = true;
        gameObject.SetActive(true);
    }

    //sets certain menu to close
    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }
}
