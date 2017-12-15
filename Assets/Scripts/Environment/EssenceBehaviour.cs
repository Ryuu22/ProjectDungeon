using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceBehaviour : MonoBehaviour {

    [SerializeField] private float radius;

    [Header("Game Elements")]
    MoveMaster moveM;

    Transform playerTransform;
    Vector2 playerPos;
    public float speed;

    // Use this for initialization
    void Start ()
    {
            
	}

    // Update is called once per frame
    void Update()
    {
        moveM.Move(this.gameObject, playerPos, speed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
