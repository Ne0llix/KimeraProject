using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Damages : MonoBehaviour
{
    public string sceneName;
    [SerializeField] int PV = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Ennemy"))
        {
            PV -= 1;
        }
        if (PV <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
