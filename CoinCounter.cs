using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public static int coinCounter = 0;
    bool isColliding = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isColliding == false)
        {
            if (other.gameObject.tag == "Coin")
            {
                Destroy(other.gameObject);
            }
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
    }
}
