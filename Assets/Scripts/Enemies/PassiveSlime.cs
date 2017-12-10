using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSlime : MonoBehaviour
{
    [Header("Slime Fields")]
    [SerializeField]
    int life = 20;
    Animator myAnim;

    //provisional
    float deadTime = 0.5f;
    float deadCounter;

    [SerializeField]
    PassiveSlimeState currentPassiveSlimeState;
    enum PassiveSlimeState
    {
        Idle,
        Dead
    }

    void Start ()
    {
        myAnim = GetComponentInChildren<Animator>();
    }

	void Update ()
    {
        switch (currentPassiveSlimeState)
        {
            case PassiveSlimeState.Idle:
                Idle();
                break;

            case PassiveSlimeState.Dead:
                Dead();
                break;

            default:
                break;
        }
    }

    #region UPDATE METHODS

    void Idle()
    {

    }

    void Dead()
    {
        myAnim.SetTrigger("Dead");
        deadCounter -= Time.deltaTime;

        if (deadCounter <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region STATE METHODS

    void IdleState()
    {
        currentPassiveSlimeState = PassiveSlimeState.Idle;
    }

    void DeadState()
    {
        currentPassiveSlimeState = PassiveSlimeState.Dead;
    }

    #endregion

    public void RecieveDamage(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            DeadState();
            deadCounter = deadTime;
        }
    }
}
