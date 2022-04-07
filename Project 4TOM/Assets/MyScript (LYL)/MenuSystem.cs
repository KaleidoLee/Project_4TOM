using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public static MenuSystem MenuInstance;

    [SerializeField] MenuComponent[] menu;

    private void Awake()
    {
        MenuInstance = this;
    }

    public void openMenu(string menuName)
    {
        for (int i = 0; i < menu.Length; i++)
        {
            if (menu[i].menuName == menuName)
            {
                DoOpenMenu(menu[i]);
            }
            else if (menu[i].open)
            {
                DoCloseMenu(menu[i]);
            }
        }
    }

    public void DoOpenMenu(MenuComponent menu)
    {
        menu.Open();
    }

    public void DoCloseMenu(MenuComponent menu)
    {
        menu.Close();
    }

    public void exitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
