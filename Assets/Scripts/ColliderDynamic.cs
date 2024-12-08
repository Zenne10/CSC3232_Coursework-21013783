using UnityEngine;

public class AssignedColliderMover : MonoBehaviour
{
    [Header("Collider Settings")]
    private Collider assignedCollider;
    private Vector2 moveRange = new Vector2(1f, 1f); 

    [Header("Toggle Settings")]
    private float toggleInterval = 5f; 
    private bool isColliderEnabled = true; 

    private void Start()
    {
        StartCoroutine(ToggleCollider());
    }

    private void Update()
    {
        float offsetX = Mathf.Sin(Time.time) * moveRange.x;
        float offsetY = Mathf.Cos(Time.time) * moveRange.y;

        if (assignedCollider is BoxCollider boxCollider)
        {
            boxCollider.center = new Vector3(offsetX, offsetY, 0f);
        }

    }

    private System.Collections.IEnumerator ToggleCollider()
    {
        while (true)
        {
            yield return new WaitForSeconds(toggleInterval);

            isColliderEnabled = !isColliderEnabled;
            assignedCollider.enabled = isColliderEnabled;
        }
    }
}
