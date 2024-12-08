using UnityEngine;

public class PatrollingState : IState
{
    private readonly NPCController npc;

    private bool hasEnteredState = false;

    public PatrollingState(NPCController npcController)
    {
        npc = npcController;
    }

    public void EnterState()
    {
        if (!hasEnteredState)
        {
            Debug.Log("Entering Patrolling State.");
            npc.SetSpeed(npc.PatrolSpeed); 
            npc.GoToNextWaypoint(); 
            hasEnteredState = true;  
        }
    }

    public void UpdateState()
    {
        if (!npc.IsWaiting && npc.Agent.remainingDistance < 0.5f)
        {
            npc.StartCoroutine(npc.WaitAtWaypoint());
        }

        if (npc.PlayerDetected)
        {
            npc.TransitionToState(npc.ChasingState); 
        }
    }

    public void ExitState()
    {
        Debug.Log("Exiting Patrolling State.");
        hasEnteredState = false;  
    }
}
