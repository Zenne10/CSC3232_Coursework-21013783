using UnityEngine;

public class PlayerTakedown : MonoBehaviour
{
    [Header("Takedown Settings")]
    [SerializeField] private float takedownRange = 2f; 
    [SerializeField] private KeyCode takedownKey = KeyCode.Q; 

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, takedownRange);
        foreach (Collider collider in hitColliders)
        {
            NPCTakedown npcTakedown = collider.GetComponent<NPCTakedown>();
            if (npcTakedown != null)
            {
                if (npcTakedown.IsTakedownReady())
                {
                    Debug.Log("Takedown available for NPC in range.");

                    if (Input.GetKeyDown(takedownKey))
                    {
                        npcTakedown.PerformTakedown();
                        Debug.Log("NPC taken down.");
                        break; 
                    }
                }
                else
                {
                    Debug.Log("Takedown not available: Player detected.");
                }
            }
        }
    }
}
