using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//This script will be used for changing the scene. So far it is just used by the main menu. There will be a different script for in-game UI

//This script will be expanded upon later with the options menu and level select later. 

public class mainMenu : MonoBehaviour {
    public GameObject mainMenuPanel;
    public GameObject creditsPanel;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //goes to the next level. Since this runs of the main menu with a build index of 0 this goes to the 1st level; 
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void showCredits() {
        if (mainMenuPanel != null && creditsPanel != null)
        {
            mainMenuPanel.SetActive(false);
            creditsPanel.SetActive(true);
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        }
    }
    public void showMainMenu() {
        if (mainMenuPanel != null && creditsPanel != null)
        {
            mainMenuPanel.SetActive(true);
            creditsPanel.SetActive(false);
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
