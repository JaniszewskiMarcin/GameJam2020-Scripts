using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpikes : MonoBehaviour
{
    [SerializeField] GameObject spikes;
    [SerializeField] Collider2D trapDeadColiders;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spikes.SetActive(true);
        trapDeadColiders.enabled = true;
    }
}
