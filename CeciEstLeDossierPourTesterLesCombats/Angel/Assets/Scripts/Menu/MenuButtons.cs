using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject multiplayerMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject soloMenu;

    public void EnableMultiplayerMenu()
    {
        mainMenu.SetActive(false);
        multiplayerMenu.SetActive(true);
    }

    public void BackFromMultiplayer()
    {
        multiplayerMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void EnableSoloMenu()
    {
        mainMenu.SetActive(false);
        soloMenu.SetActive(true);
    }

    public void BackFromSolo()
    {
        soloMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
