using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject settingsPanel;
    public void StartGame()
    {
        //Changes scene to the first scene in the build settings
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        //Quits the Unity app
        Application.Quit();
    }

    public void OpenSettings()
    {
        //SetActive(true) makes the object visible and interactable
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        //SetActive(false) makes the object invisible and uninteractable
        settingsPanel.SetActive(false);
    }
}
