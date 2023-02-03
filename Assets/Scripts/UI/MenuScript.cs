using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private int nextSceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextSceneToLoad);
    }
    public void SettingsLevel()
    {
        // реализовать настройки
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
