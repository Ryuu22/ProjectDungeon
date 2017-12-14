using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesBehaviour : MonoBehaviour {

    public enum State { Inactive, OnHold, Active};
    public State state;
    public Sprite[] sprites;
    public SpriteRenderer rend;
    public float timeCounter = 0;
    public float inactiveTime = 0;
    public float onHoldTime = 0;
    public float activeTime = 0;
	// Use this for initialization
	void Start ()
    {
        SetInactive();
	}
	
	// Update is called once per frame
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
        }
    }
    void UpdateActive()
    {
        timeCounter += Time.deltaTime;
        if(timeCounter > activeTime)
        {
            SetInactive();
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
}
