using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Speed_Slider : MonoBehaviour
{
    private New_Car_move car;
    public Slider Speedslider;
    public float Maxspeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        car = FindObjectOfType<New_Car_move>();
        if (Speedslider != null && car != null)
        {
            Speedslider.minValue = 0f;
            Speedslider.maxValue = Maxspeed;
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (car != null)
        {
            float currentSpeed = car.Current_speed;
            
            if (Speedslider != null)
            {
                Speedslider.value = Mathf.Abs(currentSpeed);
            }
        }
    }
}
