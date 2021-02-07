using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player.transform.position.y > transform.position.y)
        {
            effector.rotationalOffset = 180f;
        }
        else
        {
            effector.rotationalOffset = 0f;
        }
    }
}
