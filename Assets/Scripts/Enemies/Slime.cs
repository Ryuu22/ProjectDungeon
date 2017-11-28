using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {

    private enum EnemyState { Idle, Patrol, Attack, Stun, Divide, Dead }
    [SerializeField] EnemyState state;
    [Header("Stats")]

    public bool canAttack = false;
    public int hitDamage = 3;
    public float coolDownAttack = 1.0f;
    public int life = 30;
    public int fase = 3;
    public float timeToLive;

	
	// Update is called once per frame
	void Update ()
    {
        switch(state)
        {
            case EnemyState.Idle:
                IdleUpdate();
                break;
            case EnemyState.Patrol:
                PatrolUpdate();
                break;
            case EnemyState.Attack:
                AttackUpdate();
                break;
            case EnemyState.Stun:
                StunUpdate();
                break;
            case EnemyState.Dead:
                DeadUpdate();
                break;
            default:
                break;
        }
    }
IdleUpdate()
    {

    }
}
