using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPointBehaviour : MonoBehaviour {

    [SerializeField]
    int lifePerSecond;
    [SerializeField]
    float nextActionTime;
    [SerializeField]
    float period;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == ("Player"))
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime += period;
                collision.GetComponent<Player>().RecieveHP(lifePerSecond);
            }

        }
    }
}
