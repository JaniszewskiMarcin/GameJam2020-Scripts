using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreBoss : MonoBehaviour
{
    public string TagToIgnore = "boss";

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagToIgnore)
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
