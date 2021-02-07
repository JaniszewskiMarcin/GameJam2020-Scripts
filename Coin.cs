using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int coinCounter = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && other == other.gameObject.GetComponent<BoxCollider2D>())
        {
            GameManager.instance.UpdateCoinValue();
            Destroy(gameObject);
        }
    }
}
