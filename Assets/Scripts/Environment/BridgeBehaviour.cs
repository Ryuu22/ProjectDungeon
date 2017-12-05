using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBehaviour : MonoBehaviour {

    [SerializeField] Transform[] bridgeTiles;
    [SerializeField] GameObject player;

    [SerializeField] float fallCounter;
    [SerializeField] float fallTime;
    int tileIndex;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            MakeTileFall(tileIndex);
        }
    }
    void MakeTileFall(int i)
    {
        fallCounter -= 0.1f;
        if(fallCounter > 5.0f)
        {

        }
    }
}
