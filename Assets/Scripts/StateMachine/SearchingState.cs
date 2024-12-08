using UnityEngine;

public class SearchingState : IState
{
    private readonly NPCController npc;

    public SearchingState(NPCController npcController)
    {
        npc = npcController;
    }

    public void EnterState()
    {
        Debug.Log("Entering Searching State.");
        npc.SetSpeed(npc.PatrolSpeed); 

        if (npc.LastKnownPlayerPosition != Vector3.zero) 
        {
            npc.Agent.destination = npc.LastKnownPlayerPosition;
        }
        else
        {
            npc.Agent.isStopped = true;  
        }
    }

    public void UpdateState()
    {
        if (npc.Agent.remainingDistance < 1f)
        {
            npc.StartCoroutine(npc.WaitAndDecideTransition()); 
        }

        if (npc.PlayerDetected)
        {
            npc.TransitionToState(npc.ChasingState); 
        }
    }

    public void ExitState()
    {
        Debug.Log("Exiting Searching State.");
        npc.Agent.isStopped = false; 
    }
}
