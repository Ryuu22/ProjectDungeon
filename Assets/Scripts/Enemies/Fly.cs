using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour {

    private bool front;
    MoveMaster moveM;
    Animator myAnim;
    public AudioPlayer audioP;

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
    float timer;



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
            timer++;
            myAnim.SetBool("FlyingFront", front);

            if (front)moveM.Move(this.gameObject,new Vector3(this.transform.position.x,this.transform.position.y,distanceFront), speed);
            else moveM.Move(this.gameObject,new Vector3(this.transform.position.x,this.transform.position.y,distanceBack), speed);

            if (this.transform.position.z == distanceFront) front = false;
            if (this.transform.position.z == distanceBack) front = true;

            if(timer%30 == 1) audioP.PlaySFX(4, 1, Random.Range(0.9f, 1.1f));
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
            other.GetComponent<Player>().RecieveDamage();
        }
    }
    public void DieInstantly()
    {
        mainSprite.gameObject.SetActive(false);
        for(int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].gameObject.SetActive(true);
        }
        audioP.Play2DSFX(5);
        blood.Play();
        dead = true;

    }
}
