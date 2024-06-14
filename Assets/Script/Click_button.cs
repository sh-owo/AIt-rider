using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Click_button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDrive()
    {
        SceneManager.LoadScene("DrivingScene");
    }

    public void Howtodo()
    {
        SceneManager.LoadScene("toHow");
    }

    public void back()
    {
        SceneManager.LoadScene("Start Menu");
    }
    
}
