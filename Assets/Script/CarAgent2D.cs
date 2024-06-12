using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CarAgent2D : Agent
{
    public Transform[] checkpoints;
    public float speed = 10f;
    public float turnSpeed = 50f;
    private int currentCheckpointIndex = 0;

    private Rigidbody2D rb;
    private Vector3 initalPosition;

    public List<GameObject> obj;

    private float previous_distance;


    public GameObject checkpoint; // prefab

    
    private void Start()
    {
        if (checkpoint == null) checkpoint = GameObject.FindGameObjectWithTag("CC");
        rb = GetComponent<Rigidbody2D>();
        initalPosition = transform.localPosition;
        previous_distance = float.MaxValue;
        getChecks();
    }


    public void getChecks() {
        for (int i = 0; i < checkpoint.transform.childCount; i++) {
            Transform obj = checkpoint.transform.GetChild(i);
            GameObject gameObject = obj.gameObject;
            Rigidbody2D rigid;
            if(!gameObject.TryGetComponent<Rigidbody2D>(out rigid))
                rigid = gameObject.AddComponent<Rigidbody2D>(); 
            BoxCollider2D box;
            if(!gameObject.TryGetComponent(out box)) box  = gameObject.AddComponent<BoxCollider2D>(); 


            box.isTrigger = true;
            rigid.bodyType = RigidbodyType2D.Kinematic;
            rigid.gravityScale = 0;
            
        }
    }
    

    public override void OnEpisodeBegin()
    {
        // 에이전트 초기화
        rb.velocity = Vector2.zero;
        currentCheckpointIndex = 0;

        // 에이전트 위치와 회전 초기화
        transform.localPosition = initalPosition;
        transform.localRotation = Quaternion.identity;
        
        previous_distance = float.MaxValue;
        obj.Clear();
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // 에이전트가 관찰할 데이터 수집
        Vector2 directionToCheckpoint = (checkpoints[currentCheckpointIndex].position - transform.position).normalized;
        sensor.AddObservation(directionToCheckpoint);
        sensor.AddObservation(transform.up);
        sensor.AddObservation(rb.velocity);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float forwardAmount = actions.ContinuousActions[0];
        float turnAmount = actions.ContinuousActions[1];

        Vector2 forward = transform.up * forwardAmount * speed * Time.deltaTime;
        rb.MovePosition(rb.position + forward);

        float turn = turnAmount * turnSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -turn);

        float current = Vector2.Distance(transform.position, checkpoints[currentCheckpointIndex].position);

        for (int i = 1; i <= 5; i++) {
            if (current < 0.3f*i) {
                AddReward(5f/i);
                break;
            }
            if (current > previous_distance) {
                AddReward(-5f/i);
                break;
            }
            
            
        }
        
        previous_distance = current;



        // Debug.Log("Action received: " + forwardAmount + ", " + turnAmount);
        Debug.Log(checkpoints[currentCheckpointIndex].gameObject.name+" "+currentCheckpointIndex);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
        continuousActionsOut[1] = Input.GetAxis("Horizontal");

        Debug.Log("Heuristic actions: " + continuousActionsOut[0] + ", " + continuousActionsOut[1]);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Track"))
        {
            AddReward(-0.3f); // 보상을 삭감
            EndEpisode();     // 에피소드를 종료
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            EndEpisode();
        }

        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            string number = "";
            foreach(char ch in collision.gameObject.name)
            {
                
                if ('0' <= ch && ch <= '9') number += ch;
            }
            
            int num = int.Parse(number);
            if (currentCheckpointIndex + 1 >= num) {
                AddReward(-3f);
                return;
            }

            previous_distance = float.MaxValue;
            currentCheckpointIndex = (currentCheckpointIndex + 1) % checkpoints.Length;
            
            AddReward(3f);
        }
        
        if (collision.gameObject.name.Contains("Bot")) {

        }
        
        
        // if(collision.gameObject.CompareTag("Goal"))
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
           
            if (obj.Contains(collision.gameObject)) {
                AddReward(-3f);
                return;
            }
            obj.Add(collision.gameObject);

            previous_distance = float.MaxValue;
            currentCheckpointIndex = (currentCheckpointIndex + 1) % checkpoints.Length;
            
            AddReward(3f);
        }
    }
}