using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour {

    public bool front;
    MoveMaster moveM;
    Animator myAnim;

    public float distanceFront;
    public float distanceBack;
    public float speed;

	// Use this for initialization
	void Start ()
    {
       front = true;
       moveM = FindObjectOfType<MoveMaster>();
        myAnim = this.GetComponentInChildren<Animator>();

       	
	}
	
	// Update is called once per frame
	void Update ()
    {
        myAnim.SetBool("FlyingFront", front);

        if (front)moveM.Move(this.gameObject,new Vector3(this.transform.position.x,this.transform.position.y,distanceFront), speed);
        else moveM.Move(this.gameObject,new Vector3(this.transform.position.x,this.transform.position.y,distanceBack), speed);

        if (this.transform.position.z == distanceFront) front = false;
        if (this.transform.position.z == distanceBack) front = true;
    }
}
