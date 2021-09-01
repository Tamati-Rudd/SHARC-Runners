using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyObject : MonoBehaviour
{
    //Prevent the destruction of the object this script is attached to when a new scene is loaded
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
