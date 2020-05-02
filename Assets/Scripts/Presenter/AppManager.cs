using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
    }
}
