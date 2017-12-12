using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlant : MonoBehaviour
{
    [Header("ShootingPlant Fields")]
    [SerializeField]
    GameObject plantShootPrefab;
    PlantProjectile projectileScript;

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
        projectileScript = plantShootPrefab.GetComponent<PlantProjectile>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
    
    void Shoot()
    {
        if (Right)
        {
            GameObject plantShoot = plantShootPrefab;
            PlantProjectile plantProjectile = projectileScript;

            plantShoot.transform.position = new Vector2(this.transform.position.x + 0.2f, this.transform.position.y);
            plantProjectile.ShotDirection(new Vector2(1, 0));
            Instantiate(plantShoot);
        }
        if (Left)
        {
            GameObject plantShoot = plantShootPrefab;
            PlantProjectile plantProjectile = projectileScript;

            plantShoot.transform.position = new Vector2(this.transform.position.x - 0.2f, this.transform.position.y);
            plantProjectile.ShotDirection(new Vector2(-1, 0));
            Instantiate(plantShoot);
        }
        if (Top)
        {
            GameObject plantShoot = plantShootPrefab;
            PlantProjectile plantProjectile = projectileScript;

            plantShoot.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 1);
            plantProjectile.ShotDirection(new Vector2(0, 1));
            Instantiate(plantShoot);
        }
        if (Bottom)
        {
            GameObject plantShoot = plantShootPrefab;
            PlantProjectile plantProjectile = projectileScript;

            plantShoot.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 1);
            plantProjectile.ShotDirection(new Vector2(0, -1));
            Instantiate(plantShoot);
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
