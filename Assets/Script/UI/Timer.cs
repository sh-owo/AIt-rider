using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    private Countdown countdown;
    public Text timetext;
    public float time;
    void Start()
    {
        time = 0f;
        countdown = FindObjectOfType<Countdown>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown != null && countdown.start)
        {
            time += Time.deltaTime;
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

            string format = "time: " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("000"); 
            timetext.text = format;
        }

    }
}
