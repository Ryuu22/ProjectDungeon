using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerBehaviour : MonoBehaviour {

    public bool attacking;
    public float counter;
    public float maxTime;

    // Update is called once per frame

    private void Start()
    {
        counter = 0;
    }
    void Update ()
    {
        Attack();
	}
    void Attack()
    {
        if(attacking)
        {
            if(counter < maxTime)
            {
                counter += Time.deltaTime;
            }
            else
            {
                attacking = false;
                counter = 0;
                this.gameObject.SetActive(false);
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(attacking)
        {
            if (collision.tag == "Slime")
            {
                collision.GetComponent<Slime>().RecieveDamage(30);
            }
        }
    }
}
