using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public Text timetext;
    public float time;
    public bool start = false;
    void Start()
    {
        time = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        string format = seconds.ToString("0");
        if (time < 1f)
        {
            format = "Start";
            start = true;
        }
        timetext.text = format;
        if(time < 0.3f) timetext.gameObject.SetActive(false);
        
        

    }
}
