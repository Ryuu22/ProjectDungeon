using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Player : MonoBehaviour
{
    [Header("Game Elements")]
    InputMaster inputM;
    CollisionMaster collisionM;
    [SerializeField]
    AudioPlayer audioP;
    public CameraBehaviour cameraBeh;
    public PostProcessingProfile lightPospo;

    [Header("Player Fields")]
    bool godMode;
    [SerializeField]
    float life = 100;
    float lightIntensity;
    [SerializeField]
    int essences = 100;
    [SerializeField]
    GameObject lifeParticles;
    [SerializeField]
    GameObject bloodParticles;
    ParticleSystem blood;
    bool isDead;
    bool recievedDamage;
    float inmuneCounter;
    Vector2 movementSpeed = Vector2.zero;
    Vector2 speed = new Vector2(5, 5);
    Vector2 dashSpeed = new Vector2(15, 15);
    Vector2 dashDirection;
    float deadCounter = 0;

    float attackCooldown;
    float attackCounter;
    int damage = 10;
    float dashCooldown;
    float dashCounter = 0.2f;

    float timer;

    Animator myAnim;
    Rigidbody rb;

    public GameObject triggerGameObject;

    PlayerState currentPlayerState;
    enum PlayerState
    {
        Idle,
        Move,
        Dash,
        Dead,
    }

    void Start()
    {
        inputM = GameObject.FindGameObjectWithTag("InputMaster").GetComponent<InputMaster>();
        isDead = false;
        rb = GetComponent<Rigidbody>();
        blood = bloodParticles.GetComponent<ParticleSystem>();
        collisionM = GetComponent<CollisionMaster>();
        myAnim = GetComponentInChildren<Animator>();

    }


    void Update()
    {
        switch (currentPlayerState)
        {
            case PlayerState.Idle:
                Idle();
                break;

            case PlayerState.Move:
                Move();
                break;

            case PlayerState.Dash:
                Dash();
                break;

            case PlayerState.Dead:
                Dead();
                break;

            default:
                break;
        }

        if(life < 100)
        {
            life += Time.deltaTime*5;
        }
        if (life > 100) life = 100;

        lightIntensity = 1 - (life/100);

        VignetteModel.Settings vignette = lightPospo.vignette.settings;
        vignette.intensity = lightIntensity;
        lightPospo.vignette.settings = vignette;

        if (attackCooldown > 0)
        {
            attackCounter -= Time.deltaTime;
        }
        dashCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.G))
        {
            godMode = !godMode;
        }

        if(godMode)
        {
            speed = new Vector2(8, 8);
            attackCooldown = 0;
            damage = 50;
            this.gameObject.layer = 1;
        }
        else
        {
            attackCooldown = 0.2f;
            damage = 10;
            this.gameObject.layer = LayerMask.NameToLayer("Player");
        }
        if (recievedDamage)
        {
            this.gameObject.GetComponent<CapsuleCollider>().radius = 0;
            inmuneCounter += Time.deltaTime;

            if(inmuneCounter >= 2)
            {
                recievedDamage = false;
                inmuneCounter = 0;
                this.gameObject.GetComponent<CapsuleCollider>().radius = 0.5f;
            }
        }

        if(this.transform.position.y <= -0.5f)
        {
            Fall();
            myAnim.SetTrigger("Fall");
            isDead = true;
        }
    }

    #region UPDATE METHODS

    void Idle()
    {
        if (inputM.GetAxis().x > 0.1 || inputM.GetAxis().y > 0.1 || inputM.GetAxis().x < -0.1 || inputM.GetAxis().y < -0.1) //PLAYER MOVES
        {
            myAnim.SetBool("Moving", true);
            MoveState();
        }
    }

    void Move()
    {
        Vector3 provisionalPos;

        provisionalPos = this.transform.position;
        movementSpeed = speed;

        if (inputM.GetAxis().x < 0.1 && inputM.GetAxis().x > -0.1 && inputM.GetAxis().y < 0.1 && inputM.GetAxis().y > -0.1) //PLAYER STOP
        {
            myAnim.SetBool("Moving", false);
            IdleState();
        }

        if (collisionM.IsLeftWalled && inputM.GetAxis().x < 0) //LEFT WALL
        {
            movementSpeed.x = 0;
            myAnim.SetBool("Moving", false);
        }

        if (collisionM.IsRightWalled && inputM.GetAxis().x > 0) //RIGHT WALL
        {
            movementSpeed.x = 0;
            myAnim.SetBool("Moving", false);
        }

        if (collisionM.IsTopWalled && inputM.GetAxis().y > 0) //TOP WALL
        {
            movementSpeed.y = 0;
            myAnim.SetBool("Moving", false);
        }

        if (collisionM.IsBottomWalled && inputM.GetAxis().y < 0) //BOTTOM WALL
        {
            movementSpeed.y = 0;
            myAnim.SetBool("Moving", false);
        }

        if (inputM.GetAxis().x > 0.1) //FLIP RIGHT
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);

        }

        if (inputM.GetAxis().x < -0.1) //FLIP LEFT
        {
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);

        }

        timer++;
        if (timer % 15 == 1) audioP.PlaySFX(9, 1, Random.Range(0.9f, 1.1f));

        provisionalPos.x += inputM.GetAxis().x * Time.deltaTime * movementSpeed.x;
        provisionalPos.z += inputM.GetAxis().y * Time.deltaTime * movementSpeed.y;
        this.transform.position = provisionalPos;
    }

    void Dash()
    {
        Vector3 provisionalPos;
        provisionalPos = this.transform.position;
        myAnim.SetTrigger("Dash");

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            provisionalPos.x += dashDirection.x * Time.deltaTime * dashSpeed.x;
            provisionalPos.z += dashDirection.y * Time.deltaTime * dashSpeed.y/2;
            rb.useGravity = false;
            this.transform.position = provisionalPos;
        }
        else
        {
            dashCounter = 0.2f;
            rb.useGravity = true;
            this.gameObject.GetComponent<CapsuleCollider>().radius = 0.5f;
            IdleState();
        }
    }

    void Dead()
    {
        deadCounter += Time.deltaTime;

        if(deadCounter >= 2)
        {
            //sceneM.LoadEndingScreen();
        }
    }

    #endregion

    #region STATE METHODS

    public void IdleState()
    {
        currentPlayerState = PlayerState.Idle;
    }

    public void MoveState()
    {
        currentPlayerState = PlayerState.Move;
    }

    public void DashState()
    {
        currentPlayerState = PlayerState.Dash;
    }

    public void DeadState()
    {
        currentPlayerState = PlayerState.Dead;
    }

    #endregion

    #region MECHANICS METHODS
    
    public void BeginDash()
    {
        if(dashCooldown < 0)
        {
            if (inputM.GetAxis().x > -0.4f && inputM.GetAxis().x < 0.4f) dashDirection.x = 0;
            if (inputM.GetAxis().x < 0) dashDirection.x = -1;
            if (inputM.GetAxis().x > 0) dashDirection.x = 1;

            if (inputM.GetAxis().y > -0.4f && inputM.GetAxis().y < 0.4f) dashDirection.y = 0;
            if (inputM.GetAxis().y < 0) dashDirection.y = -1;
            if (inputM.GetAxis().y > 0) dashDirection.y = 1;

            dashCooldown = 5;
            this.gameObject.GetComponent<CapsuleCollider>().radius = 0;
            DashState();
            audioP.PlaySFX(10, 1, Random.Range(0.9f, 1.1f));
        }
    }

    public void Attack() //ATTACK IF PLAYER IS IDLE OR MOVING
    {
        if (attackCounter <= 0 && currentPlayerState == PlayerState.Idle || attackCounter <= 0 && currentPlayerState == PlayerState.Move)
        {
            attackCounter = attackCooldown;

            speed = new Vector2(0, 0);
            myAnim.SetTrigger("Attack");
            audioP.PlaySFX(8, 1, Random.Range(0.9f, 1.1f));
            triggerGameObject.SetActive(true);
        }
    }

    public void RecieveDamage()
    {
        if(!godMode)
        {
            life --;
            blood.Emit(30);
            audioP.Play2DSFX(6);
            recievedDamage = true;
            myAnim.SetTrigger("Hurt");

            if (life <= 0)
            {
                myAnim.SetTrigger("Death");
                isDead = true;
                DeadState();
            }
        }

    }

    public void ReceiveEssences(int numberOfEssences)
    {
        essences += numberOfEssences;
    }

    public void StartLifeParticles()
    {
        lifeParticles.SetActive(true);
    }

    public void StopLifeParticles()
    {
        lifeParticles.SetActive(false);
    }

    public void Fall()
    {
        DeadState();
    }

    #endregion

    #region GETTERS/SETTERS

    public float Life { get { return life; } }
    public bool IsDead { get { return isDead; } }
    public int Damage { get { return damage; } }

    public void ResetSpeed()
    {
        speed = new Vector2(5, 5);
    }

    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
    }
}
