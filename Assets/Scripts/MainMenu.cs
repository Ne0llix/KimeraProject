using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject fade;
    public GameObject settingWindow;
    public GameObject quitButtonSettingWindow;

    public static MainMenu instance;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de MainMenu dans la scène");
            return;
        }
        instance = this;
    }

    public void StartButton()
    {
        SceneManager.LoadScene("SCN_TUTO");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void SettingButton()
    {
        settingWindow.SetActive(true);
    }

    public void QuitSettingWindow()
    {
        settingWindow.SetActive(false);
    }
}
