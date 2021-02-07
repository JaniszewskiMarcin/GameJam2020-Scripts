using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossConv_01 : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    [SerializeField] GameObject spriteBoss;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && other == other.gameObject.GetComponent<BoxCollider2D>())
        {
            ps.gameObject.SetActive(true);
            Destroy(spriteBoss.gameObject);
            Destroy(gameObject);
        }
    }
}
