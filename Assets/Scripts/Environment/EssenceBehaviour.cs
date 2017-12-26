using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceBehaviour : MonoBehaviour {

    [SerializeField] private float radius;

    [Header("Game Elements")]
    MoveMaster moveM;

    [SerializeField] int essenceValue;
    [SerializeField] bool playerDetected;

    public Transform playerTransform;
    public Vector2 playerPos;
    public float speed;

    // Use this for initialization
    void Start ()
    {

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        moveM = GameObject.FindGameObjectWithTag("MoveMaster").GetComponent<MoveMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = playerTransform.position;

        if (Vector2.Distance(playerPos, this.transform.position) < radius && !playerDetected)
        {
            this.GetComponent<AudioSource>().Play();
            playerDetected = true;
        }

            if (playerDetected)
        {
            moveM.Move(this.gameObject, playerPos, speed);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().ReceiveEssences(essenceValue);
            Destroy(this.gameObject);
        }
        


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
