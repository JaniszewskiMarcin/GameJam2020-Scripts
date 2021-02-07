using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthCounter : MonoBehaviour
{
    [SerializeField] GameObject hearth_01;
    [SerializeField] GameObject hearth_02;
    [SerializeField] GameObject hearth_03;

    private void Update()
    {
        if(PlayerStats.currentHealthStatic >= 90)
        {
            if(hearth_03.activeSelf == true && hearth_02.activeSelf == true && hearth_01.activeSelf == true)
            {
                return;
            }
            else
            {
                hearth_01.SetActive(true);
                hearth_02.SetActive(true);
                hearth_03.SetActive(true);
            }
        }
        if (PlayerStats.currentHealthStatic <= 60)
        {
            hearth_03.SetActive(false);
        }
        if (PlayerStats.currentHealthStatic <= 30)
        {
            hearth_02.SetActive(false);
        }
        if (PlayerStats.currentHealthStatic <= 0)
        {
            hearth_01.SetActive(false);
        }

    }
}
