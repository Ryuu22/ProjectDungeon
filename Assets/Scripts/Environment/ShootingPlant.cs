using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlant : MonoBehaviour
{
    [Header("ShootingPlant Fields")]
    [SerializeField]
    GameObject plantShootPrefab;

    [SerializeField]
    bool Right;
    [SerializeField]
    bool Left;
    [SerializeField]
    bool Top;
    [SerializeField]
    bool Bottom;

    [Header("Player Fields")]
    Player player;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
    
    void Shoot()
    {
        if (Right)
        {
            Instantiate(plantShootPrefab);
            plantShootPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0), ForceMode2D.Impulse);
        }
        if (Left)
        {
            plantShootPrefab.transform.position = new Vector2(this.transform.position.x - 0.2f, this.transform.position.y);
            plantShootPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0), ForceMode2D.Impulse);
            Instantiate(plantShootPrefab);
        }
        if (Top)
        {
            Instantiate(plantShootPrefab);
            plantShootPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
        }
        if (Bottom)
        {
            Instantiate(plantShootPrefab);
            plantShootPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1), ForceMode2D.Impulse);
        }
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
