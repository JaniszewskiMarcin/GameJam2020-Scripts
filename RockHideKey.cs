using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHideKey : MonoBehaviour
{
    [SerializeField] Collider2D key;
    Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if(key == null)
        {
            return;
        }
        if (startPos.x + 1f == transform.position.x)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            key.enabled = true;
        }
    }

    private void OnMouseOver()
    {

        if(Input.GetMouseButtonDown(1) && startPos.x + 1f!= transform.position.x)
        {
            transform.position = new Vector2(transform.position.x + 1f, transform.position.y);
        }
    }
}
