using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesBehaviour : MonoBehaviour {

    public Color randomColor;
    public float randomNumber;

    public float minimumTone;
    public float maximunTone;

    float fallingCounter = 3;
    bool falling;

	// Use this for initialization
	void Start ()
    {
        randomNumber = Random.Range(minimumTone, maximunTone);
        this.gameObject.tag = "Wall";
        randomColor = new Color(randomNumber, randomNumber, randomNumber, 255);
        
	}
    private void Update()
    {
        if(falling)
        {
            fallingCounter -= Time.deltaTime;

            if(fallingCounter <= 0)
            {
                StopFalling();
            }
        }
    }
    public void StartFalling()
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        falling = true;
    }
    public void StopFalling()
    {
        this.gameObject.SetActive(false);
    }

}
