using UnityEngine;

public class NPCTakedown : MonoBehaviour
{
    [Header("Takedown Settings")]
    [SerializeField] private float takedownRange = 2f; 
    [SerializeField] private KeyCode takedownKey = KeyCode.Q; 
    [SerializeField] private Material defaultMaterial; 
    [SerializeField] private Material takedownMaterial; 

    private Renderer npcRenderer;
    private bool isTakedownAvailable = false;

    private void Start()
    {
        npcRenderer = GetComponent<Renderer>();
        UpdateMaterial(false); 
    }

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, takedownRange);
        isTakedownAvailable = false; 

        foreach (Collider collider in hitColliders)
        {
            PlayerTakedown player = collider.GetComponent<PlayerTakedown>();
            NPCDetection detection = GetComponent<NPCDetection>();

            if (player != null && detection != null && !detection.IsPlayerDetected)
            {
                isTakedownAvailable = true;

                if (Input.GetKeyDown(takedownKey))
                {
                    PerformTakedown();
                }
                break; 
            }
        }
        UpdateMaterial(isTakedownAvailable);
    }

    private void UpdateMaterial(bool takedownReady)
    {
        npcRenderer.material = takedownReady ? takedownMaterial : defaultMaterial;
    }

    public bool IsTakedownReady()
    {
        return isTakedownAvailable;
    }

    public void PerformTakedown()
    {
        Debug.Log("Takedown performed!");
        gameObject.SetActive(false); 
    }

}
