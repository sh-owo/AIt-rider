using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_line : MonoBehaviour
{
    public Timer timer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        New_Car_move car = other.GetComponent<New_Car_move>();
        if (car != null)
        {
            if (timer.time > 30f)
            {
                Time.timeScale = 0f;
            }
        }
    }
}
