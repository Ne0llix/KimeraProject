using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public bool isPlayerPresent = false;

    public static CurrentSceneManager instance;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de CurrentSceneManager dans la scène");
            return;
        }

        instance = this;
    }
}
