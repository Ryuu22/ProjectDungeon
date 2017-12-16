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
    GameObject endingPanel;
    [SerializeField]
    Image logo;
    [SerializeField]
    Text pressAnyButton;
    float anyButtonCounter;
    bool started;
    [SerializeField]
    Animator myAnim;
    public float logoFade;
    public float fade;
    public bool fadeOut = false;
    public bool titleScreen = false;
    Player player;

    public float counter;
    float alphaCounter;

    private void Start()
    {
        counter = 0;
        fade = 1;
        blackScreen.color = new Color(0, 0, 0, fade);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        anyButtonCounter += Time.deltaTime;

        if (anyButtonCounter >= 0.5f && !started && pressAnyButton != null)
        {
            pressAnyButton.color = new Color(255, 255, 255, 255);

            if(anyButtonCounter >= 1)
            {
                pressAnyButton.color = new Color(255, 255, 255, 0);
                anyButtonCounter = 0;
            }
        }

        if(Input.anyKeyDown && myAnim != null)
        {
            pressAnyButton.color = new Color(255, 255, 255, 0);
            started = true;
            myAnim.SetTrigger("Start");
        }

        counter += Time.deltaTime;

        if(counter >= 2 && logo != null)
        {
            if(logoFade < 1) logoFade += Time.deltaTime / 2;

            if(counter >= 8) FadeOut(false);

            logo.color = new Color(255, 255, 255, logoFade);
        }

        if (fade > 0 && !fadeOut) fade -= Time.deltaTime / 3;

        blackScreen.color = new Color(0, 0, 0, fade);

        if(fadeOut)
        {
            logoFade -= Time.deltaTime;

            if(fade < 1)
            {
                fade += Time.deltaTime / 2;
            }

            blackScreen.color = new Color(0, 0, 0, fade);

            if(fade >= 1 && !titleScreen)
            {
                LoadTitleScreen();
            }

            if(fade >= 1 && titleScreen)
            {
                LoadGameplayScreen();
            }
        }

        if(player != null)
        {
            if(player.IsDead)
            {
                EndingPanel(false);
            }
        }

    }

    public void EndingPanel(bool victory)
    {
        if (victory)
        {
            endingPanel.SetActive(true);
            GameObject.FindGameObjectWithTag("EndingText").GetComponent<Text>().text = "victory";
            GameObject.FindGameObjectWithTag("EndingText").GetComponent<Text>().color = new Color(0, 255, 0);
        }
        else
        {
            endingPanel.SetActive(true);
            GameObject.FindGameObjectWithTag("EndingText").GetComponent<Text>().text = "defeat";
            GameObject.FindGameObjectWithTag("EndingText").GetComponent<Text>().color = new Color(255, 0, 0);
        }
    }

    public void FadeOut(bool isTitleScreen)
    {
        fadeOut = true;
        titleScreen = isTitleScreen;
    }

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

    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void OpenMenu(GameObject menu)
    {
        menu.gameObject.SetActive(true);
    }

    public void CloseMenu(GameObject menu)
    {
        menu.gameObject.SetActive(false);
    }

    public void CloseOptionsMenu(Animator menu)
    {
        menu.SetTrigger("Close");
    }
}
