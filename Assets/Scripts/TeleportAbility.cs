using UnityEngine;

public class TeleportAbility : MonoBehaviour
{
    [SerializeField] private float maxTeleportDistance = 10f; 
    [SerializeField] private GameObject teleportHologramPrefab; 
    [SerializeField] private LayerMask obstacleMask; 

    private GameObject teleportHologramInstance;
    private bool isTeleportEnabled = false; 

    void Start()
    {
        teleportHologramInstance = Instantiate(teleportHologramPrefab);
        teleportHologramInstance.SetActive(false);
    }

    void Update()
    {
        if (!isTeleportEnabled) return; 

        if (Input.GetKey(KeyCode.E))
        {
            UpdateHologramPosition();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            TryTeleportPlayer();
        }
    }

    public void EnableTeleport()
    {
        isTeleportEnabled = true; 
        Debug.Log("Teleport ability enabled!");
    }

    void UpdateHologramPosition()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxTeleportDistance, obstacleMask))
        {
            teleportHologramInstance.transform.position = hit.point;
        }
        else
        {
            teleportHologramInstance.transform.position = ray.origin + ray.direction * maxTeleportDistance;
        }

        teleportHologramInstance.SetActive(true);
    }

    void TryTeleportPlayer()
    {
        if (teleportHologramInstance.activeSelf)
        {
            transform.position = teleportHologramInstance.transform.position;
        }

        teleportHologramInstance.SetActive(false);
    }
}
