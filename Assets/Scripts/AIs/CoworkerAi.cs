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

    States currentState = States.Idle;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        myNavMeshAgent.updateRotation = false;
        myNavMeshAgent.updateUpAxis = false;
    }

    void Update()
    {
        Transition();
        switch (currentState)
        {
            case States.Idle:
                //Idle();
                break;
            case States.Chase:
                myNavMeshAgent.SetDestination(player.transform.position);
                break;
            case States.Intercept:
                //Intercept();
                break;
        }
        
    }

    void Transition()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 5f)
        {
            currentState = States.Chase;
        }
        else
        {
            currentState = States.Idle;
        }
    }
}

