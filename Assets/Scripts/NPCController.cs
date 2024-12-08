using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCController : MonoBehaviour
{
    public IState CurrentState { get; private set; }

    [Header("Patrol Settings")]
    public Transform[] waypoints;  
    public int currentWaypoint = 0;  
    public float patrolSpeed = 3.5f;  
    public float waitTimeAtWaypoint = 2.0f;  

    [Header("Chase Settings")]
    public float chaseSpeed = 5f;  

    [Header("Search Settings")]
    public float searchWaitTime = 5f;  

    public NavMeshAgent agent;  
    public bool isWaiting = false;  
    public bool playerDetected = false;  
    public Vector3 lastKnownPlayerPosition;  

    public NPCDetection detectionScript;  

    public Animator animator;  

    public PatrollingState PatrollingState { get; private set; }
    public ChasingState ChasingState { get; private set; }
    public SearchingState SearchingState { get; private set; }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        detectionScript = GetComponent<NPCDetection>();
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component is missing on NPC!");
        }

        PatrollingState = new PatrollingState(this);
        ChasingState = new ChasingState(this);
        SearchingState = new SearchingState(this);

        TransitionToState(PatrollingState);
    }

    void Update()
    {
        CurrentState?.UpdateState();

        playerDetected = detectionScript.IsPlayerDetected;
        if (CurrentState != null && playerDetected != detectionScript.IsPlayerDetected)
        {
            playerDetected = detectionScript.IsPlayerDetected;
            if (playerDetected)
            {
                TransitionToState(ChasingState);
            }
            else if (CurrentState == ChasingState)
            {
                TransitionToState(SearchingState);
            }
        }

        UpdateAnimation();
    }

    public void TransitionToState(IState newState)
    {
        if (CurrentState != newState)
        {
            CurrentState?.ExitState(); 
            CurrentState = newState;   
            CurrentState.EnterState(); 
        }
    }

    public void SetSpeed(float speed)
    {
        agent.speed = speed;
    }

    public void GoToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        agent.destination = waypoints[currentWaypoint].position;
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
    }

    public IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTimeAtWaypoint);
        isWaiting = false;

        float randomValue = Random.Range(0f, 1f);
        if (randomValue < 0.3f) 
        {
            TransitionToState(SearchingState);
        }
        else
        {
            GoToNextWaypoint();
        }
    }

    public IEnumerator WaitAndDecideTransition()
    {
        yield return new WaitForSeconds(searchWaitTime); 

        if (!playerDetected)
        {
            float randomValue = Random.Range(0f, 1f);
            if (randomValue < 0.7f) 
            {
                TransitionToState(PatrollingState);
                GoToNextWaypoint();
            }
        }
    }

    private void UpdateAnimation()
    {
        if (animator == null) return;


        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public bool IsWaiting => isWaiting;
    public bool PlayerDetected => playerDetected;
    public Vector3 LastKnownPlayerPosition { get; set; } 
    public NavMeshAgent Agent => agent;
    public NPCDetection DetectionScript => detectionScript;
    public float PatrolSpeed => patrolSpeed;
    public float ChaseSpeed => chaseSpeed;
}
