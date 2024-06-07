using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_moving : MonoBehaviour
{
    public GameObject car; 
    
    // Start is called before the first frame update
    void Start() {
        if (car == null) Debug.LogError("car load fail");
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = car.transform.position;
        Vector3 newposition = transform.position;
        if (transform.position.y + 60 < car.transform.position.y)
        {
            newposition.y = position.y + 60;
        }
        if (transform.position.y - 60 > car.transform.position.y)
        {
            newposition.y = position.y - 60;
        }
        
        if (transform.position.x != car.transform.position.x)
        {
            newposition.x = position.x;
        }

        transform.position = newposition;

        

    }
}
