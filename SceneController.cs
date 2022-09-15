using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public void LoadGameScene()
    {
        SceneManager.LoadScene((int)Scenes.LoadScene);
    }    

    public void EnterDream()
    {
        SceneManager.LoadScene((int)Scenes.DreamScene);
    }

    public void EnterNightmare()
    {
        SceneManager.LoadScene((int)Scenes.NightmareScene);
    }

    public void GameComplete()
    {
        SceneManager.LoadScene((int)Scenes.WinningScene);
    }

    public void GameOver()
    {
        SceneManager.LoadScene((int)Scenes.GameOverScene);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene((int)Scenes.MainMenuScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

public enum Scenes
{
    MainMenuScene,
    LoadScene,
    DreamScene,
    NightmareScene,
    GameOverScene,
    WinningScene
}
