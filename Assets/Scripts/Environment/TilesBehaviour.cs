using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesBehaviour : MonoBehaviour {

    public Color randomColor;
    public float randomNumber;

    public float minimumTone;
    public float maximunTone;

	// Use this for initialization
	void Start ()
    {
        randomNumber = Random.Range(minimumTone, maximunTone);

        randomColor = new Color(randomNumber, randomNumber, randomNumber, 255);

        this.gameObject.GetComponent<SpriteRenderer>().color = randomColor;
        
	}
	

}
