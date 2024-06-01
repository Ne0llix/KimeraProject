using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject player;
    public GameObject canvas;
    public GameObject fade;
    public static MenuManager instance;
    public Animator fadeAnimator;
    public GameObject settingWindow;

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

    public void SettingsButton() 
    {
        settingWindow.SetActive(true);
    }

    public void QuitSettingWindow()
    {
        settingWindow.SetActive(false);
    }

    public void MainMenuButton()
    {
        StartCoroutine(LoadMainMenu());
    }

    public IEnumerator LoadMainMenu()
    {
        fade.SetActive(true);
        fadeAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroy();
        SceneManager.LoadScene("MAIN_MENU");
        yield return new WaitForSeconds(1f);
        fade.SetActive(false);
    }
}
