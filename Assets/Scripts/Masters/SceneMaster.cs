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
    Image logo;
    public float logoFade;
    public float fade;
    public bool fadeOut = false;
    public bool titleScreen = false;

    public float counter;
    float alphaCounter;

    private void Start()
    {
        counter = 0;
        fade = 1;
        blackScreen.color = new Color(0, 0, 0, fade);
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if(counter >= 3 && logo != null)
        {
            if(logoFade < 1) logoFade += Time.deltaTime / 3;

            if(counter >= 8) FadeOut(false);

            logo.color = new Color(0, 0, 0, logoFade);
        }

        if (fade > 0 && !fadeOut) fade -= Time.deltaTime / 3;

        blackScreen.color = new Color(0, 0, 0, fade);

        if(fadeOut)
        {

            if(fade < 1) fade += Time.deltaTime / 2;

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
