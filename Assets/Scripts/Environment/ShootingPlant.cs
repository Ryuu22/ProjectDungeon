using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlant : MonoBehaviour
{
    [Header("ShootingPlant Fields")]
    Vector2 shootedDirection;
    [SerializeField]
    GameObject plantShootPrefab;

    [Header("Player Fields")]
    Player player;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
    
    void Shoot()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == ("Arrow"))
        {
            Debug.Log("AH");
            Shoot();
        }
    }
}
