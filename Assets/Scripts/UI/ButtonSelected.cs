using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelected : MonoBehaviour
{
    [SerializeField]
    Animator myAnim;
    
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

}
