using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimatorEvents : MonoBehaviour
{
    
    GameObject bossAttackTrigger;

    void Start ()
    {
        bossAttackTrigger = GameObject.Find("BossAttackTrigger");
        bossAttackTrigger.SetActive(false);
    }

    void ActiveAttackTrigger()
    {
        bossAttackTrigger.SetActive(true);
    }

    void DesactiveAttackTrigger()
    {
        bossAttackTrigger.SetActive(false);
    }

    void DesactiveBoss()
    {
        this.gameObject.SetActive(false);
    }
}
