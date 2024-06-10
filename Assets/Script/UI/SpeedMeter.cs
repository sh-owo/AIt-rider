using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedMeter : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    private New_Car_move car;
    void Start()
    {
        car = FindObjectOfType<New_Car_move>();
    }

    // Update is called once per frame
    void Update()
    {
        float speeds = Mathf.Abs(car.Current_speed);
        if (car != null)
        {
            string format = "Speed: " + speeds.ToString("00");
            text.text = format;
        }
    }
}
