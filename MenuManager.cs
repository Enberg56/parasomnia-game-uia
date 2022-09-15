using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
            if (pauseMenu.activeInHierarchy)
            {
                Time.timeScale = 0;
            } else
            {
                Time.timeScale = 1;
            }
        }
    }
        public void Resume()
        {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        }
    
        public void Exit()
        {
        Time.timeScale = 1;
        SceneManager.LoadScene((int)Scenes.MainMenuScene);    
        }
 
}
