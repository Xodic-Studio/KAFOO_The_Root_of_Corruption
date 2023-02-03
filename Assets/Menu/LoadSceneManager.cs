using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SceneName
{
    Menu,
    Game
}

public class LoadSceneManager : Singleton<LoadSceneManager>
{
    public void LoadScene(SceneName sceneName)
    {
        SceneManager.LoadScene((int)sceneName);
    }
}
