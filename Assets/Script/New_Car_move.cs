using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// using Vector2 = System.Numerics.Vector2; = 개새

public class New_Car_move : MonoBehaviour
{
    public float Current_speed = 0f;
    public float Currnet_rotation = 0f;
    
    private float Moving_speed = 0f;
    public float Rotation_speed = 0f;
    private float Maxspeed = 30f;
    // private float Maxrotation = 40f;
    
    private float speed_acceleration = 1f;
    private float rotation_acceleration = 2f;

    public float Boost_delay_time = 5f;
    public float Current_delay_time = 6f;
    
    
    private Countdown countdown;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, fullscreen: true);
        rb = GetComponent<Rigidbody2D>();
        countdown = FindObjectOfType<Countdown>();
        // Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        float rotataion = Input.GetAxis("Horizontal");
        float accelerate = Input.GetAxis("Vertical");
        if (countdown != null && countdown.start)
        {
            //회전, 가속도값 설정
            if (rotataion != 0)
            {
                if (accelerate < 0)
                    Rotation_speed += rotation_acceleration * rotataion * Time.deltaTime;
                else
                    Rotation_speed += -rotation_acceleration * rotataion * Time.deltaTime;
            }
            else {
                Rotation_speed = 0f;
            }

            if (accelerate > 0) {
                
                Moving_speed = Mathf.Min(Moving_speed + speed_acceleration * Time.deltaTime, 2.5f);
            }
            else if (accelerate < 0) {
                Moving_speed = Mathf.Max(Moving_speed - speed_acceleration * Time.deltaTime, -2.5f);
                Current_speed = Mathf.Clamp(Current_speed, -10f, 30f);
            }
            else {
                Moving_speed = Mathf.Lerp(Moving_speed, 0, 0.6f);
            }
            

            //드리프트
            if (Input.GetKey(KeyCode.LeftShift)) {
                rotation_acceleration = 2f;
            }
            else {
                rotation_acceleration = 1f;
            }
            
            //부스트
            if (Input.GetKey(KeyCode.LeftControl) && Current_delay_time > 5f)
            {
                Boost_delay_time = 0f;
                Current_delay_time = 0f;
            }
            else
            {
                if (Boost_delay_time < 2f)
                {
                    speed_acceleration = 2.5f;
                }
                else
                {
                    speed_acceleration = 1f;
                    Moving_speed = Mathf.Clamp(Moving_speed, -2.5f, 2.5f);
                    Current_speed = Mathf.Clamp(Current_speed, -Maxspeed * 3/5, Maxspeed * 3/5);
                }
            }

            Current_speed += Moving_speed;
            Current_speed = Mathf.Clamp(Current_speed, -Maxspeed, Maxspeed);
            Currnet_rotation += Rotation_speed;

            Vector2 move = transform.up * Current_speed * Time.deltaTime;
            
            rb.MovePosition(rb.position + move);
            transform.Rotate(0, 0, Currnet_rotation);
            
            

            Boost_delay_time += Time.deltaTime;
            Current_delay_time += Time.deltaTime;
            Current_speed = Mathf.Lerp(Current_speed, 0, Time.deltaTime * 2);
            Currnet_rotation = Mathf.Lerp(Currnet_rotation, 0, 0.3f);
        }
    }
    
}