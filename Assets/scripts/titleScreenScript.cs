using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleScreenScript : MonoBehaviour
{
    public void loadLevelSelectScene()
    {
        SceneManager.LoadScene("levelSelect");
    }
    public void loadOptionsScene()
    {
        SceneManager.LoadScene("Options");
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
