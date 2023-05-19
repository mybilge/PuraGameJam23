using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuManager : MonoBehaviour
{
    [SerializeField] List<GameObject> menuList;

    private void Awake() {

        if(PlayerPrefs.HasKey("Username"))
        {
            OpenMenu(menuList[1]);
        }
        else{
            OpenMenu(menuList[0]);
        }
        
    }

    public void OpenMenu(GameObject menu)
    {
        foreach (var item in menuList)
        {
            item.SetActive(false);
        }
        menu.SetActive(true);
    }

    void OpenMenu(GameObject[] menus)
    {
        foreach (var item in menuList)
        {
            item.SetActive(false);
        }

        foreach (var item in menus)
        {
            item.SetActive(true);
        }        
    }

    
}
