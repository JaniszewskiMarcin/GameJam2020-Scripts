using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBridge : MonoBehaviour
{
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(transform.position == startPos.position)
            {
                transform.position = endPos.position;
                transform.rotation = endPos.rotation;
                GetComponent<Collider2D>().isTrigger = false;
            }
            else
            {
                transform.position = startPos.position;
                transform.rotation = startPos.rotation;
                GetComponent<Collider2D>().isTrigger = true;
            }
        }
    }
}
