using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public void LoadLogoScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameplayScreen()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {

    }

    public void OpenMenuAnimation(GameObject menu)
    {
        menu.gameObject.SetActive(true);
    }

    public void OpenMenu(GameObject menu)
    {
        menu.gameObject.SetActive(true);
    }

    public void CloseMenu(GameObject menu)
    {
        menu.gameObject.SetActive(false);
    }
}
