using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFunction : MonoBehaviour
{
    [SerializeField] Collider2D doorColider;

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
        {
            doorColider.enabled = false;
            Destroy(gameObject);
        }
    }
}
