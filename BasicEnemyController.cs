using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class BasicEnemyController : MonoBehaviour
{
    private enum State
    {
        Moving,
        Knockback,
        Dead
    }

    private State currentState;
    [SerializeField] private float 
        groundCheckDistance,
        wallCheckDistance, 
        movementSpeed, 
        maxHealth, 
        knocbackDuration,
        lastTouchDamageTime,
        touchDamageCollDown,
        touchDamage,
        touchDamageWidth,
        touchDamageHeight;
    [SerializeField] private Transform 
        groundCheck, 
        wallCheck,
        touchDamageCheck;
    [SerializeField] private LayerMask 
        whatIsGround,
        whatIsPlayer;
    [SerializeField] private Vector2 knockbackSpeed;

    private bool groundDetected, wallDetected;
    private int facingDirection, damageDirection;
    private GameObject alive;
    private GameObject player;
    private Rigidbody2D aliveRB;
    private Vector2 movement;
    private Vector2 touchDamageBotLeft;
    private Vector2 touchDamageTopRight;
    private float currentHealth, knockbackStartTime, deadTime;
    private Animator aliveAnim;

    public static bool enemyAttack;

    private float[] attackDetails = new float[2];

    private void Start()
    {
        alive = transform.Find("Live").gameObject;
        aliveRB = alive.GetComponent<Rigidbody2D>();
        aliveAnim = alive.GetComponent<Animator>();

        currentHealth = maxHealth;
        facingDirection = 1;
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null || PlayerStats.currentHealthStatic == 0)
        {
            enemyAttack = false;
        }

        switch (currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            
            case State.Knockback:
                UpdateKnockbackState();
                break;

            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    //WALKING STATE

    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
            groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
            wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);


            CheckTouchDamage();
            if (!groundDetected || wallDetected)
            {
                Flip();
            }
            else
            {
                movement.Set(movementSpeed * facingDirection, aliveRB.velocity.y);
                aliveRB.velocity = movement;
            }

        aliveAnim.SetBool("enemyAttacking", enemyAttack);
    }

    private void ExitMovingState()
    {

    }

    //KNOCKBACK STATE

    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRB.velocity = movement;
        aliveAnim.SetBool("Knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if(Time.time >= knockbackStartTime + knocbackDuration)
        {
            SwitchState(State.Moving);
        }

    }

    private void ExitKnockbackState()
    {
         aliveAnim.SetBool("Knockback", false);
    }

    //DEAD STATE

    private void EnterDeadState()
    {
        //Spawn dead particle
        aliveAnim.SetBool("dead", true);
        aliveRB.isKinematic = true;
        alive.GetComponent<Collider2D>().isTrigger = true;
        aliveRB.velocity = new Vector2(0.0f, 0.0f);
        deadTime = Time.time;
    }

    private void UpdateDeadState()
    {
        TimeToDestroyEnemy(2f);
    }

    private void ExitDeadState()
    {
    }

    //Other

    private void TimeToDestroyEnemy(float timeDuration)
    {
        if(Time.time >= deadTime + timeDuration)
        {
            Destroy(gameObject);
        }
    }

    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];

        if(attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        //Hit particles

        if(currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if(currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    private void CheckTouchDamage()
    {
        if(Time.time >= lastTouchDamageTime + touchDamageCollDown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if(hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = alive.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
                enemyAttack = true;
            }
            else
            {
                enemyAttack = false;
            }
        }
    }

    IEnumerator WaitSomeTime()
    {
        yield return new WaitForSeconds(0.5f);
    }

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void SwitchState(State state)
    {
        switch(currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;

            case State.Knockback:
                ExitKnockbackState();
                break;

            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;

            case State.Knockback:
                EnterKnockbackState();
                break;

            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

        Vector2 bottomLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 bottomRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2)); 
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2)); 
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
    }
}
