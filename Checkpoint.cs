using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static int checkpointNumber = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && gameObject.name == "FirstCheckpoint")
        {
            checkpointNumber = 1;
        }

        if (other.tag == "Player" && gameObject.name == "SecondCheckpoint")
        {
            checkpointNumber = 2;
        }

        if (other.tag == "Player" && gameObject.name == "ThirdCheckpoint")
        {
            checkpointNumber = 3;
        }

        if (other.tag == "Player" && gameObject.name == "FourthCheckpoint")
        {
            checkpointNumber = 4;
        }
    }
}
