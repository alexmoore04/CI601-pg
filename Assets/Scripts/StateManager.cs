using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public void ChangeSceneByName(string Name)
    {
        SceneManager.LoadScene(Name);
    }
}
