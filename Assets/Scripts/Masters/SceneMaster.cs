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

    public Text pressAnyButton;
    float pressAnyButtonAlpha;
    bool alphaFilled;

    bool findController;

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
                alphaBlackScreen -= Time.deltaTime / 3;
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

                if(!findController)
                {
                    pressAnyButton = GameObject.Find("PressAnyButton").GetComponent<Text>();
                    pressAnyButton.gameObject.SetActive(false);
                    findController = true;
                }

                titleCounter += Time.deltaTime;
                pressAnyButton.color = new Color(pressAnyButton.color.r, pressAnyButton.color.g, pressAnyButton.color.b, pressAnyButtonAlpha);

                if(pressAnyButtonAlpha < 1 && !alphaFilled)
                {
                    pressAnyButtonAlpha += Random.Range(Time.deltaTime, Time.deltaTime / 3);

                    if(pressAnyButtonAlpha >= 1)
                    {
                        alphaFilled = true;
                    }
                }
                if (pressAnyButtonAlpha > 0 && alphaFilled)
                {
                    pressAnyButtonAlpha -= Random.Range(Time.deltaTime, Time.deltaTime / 3);

                    if (pressAnyButtonAlpha <= 0)
                    {
                        alphaFilled = false;
                    }
                }

                if(titleCounter >= 3)
                {
                    pressAnyButton.gameObject.SetActive(true);

                    if(Input.anyKey)
                    {
                        GameObject.Find("TitleCanvas").GetComponent<Animator>().SetTrigger("Start");
                        pressAnyButton.gameObject.SetActive(false);
                    }
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

    public void OpenOptionsMenu()
    {
        if(!optionsPanel.activeInHierarchy)
        {
            optionsPanel.gameObject.SetActive(true);
        }
    }

    public void CloseOptionsMenu()
    {
        optionsPanel.GetComponent<Animator>().SetTrigger("Close");
    }

    public void ExitGame()
    {

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
