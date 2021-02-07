using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownfallMace : MonoBehaviour
{
    [SerializeField] GameObject mace;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            mace.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
    }
}
