using UnityEngine;

public class WaterPhysics : MonoBehaviour
{
    [Header("Water Physics Settings")]
    [SerializeField] private float buoyancyForce = 10f; 
    [SerializeField] private float verticalDamping = 0.5f; 
    [SerializeField] private float waterDrag = 2f; 

    private Collider waterPlane; 
    private float waterSurfaceY; 

    void Start()
    {
        waterPlane = GetComponent<Collider>();

        if (waterPlane == null)
        {
            Debug.LogError("WaterPhysics requires a Collider on the same GameObject!");
            return;
        }

        if (!waterPlane.isTrigger)
        {
            Debug.LogWarning("WaterPhysics collider should be set as a trigger!");
        }

        waterSurfaceY = waterPlane.bounds.center.y + waterPlane.bounds.extents.y;
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        ApplyBuoyancy(rb);
        rb.drag = waterDrag;
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            rb.drag = 0; 
        }
    }

    public void ApplyBuoyancy(Rigidbody rb)
    {
        float depth = waterSurfaceY - rb.position.y;

        if (depth > 0)
        {
            float adjustedBuoyancy = buoyancyForce * depth;
            rb.AddForce(Vector3.up * adjustedBuoyancy, ForceMode.Force);

            rb.velocity = new Vector3(
                rb.velocity.x,
                rb.velocity.y * (1 - verticalDamping * Time.deltaTime),
                rb.velocity.z
            );
        }
    }

    public Collider GetWaterPlaneCollider()
    {
        return waterPlane;
    }
}
