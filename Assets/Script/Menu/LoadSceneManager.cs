using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SceneName
{
    Menu,
    Game,
    MemoryGame,
    Quit
}

public class LoadSceneManager : Singleton<LoadSceneManager>
{
    public void LoadScene(SceneName sceneName)
    {
        if ((sceneName == SceneName.Quit))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene((int)sceneName);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
