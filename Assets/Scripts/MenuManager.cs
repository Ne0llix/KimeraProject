using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuUI;
    public static MenuManager instance;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de MenuManager dans la scène");
            return;
        }
        menuUI.SetActive(false);
        instance = this;
    }

    public void Menu()
    {
        menuUI.SetActive(true);
    }

    public void EndMenu()
    {
        menuUI.SetActive(false);
    }

    public void ResumeButton()
    {
        menuUI.SetActive(false);
    }

    public void OptionsButton() 
    {

    }

    public void MainMenuButton()
    {
        menuUI.SetActive(false);
        SceneManager.LoadScene("SCN_TUTO");
    }
}
