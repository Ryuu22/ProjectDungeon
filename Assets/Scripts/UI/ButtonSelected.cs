using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelected : MonoBehaviour
{
    Animator myAnim;
    public SceneMaster sceneM;
    
    void Start()
    {
        myAnim = GetComponent<Animator>();
        sceneM = GameObject.FindGameObjectWithTag("SceneMaster").GetComponent<SceneMaster>();
    }

    public void NewGame()
    {
        myAnim.SetTrigger("NewGame");
    }

    public void Options()
    {
        myAnim.SetTrigger("Options");
    }

    public void Exit()
    {
        myAnim.SetTrigger("Exit");
    }

    public void Nothing()
    {
        myAnim.SetTrigger("Nothing");
    }

    public void PressedStart()
    {
        sceneM.LoadGameplayScreen();
    }

    public void PressedOptions()
    {
        sceneM.OpenOptionsMenu();
    }

    public void ClosedOptions()
    {
        sceneM.CloseOptionsMenu();
    }

    public void PressedExit()
    {
        sceneM.ExitGame();
    }
}
