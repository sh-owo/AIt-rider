using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CarAgent2D : Agent
{
    public Transform[] checkpoints;
    public float speed = 10f;
    public float turnSpeed = 5f;
    private int currentCheckpointIndex = 0;

    private Rigidbody2D rb;
    private Vector3 initalPosition;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initalPosition = transform.localPosition;
    }

    public override void OnEpisodeBegin()
    {
        // 에이전트 초기화
        rb.velocity = Vector2.zero;
        currentCheckpointIndex = 0;

        // 에이전트 위치와 회전 초기화
        transform.localPosition = initalPosition;
        transform.localRotation = Quaternion.identity;
        
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

        if (Vector2.Distance(transform.position, checkpoints[currentCheckpointIndex].position) < 1f)
        {
            currentCheckpointIndex = (currentCheckpointIndex + 1) % checkpoints.Length;
            AddReward(1.0f);
        }

        Debug.Log("Action received: " + forwardAmount + ", " + turnAmount);
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
        // if(collision.gameObject.CompareTag("Goal"))
    }
}
