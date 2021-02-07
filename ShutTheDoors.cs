using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutTheDoors : MonoBehaviour
{
    [SerializeField] Camera cameraObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            cameraObject.orthographicSize = 10f;
            GetComponent<Collider2D>().isTrigger = false;
            transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
       }
    }
}
