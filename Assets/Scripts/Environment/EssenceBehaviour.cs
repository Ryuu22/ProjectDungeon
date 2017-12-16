using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceBehaviour : MonoBehaviour {

    [SerializeField] private float radius;

    [Header("Game Elements")]
    MoveMaster moveM;

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

        moveM.Move(this.gameObject, playerPos, speed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
