using UnityEngine;

public class ChasingState : IState
{
    private readonly NPCController npc;

    public ChasingState(NPCController npcController)
    {
        npc = npcController;
    }

    public void EnterState()
    {
        Debug.Log("Entering Chasing State.");
        npc.SetSpeed(npc.chaseSpeed);
    }

    public void UpdateState()
    {
        npc.Agent.destination = npc.DetectionScript.Player.position;

        if (!npc.PlayerDetected)
        {
            npc.LastKnownPlayerPosition = npc.DetectionScript.Player.position;
            npc.TransitionToState(npc.SearchingState); 
        }
    }

    public void ExitState()
    {
        Debug.Log("Exiting Chasing State.");
    }
}
