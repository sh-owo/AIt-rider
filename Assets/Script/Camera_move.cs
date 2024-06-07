using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_move : MonoBehaviour
{
    private Countdown countdown;
    private float time;
    public Camera camera;
    
    public Transform Car;
    public float smooth = 0.25f;
    private float zoomrate = 6.5f;
    public Vector3 offset;

    private void Start()
    {
        Application.targetFrameRate = 60;
        countdown = FindObjectOfType<Countdown>();
    }

    void LateUpdate()
    {
        if (Car != null)
        {
            Vector3 desiredPosition = Car.position + new Vector3(offset.x, offset.y, transform.position.z); 
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smooth);
            transform.position = smoothedPosition;
        }

        if (!(countdown.time < 1.5f))
        {
            camera.orthographicSize -= zoomrate * Time.deltaTime;
        }
        else
        {
            camera.orthographicSize = 20f;
        }
        
        
        /*if (!countdown.start)
        {
            camera.orthographicSize -= zoomrate * Time.deltaTime * 3f;
        }
        else
        {
            camera.orthographicSize = 20f;
        }*/

        

    }
}
