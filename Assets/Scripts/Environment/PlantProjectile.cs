using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantProjectile : MonoBehaviour
{
    Vector2 shootPos;
    [SerializeField]
    Vector2 shootDirection;

    void Start ()
    {

    }

    void Update ()
    {
        this.transform.Translate(new Vector3(shootDirection.x, shootDirection.y, 0) * 7 * Time.deltaTime);		
	}

    public void ShotDirection(Vector2 direction)
    {
        shootDirection = direction;
    }
}
