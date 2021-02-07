using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private bool combatEnabled;
    [SerializeField] private Transform attack1HitBox;
    [SerializeField] private LayerMask whatIsAttackable;
    private bool gotInput, isAttacking, isFirstAttack;
    [SerializeField] private float inputTimer, attack1Radius, attack1Damage;
    private float lastInputTime = Mathf.NegativeInfinity;
    private Animator anim;
    private float[] attackDetails = new float[2];
    private PlatformerCharacter2D PC;
    private PlayerStats PS;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        PC = GetComponent<PlatformerCharacter2D>();
        PS = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    private void CheckCombatInput()
    {
        if(Input.GetMouseButtonDown(0) && PlatformerCharacter2D.currentSpeed <= 0.8f && PlatformerCharacter2D.currentSpeed >= -0.8f && DialogeEditor.inConversation != true)
        {
            if(combatEnabled)
            {
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if(gotInput)
        {
            if(!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("Attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);
            }
        }

        if (Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBox.position, attack1Radius, whatIsAttackable);

        attackDetails[0] = attack1Damage;
        attackDetails[1] = transform.position.x;

        foreach(Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
        }
    }

    private void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("Attack1", false);
    }

    private void Damage(float[] attackDetails)
    {
        int direction;

        PS.DecreaseHealth(attackDetails[0]);

        if(attackDetails[1] < transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        PC.Knockback(direction);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBox.position, attack1Radius);
    }
}
