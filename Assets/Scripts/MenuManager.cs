using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    //making this class a singleton
    public static MenuManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] Menu[] menus;

    //takes a string to open a menu
    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                menus[i].Open();
            }
            else if (menus[i].open)
            {
                Closemenu(menus[i]);
            }
        }
    }

    //takes a menu object to open a menu
    public void Openmenu(Menu menu)
    {

        //make sure only one menu is open at a time
        for (int i = 0; i < menus.Length; i++)
        {

            if (menus[i].open)
            {
                Closemenu(menus[i]);
            }
        }

        menu.Open();
    }

    public void Closemenu(Menu menu)
    {
        menu.Close();
    }

}
