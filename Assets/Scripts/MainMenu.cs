using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject player;
    public GameObject canvas;
    public static MainMenu instance;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de MainMenu dans la scène");
            return;
        }
        instance = this;
        Screen.SetResolution(1920, 1080, true);
    }

    public void mainMenu()
    {
        menuUI.SetActive(true);
    }

    public void EndmainMenu()
    {
        menuUI.SetActive(false);
    }

    public void StartButton()
    {
        SceneManager.LoadScene("SCN_TUTO");
        player.SetActive(true);
        canvas.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void OptionsButton()
    {

    }
}
