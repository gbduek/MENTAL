using UnityEngine;

public class CoworkerAi : MonoBehaviour
{
    private GameObject player;
    private UnityEngine.AI.NavMeshAgent myNavMeshAgent;
    private Vector3 moveDirection;
    private Animator animator;

    enum States
    {
        Idle,
        Chase,
        Intercept
    };

    States currentState = States.Idle;

    void Start()
    {
        animator = GetComponent<Animator>();
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

        moveDirection = myNavMeshAgent.velocity.normalized;
        if (animator != null)
        {
            // Check if the player is moving
            if (moveDirection == Vector3.zero)
            {
                //if not -> idle
                animator.SetBool("isWalking", false);
                animator.SetFloat("LastInputX", animator.GetFloat("InputX"));
                animator.SetFloat("LastInputY", animator.GetFloat("InputY"));
            }
            else
            {
                //if moving -> walking
                animator.SetBool("isWalking", true);
                animator.SetFloat("InputX", moveDirection.x);
                animator.SetFloat("InputY", moveDirection.y);
            }
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

