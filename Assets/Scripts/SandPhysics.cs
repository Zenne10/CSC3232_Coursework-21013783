using UnityEngine;

public class SandPhysics : MonoBehaviour
{
    [Header("Sand Physics Settings")]
    [SerializeField] private float sandDrag = 5f; 
    [SerializeField] private float jumpReduction = 0.5f; 
    [SerializeField] private float speedReduction = 0.5f; 

    private Collider sandPlane; 
    private bool isActive = false; 

    void Start()
    {
        sandPlane = GetComponent<Collider>();

        if (sandPlane == null)
        {
            Debug.LogError("SandPhysics requires a Collider on the same GameObject!");
            return;
        }

        if (!sandPlane.isTrigger)
        {
            sandPlane.isTrigger = true; 
            Debug.LogWarning("SandPhysics collider was not set as a trigger, fixing it.");
        }

        sandPlane.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isActive) return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        ApplySandPhysics(rb);
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            ResetPhysics(rb);
        }
    }

    public void EnableSandPhysics()
    {
        isActive = true;
        sandPlane.enabled = true; 
        Debug.Log("SandPhysics enabled.");
    }

    private void ApplySandPhysics(Rigidbody rb)
    {
        rb.drag = sandDrag;
        rb.velocity = new Vector3(
            rb.velocity.x * speedReduction,
            rb.velocity.y,
            rb.velocity.z * speedReduction
        );

        if (rb.velocity.y > 0)
        {
            rb.velocity = new Vector3(
                rb.velocity.x,
                rb.velocity.y * jumpReduction,
                rb.velocity.z
            );
        }
    }

    private void ResetPhysics(Rigidbody rb)
    {
        rb.drag = 0f;
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
    }
}
