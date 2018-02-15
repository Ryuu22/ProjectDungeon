using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour {

    public bool front;
    MoveMaster moveM;
    Animator myAnim;

    [SerializeField] GameObject particlesObj;
    ParticleSystem blood;

    [Header("Stats")]

    public float timeToLive;

    public float distanceFront;
    public float distanceBack;
    public float speed;

    [Header("BodyParts")]

    public GameObject mainSprite;
    public GameObject[] bodyParts;

    [SerializeField]bool dead;

    float counter;



    [Range(1,3)]public int damage;

	// Use this for initialization
	void Start ()
    {
       front = true;
       moveM = FindObjectOfType<MoveMaster>();
       myAnim = this.GetComponentInChildren<Animator>();

        blood = particlesObj.GetComponent<ParticleSystem>();
       	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!dead)
        {
            myAnim.SetBool("FlyingFront", front);

            if (front)moveM.Move(this.gameObject,new Vector3(this.transform.position.x,this.transform.position.y,distanceFront), speed);
            else moveM.Move(this.gameObject,new Vector3(this.transform.position.x,this.transform.position.y,distanceBack), speed);

            if (this.transform.position.z == distanceFront) front = false;
            if (this.transform.position.z == distanceBack) front = true;
        }
        else
        {
            counter += Time.deltaTime;
            if(counter > timeToLive)
            {
                this.gameObject.SetActive(false);
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("Player") && !dead)
        {
            myAnim.SetTrigger("Attack");
            other.GetComponent<Player>().RecieveDamage(damage);
        }
    }
    public void DieInstantly()
    {
        mainSprite.gameObject.SetActive(false);
        for(int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].gameObject.SetActive(true);
        }
        blood.Play();
        dead = true;

    }
}
