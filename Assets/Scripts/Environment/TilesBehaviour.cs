using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesBehaviour : MonoBehaviour {

    public Color randomColor;
    public float randomNumber;

	// Use this for initialization
	void Start ()
    {
        randomNumber = Random.Range(0.7f, 1.0f);

        randomColor = new Color(randomNumber, randomNumber, randomNumber, 255);

        this.gameObject.GetComponent<SpriteRenderer>().color = randomColor;
        
	}
	

}
