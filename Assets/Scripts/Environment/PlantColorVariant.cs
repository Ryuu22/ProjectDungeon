using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantColorVariant : MonoBehaviour {

    private SpriteRenderer SR;
    private Color lerpedColor;
    [SerializeField] Color colorOne;
    [SerializeField] Color colorTwo;

    [Header("Time")]
    [SerializeField]float time;
    //[SerializeField] Color colorThree;
    // Use this for initialization
    void Start ()
    {
        SR = GetComponentInChildren<SpriteRenderer>();
        lerpedColor = colorOne;
	}
	
	// Update is called once per frame
	void Update ()
    {
        lerpedColor = Color.Lerp(colorOne, colorTwo, Mathf.PingPong(Time.time, time));

        SR.color = lerpedColor;
	}
    
}
