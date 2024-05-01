using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunGame : MonoBehaviour
{
    public void playVersion1()
    {
        SceneManager.LoadScene("Version1");
    }

    public void playVersion2()
    {
        SceneManager.LoadScene("Version2");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
