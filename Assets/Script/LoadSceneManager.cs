using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SceneName
{
    Menu,
    Selection,
    RabbitPress,
    VampireSurvival,
    MemoryGame,
    PantherShooter,
    EndScene,
    Credit,
    Quit,
}

public class LoadSceneManager : Singleton<LoadSceneManager>
{
    public bool loadingScene = false;
    public bool finishedLoading = true;
    private int nextSceneNumber = 0;
    public void LoadScene(SceneName sceneName)
    {
        loadingScene = true;
        finishedLoading = false;
        if (sceneName == SceneName.Quit)
        {
            Debug.Log("Quit");
            Application.Quit();
        }
        else
        {
            nextSceneNumber = (int)sceneName;
            Invoke(nameof(WaifBeforeLoad), 2f);
        }
    }

    public void WaifBeforeLoad()
    {
        SceneManager.LoadScene((nextSceneNumber));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
