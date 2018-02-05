using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMaster : MonoBehaviour
{
    [SerializeField]
    Image blackScreen;
    [SerializeField]
    GameObject optionsPanel;

    float alphaBlackScreen;
    bool fadeToBlack;

    float logoCounter;
    float titleCounter;

    CurrentScene currentScene;
    enum CurrentScene
    {
        Logo,
        Title,
        Gameplay,
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        currentScene = CurrentScene.Logo;
        alphaBlackScreen = 1;
        logoCounter = 8;
    }

    private void Update()
    {
        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alphaBlackScreen);

        if(Input.GetKey(KeyCode.LeftControl))
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                LoadLogoScreen();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                LoadTitleScreen();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                LoadGameplayScreen();
            }
        }

        if(!fadeToBlack)
        {
            if(alphaBlackScreen > 0)
            {
                alphaBlackScreen -= Time.deltaTime/3;
            }
            else
            {
                alphaBlackScreen = 0;
            }
        }
        else
        {
            if(alphaBlackScreen < 1)
            {
                alphaBlackScreen += Time.deltaTime / 3;
            }
            else
            {
                alphaBlackScreen = 1;
            }
        }

        switch(currentScene)
        {
            case CurrentScene.Logo:

                logoCounter -= Time.deltaTime;

                if(logoCounter <= 3)
                {
                    fadeToBlack = true;
                }
                if(logoCounter <= 0)
                {                    
                    LoadTitleScreen();
                }

                break;

            case CurrentScene.Title:

                titleCounter += Time.deltaTime;

                if(titleCounter >= 3)
                {
                    GameObject.Find("TitleCanvas").GetComponent<Animator>().SetTrigger("Start");
                }

                break;

            case CurrentScene.Gameplay:

                break;

            default:
                break;
        }

    }

    #region OPTIONS MENU METHODS

    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void OpenCloseMenu(GameObject menu)
    {
        if(menu.activeInHierarchy)
        {
            menu.gameObject.SetActive(false);
        }
        else
        {
            menu.gameObject.SetActive(true);
        }
    }

    public void CloseOptionsMenu(Animator menu)
    {
        menu.SetTrigger("Close");
    }

    #endregion

    #region SCENE MANAGEMENT METHODS

    public void LoadLogoScreen()
    {
        currentScene = CurrentScene.Logo;
        fadeToBlack = false;
        alphaBlackScreen = 1;
        logoCounter = 8;
        SceneManager.LoadScene(0);
    }

    public void LoadTitleScreen()
    {
        currentScene = CurrentScene.Title;
        fadeToBlack = false;
        alphaBlackScreen = 1;
        SceneManager.LoadScene(1);
    }

    public void LoadGameplayScreen()
    {
        currentScene = CurrentScene.Gameplay;
        fadeToBlack = false;
        alphaBlackScreen = 1;
        SceneManager.LoadScene(2);
    }

    #endregion
}
