using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpite : MonoBehaviour
{
    [SerializeField]
    Vector2 direction;
    [SerializeField]
    float speed;
    [SerializeField]
    int damage;

    void Start()
    {
        speed = Random.Range(3.0f, 4.0f);
    }

	void Update ()
    {
        this.transform.Translate(new Vector2(direction.x, direction.y) * speed * Time.deltaTime);
    }

    public void Hit()
    {
        direction.x = -direction.x;
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void InitializateStats(int intDirection, int spitDamage, bool isFacingRight)
    {
        damage = spitDamage;

        if(isFacingRight)
        {
            if(intDirection == 1)
            {
                direction = new Vector2(1, 0.6f);
            }
            if (intDirection == 2)
            {
                direction = new Vector2(1, 0.45f);
            }
            if (intDirection == 3)
            {
                direction = new Vector2(1, 0.3f);
            }
            if (intDirection == 4)
            {
                direction = new Vector2(1, 0.15f);
            }
            if (intDirection == 5)
            {
                direction = new Vector2(1, 0);
            }
            if (intDirection == 6)
            {
                direction = new Vector2(1, -0.15f);
            }
            if (intDirection == 7)
            {
                direction = new Vector2(1, -0.3f);
            }
            if (intDirection == 8)
            {
                direction = new Vector2(1, -0.45f);
            }
            if (intDirection == 9)
            {
                direction = new Vector2(1, -0.6f);
            }
        }
        else
        {
            if (intDirection == 1)
            {
                direction = new Vector2(-1, 0.6f);
            }
            if (intDirection == 2)
            {
                direction = new Vector2(-1, 0.45f);
            }
            if (intDirection == 3)
            {
                direction = new Vector2(-1, 0.3f);
            }
            if (intDirection == 4)
            {
                direction = new Vector2(-1, 0.15f);
            }
            if (intDirection == 5)
            {
                direction = new Vector2(-1, 0);
            }
            if (intDirection == 6)
            {
                direction = new Vector2(-1, -0.15f);
            }
            if (intDirection == 7)
            {
                direction = new Vector2(-1, -0.3f);
            }
            if (intDirection == 8)
            {
                direction = new Vector2(-1, -0.45f);
            }
            if (intDirection == 9)
            {
                direction = new Vector2(-1, -0.6f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().RecieveDamage(damage);
        }
        if (other.gameObject.tag == "BossSlime")
        {
            other.GetComponent<BossSlime>().RecieveDamage(damage);
        }

        Destroy(this.gameObject);
    }
}
