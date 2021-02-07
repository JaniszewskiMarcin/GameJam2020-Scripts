using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float currentHealth;
    public static float currentHealthStatic;

    [SerializeField] Animator anim;
    Camera2DFollow cameraObject;

    bool isColliding = false;

    private void Start()
    {
        cameraObject = GameObject.Find("Main Camera").GetComponent<Camera2DFollow>();
        currentHealth = maxHealth;
        currentHealthStatic = currentHealth;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        currentHealthStatic = currentHealth;

        if (currentHealth <= 0.0f)
        {
            Die();
        }

    }

    private void Die()
    {
        anim.SetBool("Dead", true);
        DialogeEditor.inConversation = true;
        gameObject.GetComponent<PlayerCombatController>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        gameObject.GetComponent<PlayerStats>().enabled = false;
        Debug.Log("JEST");
        GameManager.instance.Respawn();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isColliding == false)
        {
            if (other.gameObject.tag == "DeadCollider")
            {
                anim.SetBool("Dead", true);
                DialogeEditor.inConversation = true;
                cameraObject.enabled = false;
                currentHealthStatic = 0.0f;
                gameObject.GetComponent<PlayerStats>().enabled = false;
                gameObject.GetComponent<PlayerCombatController>().enabled = false;
                GameManager.instance.Respawn();

            }
        }
    }

    IEnumerator WaitSomeTime(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        isColliding = false;
    }
}
