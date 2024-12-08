using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Door Settings")]
    public GameObject door;

    [Header("Player Settings")]
    public string playerTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (door != null)
            {
                Destroy(door);
            }
            else
            {
                Debug.LogWarning("No door assigned to ItemPickup script.");
            }
            Destroy(gameObject);
        }
    }
}
