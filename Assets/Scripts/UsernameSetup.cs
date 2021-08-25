using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UsernameSetup : MonoBehaviour
{
    public TMPro.TMP_InputField username;

    public void Setup()
    {
        PlayerPrefs.SetString("username", username.text);
        
    }
}
