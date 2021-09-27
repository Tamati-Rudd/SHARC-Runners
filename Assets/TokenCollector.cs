using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TokenCollector : MonoBehaviour
{
    int token;
    CharacterLib clib = new CharacterLib();

    // Start is called before the first frame update
    void Start()
    {        
        
        token = PlayerPrefs.GetInt("token");

        if (PlayerPrefs.GetInt("done") == 0)
        {
            PlayerPrefs.SetInt("done", 0);
        }

        if (PlayerPrefs.GetInt("done") == 0)
        {
            PlayerPrefs.SetInt("0", 0);
            PlayerPrefs.SetInt("1", 1);
            PlayerPrefs.SetInt("2", 1);
            PlayerPrefs.SetInt("done", 1);

        }



    }

    void Unlock()
    {

    }

    void Save()
    {

    }


}
