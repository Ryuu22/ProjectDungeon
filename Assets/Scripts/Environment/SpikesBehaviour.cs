using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesBehaviour : MonoBehaviour
{
    public enum State { Inactive, OnHold, Active};
    public State state;
    public Sprite[] sprites;
    public SpriteRenderer rend;
    public float timeCounter = 0;
    public float inactiveTime = 0;
    public float onHoldTime = 0;
    public float activeTime = 0;
    public float damageTime = 0;
    float damageCounter;
    [SerializeField]
    BoxCollider2D collider2d;

    void Start ()
    {
        SetInactive();
	}

	void Update ()
    {
        switch(state)
        {
            case State.Inactive:
                UpdateInactive();
                break;
            case State.OnHold:
                UpdateOnHold();
                break;
            case State.Active:
                UpdateActive();
                break;
            default:
                break;
        }
    }

    #region Upadters

    void UpdateInactive()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter > inactiveTime)
        {
            SetOnHold();
        }

    }
    void UpdateOnHold()
    {
        timeCounter += Time.deltaTime;
        if(timeCounter > onHoldTime)
        {
            SetActive();
            collider2d.enabled = true;
        }
    }
    void UpdateActive()
    {
        timeCounter += Time.deltaTime;
        if(timeCounter > activeTime)
        {
            SetInactive();
            collider2d.enabled = false;
        }
    }
#endregion

    #region Setters

    void SetInactive()
    {
        timeCounter = 0;
        rend.sprite = sprites[0];

        state = State.Inactive;
    }
    void SetOnHold()
    {
        timeCounter = 0;
        rend.sprite = sprites[1];
        state = State.OnHold;
    }
    void SetActive()
    {
        timeCounter = 0;
        rend.sprite = sprites[2];
        state = State.Active;
    }
    #endregion

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == ("Player"))
        {
            if (state == State.Active)
            {
                other.GetComponent<Player>().RecieveDamage();
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            if (state == State.Active)
            {
                damageCounter -= Time.deltaTime;

                if(damageCounter <= 0)
                {
                    damageCounter = damageTime;
                    other.GetComponent<Player>().RecieveDamage();
                }
            }
        }
    }
}
