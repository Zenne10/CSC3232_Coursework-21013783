using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform player;
    [SerializeField] private float defaultDistance = 7f;
    [SerializeField] private float height = 2f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float verticalRotationLimit = 60f;
    [SerializeField] private float mouseSensitivity = 100f;

    [Header("Speed Settings")]
    [SerializeField] private float walkDistanceModifier = 6f;
    [SerializeField] private float sprintDistanceModifier = 9f;
    [SerializeField] private float walkFOV = 60f;
    [SerializeField] private float sprintFOV = 80f;
    [SerializeField] private float crouchDistanceModifier = 5f;
    [SerializeField] private float crouchFOV = 50f;

    private float currentRotation = 0f;
    private float verticalRotation = 0f;
    private Camera playerCamera;
    private bool isSprinting = false;
    private bool isCrouching = false;
    private bool isInFirstPerson = false;
    private float originalMouseSensitivity;

    void Start()
    {
        playerCamera = GetComponent<Camera>();
        if (playerCamera == null)
        {
            Debug.LogError("No Camera component found on this GameObject.");
            enabled = false;
            return;
        }
        originalMouseSensitivity = mouseSensitivity;
    }

    void Update()
    {
        HandleInput();
        UpdateCameraPositionAndRotation();
        AdjustCameraFOV();
    }

    private void HandleInput()
    {
        isInFirstPerson = Input.GetKey(KeyCode.E);
        mouseSensitivity = isInFirstPerson ? 200f : originalMouseSensitivity;

        isSprinting = Input.GetKey(KeyCode.LeftShift);
        isCrouching = Input.GetKey(KeyCode.LeftControl);
    }

    private void UpdateCameraPositionAndRotation()
    {
        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");

        currentRotation += horizontalInput * rotationSpeed;
        verticalRotation -= verticalInput * mouseSensitivity * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);

        if (isInFirstPerson)
        {
            transform.position = player.position;
            transform.rotation = Quaternion.Euler(verticalRotation, currentRotation, 0f);
        }
        else
        {
            Vector3 targetPosition = player.position - transform.forward * GetCameraDistance() + Vector3.up * height;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.rotation = Quaternion.Euler(verticalRotation, currentRotation, 0f);
        }
    }

    private float GetCameraDistance()
    {
        if (isCrouching) return crouchDistanceModifier;
        if (isSprinting) return sprintDistanceModifier;
        return walkDistanceModifier;
    }

    private void AdjustCameraFOV()
    {
        if (isCrouching)
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, crouchFOV, Time.deltaTime * smoothSpeed);
        }
        else if (isSprinting)
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, sprintFOV, Time.deltaTime * smoothSpeed);
        }
        else
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, walkFOV, Time.deltaTime * smoothSpeed);
        }
    }
}
