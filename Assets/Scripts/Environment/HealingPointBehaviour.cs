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

    Player player;

    bool playerInside = false;

    private void Update()
    {
        if (Time.time > nextActionTime && playerInside)
        {
            nextActionTime += period;
            player.RecieveHP(lifePerSecond);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == ("Player"))
        {
            player = collision.GetComponent<Player>();
            playerInside = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == ("Player"))
        {

            playerInside = false;

        }
    }

}
