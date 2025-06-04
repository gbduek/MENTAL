using UnityEngine;

public class CoworkerAi : MonoBehaviour
{
    GameObject player;
    UnityEngine.AI.NavMeshAgent myNavMeshAgent;

    enum States
    {
        Idle,
        Chase,
        Intercept
    };

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        myNavMeshAgent.updateRotation = false;
        myNavMeshAgent.updateUpAxis = false;
    }

    void Update()
    {
        myNavMeshAgent.SetDestination(player.transform.position);
    }
}

