using UnityEngine;

public class IceCubeMelting : MonoBehaviour
{
    [Header("Melting Settings")]
    public float meltingDuration = 10f;
    public AnimationCurve meltingCurve;
    private float elapsedTime = 0f;

    private Vector3 initialScale; 

    private void Start()
    {
        initialScale = transform.localScale;

        if (meltingCurve == null || meltingCurve.keys.Length == 0)
        {
            meltingCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        }
    }

    private void Update()
    {
       
        elapsedTime += Time.deltaTime;

        float progress = Mathf.Clamp01(elapsedTime / meltingDuration);
        float scaleFactor = meltingCurve.Evaluate(progress);

        transform.localScale = initialScale * (1f - scaleFactor);

        if (transform.localScale.magnitude < 0.01f)
        {
            Debug.Log($"Ice cube fully melted in {elapsedTime:F2} seconds.");
            Destroy(gameObject);
        }
    }
    public float GetElapsedTime() => elapsedTime;
}
