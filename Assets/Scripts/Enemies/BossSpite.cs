using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpite : MonoBehaviour
{
    [SerializeField]
    Vector2 direction;

    [SerializeField]
    bool targetPlayer;
    GameObject player;

    [SerializeField]
    Vector2 playerPos;

    [SerializeField]
    float speed;

    [SerializeField]
    int damage;

    void Start()
    {
        speed = Random.Range(3.0f, 4.0f);
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;

        if(targetPlayer)
        {
            direction = (new Vector2(playerPos.x, playerPos.y + 1) - new Vector2(this.transform.position.x, this.transform.position.y)).normalized;
        }
    }

    void Update ()
    {
        this.transform.Translate(new Vector2(direction.x, direction.y) * speed * Time.deltaTime);
    }

    public void InitializateStats(int intDirection, int spitDamage, bool isFacingRight, bool _targetPlayer)
    {
        damage = spitDamage;
        targetPlayer = _targetPlayer;

        if (isFacingRight && !targetPlayer)
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
        if (!isFacingRight && !targetPlayer)
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

    public void Hit()
    {
        direction.x = -direction.x;
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().RecieveDamage();
        }
        if (other.gameObject.tag == "BossSlime")
        {

        }

        Destroy(this.gameObject);
    }
}
