using UnityEngine;

public class NPCDetection : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private Transform player; 
    [SerializeField] private float detectionAngle = 45f; 
    [SerializeField] private float detectionRange = 10f; 
    [SerializeField] private float maxDetectionDistance = 10f; 
    [SerializeField] private LayerMask obstacleMask; 

    private bool isPlayerDetected = false;

    public bool IsPlayerDetected => isPlayerDetected;
    public Transform Player => player;

    void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer < maxDetectionDistance)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer < detectionAngle)
            {
                if (!Physics.Raycast(transform.position, directionToPlayer.normalized, distanceToPlayer, obstacleMask))
                {
                    if (!isPlayerDetected)
                    {
                        isPlayerDetected = true;
                        Debug.Log("Player Detected!"); 
                    }
                }
                else
                {
                    if (isPlayerDetected)
                    {
                        isPlayerDetected = false;
                        Debug.Log("Player Lost!"); 
                    }
                }
            }
            else
            {
                if (isPlayerDetected)
                {
                    isPlayerDetected = false;
                    Debug.Log("Player Lost!"); 
                }
            }
        }
        else
        {
            if (isPlayerDetected)
            {
                isPlayerDetected = false;
                Debug.Log("Player Lost!"); 
            }
        }
    }
}
