using UnityEngine;

public class TeleportItem : MonoBehaviour
{
    [Header("Floating Animation Settings")]
    [SerializeField] private float floatAmplitude = 0.5f; 
    [SerializeField] private float floatFrequency = 1f; 
    [SerializeField] private float rotationSpeed = 50f; 

    [Header("References")]
    [SerializeField] private TeleportAbility teleportAbility; 
    [SerializeField] private GameObject ground; 
    [SerializeField] private Material sandMaterial; 
    [SerializeField] private Material defaultMaterial; 
    [SerializeField] private SandPhysics sandPhysics; 

    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;

        if (ground != null && defaultMaterial != null)
        {
            ground.GetComponent<Renderer>().material = defaultMaterial;
        }

        if (sandPhysics != null)
        {
            sandPhysics.enabled = false; 
            Debug.Log("SandPhysics disabled at start"); 
        }
    }

    void Update()
    {
        float floatOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = _startPosition + new Vector3(0, floatOffset, 0);

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && teleportAbility != null)
        {
            teleportAbility.EnableTeleport(); 

            if (sandPhysics != null)
            {
                sandPhysics.EnableSandPhysics(); 
            }

            if (ground != null && sandMaterial != null)
            {
                ground.GetComponent<Renderer>().material = sandMaterial;
            }

            Destroy(gameObject); 
        }
    }

}
