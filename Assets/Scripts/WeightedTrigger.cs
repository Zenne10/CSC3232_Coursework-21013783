using UnityEngine;

public class WeightedTrigger : MonoBehaviour
{
    [Header("Trigger Settings")]
    [SerializeField] private float requiredMass = 2f; 
    [SerializeField] private float scaleDuration = 1f; 

    [Header("References")]
    [SerializeField] private GameObject water; 

    private float currentMass = 0f;
    private bool isScaling = false; 

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            currentMass += rb.mass;

            if (currentMass >= requiredMass && !isScaling)
            {
                StartCoroutine(ScaleAndActivate());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            currentMass -= rb.mass;
            currentMass = Mathf.Max(currentMass, 0f);
        }
    }

    private System.Collections.IEnumerator ScaleAndActivate()
    {
        isScaling = true;

        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = new Vector3(initialScale.x, 0f, initialScale.z);
        float elapsedTime = 0f;

        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale; 
        ActivateTrigger();
    }

    private void ActivateTrigger()
    {
        if (water != null)
        {
            water.SetActive(false); 
        }
    }
}
