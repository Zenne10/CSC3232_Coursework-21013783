using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwim : MonoBehaviour
{
    [Header("Swimming Settings")]
    [SerializeField] private float swimSpeed = 3f; 

    [Header("References")]
    [SerializeField] private Camera playerCamera; 
    [SerializeField] private WaterPhysics waterPhysics; 

    private Rigidbody _rb;
    private bool _isInWater = false; 

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (waterPhysics == null)
        {
            Debug.LogError("WaterPhysics is not assigned! Please assign it in the Inspector.");
        }
    }

    void Update()
    {
        if (_isInWater)
        {
            HandleSwimming();
        }
    }

    private void HandleSwimming()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0; 
        cameraForward.Normalize();

        Vector3 cameraRight = playerCamera.transform.right;
        cameraRight.y = 0; 
        cameraRight.Normalize();

        Vector3 swimDirection = cameraForward * moveVertical + cameraRight * moveHorizontal;
        swimDirection.Normalize();

        if (swimDirection.magnitude > 0)
        {
            _rb.AddForce(swimDirection * swimSpeed, ForceMode.Acceleration);
        }

        waterPhysics.ApplyBuoyancy(_rb); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == waterPhysics.gameObject) 
        {
            _isInWater = true;
            _rb.useGravity = false; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == waterPhysics.gameObject) 
        {
            _isInWater = false;
            _rb.useGravity = true; 
            _rb.drag = 0; 
        }
    }
}
