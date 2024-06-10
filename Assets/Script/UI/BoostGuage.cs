using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostGuage : MonoBehaviour
{
    public Slider Boostslider;

    private New_Car_move car;
    // Start is called before the first frame update
    void Start()
    {
        if (car == null)
        {
            car = FindObjectOfType<New_Car_move>();
        }

        Boostslider.maxValue = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (car != null)
        {
            Boostslider.value = car.Boost_delay_time;
        }
    }
}
