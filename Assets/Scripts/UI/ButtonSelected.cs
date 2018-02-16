using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelected : MonoBehaviour
{
    Animator myAnim;
    public SceneMaster sceneM;
    AudioPlayer audioP;
    
    void Start()
    {
        myAnim = GetComponent<Animator>();
        sceneM = GameObject.FindGameObjectWithTag("SceneMaster").GetComponent<SceneMaster>();
        audioP = GameObject.FindObjectOfType<AudioPlayer>();
    }

    public void NewGame()
    {
        myAnim.SetTrigger("NewGame");
        audioP.Play2DSFX(0);
    }

    public void Options()
    {
        myAnim.SetTrigger("Options");
        audioP.Play2DSFX(0);
    }

    public void Exit()
    {
        myAnim.SetTrigger("Exit");
        audioP.Play2DSFX(0);
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
