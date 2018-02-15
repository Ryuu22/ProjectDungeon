using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeTrigger : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.RecieveDamage();
        }
    }
}
