using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerBehaviour : MonoBehaviour
{
    float counter = 0.4f;
    int damage;

    private void Start()
    {
        counter = 0;
        damage = GetComponentInParent<Player>().Damage;
    }

    void Update ()
    {
        counter -= Time.deltaTime;

        if (counter <= 0)
        {
            counter = 0.4f;
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider collision)
    {

        if(collision.gameObject.tag == ("Slime"))
        {
            collision.gameObject.GetComponent<Slime>().RecieveDamage();
        }
        if(collision.gameObject.tag == ("Fly"))
        {
            collision.gameObject.GetComponent<Fly>().DieInstantly();
        }
        if (collision.gameObject.tag == ("Destructible"))
        {
            collision.gameObject.GetComponent<DestructibleBehaviour>().GetDestroyed();
        }
        if (collision.gameObject.tag == ("BossSlime"))
        {
            collision.gameObject.GetComponent<BossSlime>().RecieveDamage(damage);
        }
    }

}
