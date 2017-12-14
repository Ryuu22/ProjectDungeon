using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBehaviour : MonoBehaviour {

    [SerializeField] float timeToLive;

    [Header("Game Objects")]

    [SerializeField] GameObject graphic;
    [SerializeField] GameObject objCollider;

    //TODO: Get Player and Camera position control.

    private float timeCounter = 0.0f;
    private bool triggered = false;

	// Update is called once per frame
	void Update ()
    {
        if(triggered)
        {
            timeCounter += Time.deltaTime;
            if(timeCounter >= timeToLive)
            {
                graphic.SetActive(false);

            }
            //TODO: Add play animation when ready

        //Freeze player controls
        //Todo Camara focus on animation
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            objCollider.SetActive(true); 
            triggered = true;
        }
    }

}
