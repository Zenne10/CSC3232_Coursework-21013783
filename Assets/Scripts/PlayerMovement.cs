using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float stealthSpeed = 2f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float airControlMultiplier = 0.5f; 
    [SerializeField] private float gravityScale = 2f;          
    [SerializeField] private float fallMultiplier = 3f;       
    [SerializeField] private float jumpDrag = 1f;              

    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private ParticleSystem dustEffect; 

    private Rigidbody _rb;
    private bool _isGrounded;
    private bool _canDoubleJump;
    private bool _isSprinting;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 2f);

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 cameraRight = playerCamera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        Vector3 movement = cameraForward * moveVertical + cameraRight * moveHorizontal;

        float currentSpeed = speed;
        bool wasSprinting = _isSprinting;

        if (Input.GetKey(KeyCode.LeftShift) && movement != Vector3.zero)
        {
            currentSpeed = sprintSpeed;
            _isSprinting = true;
        }
        else
        {
            _isSprinting = false;
        }

        if (_isSprinting && !wasSprinting && _isGrounded)
        {
            PlayDustEffect();
        }
        else if (!movement.Equals(Vector3.zero) && wasSprinting && !_isSprinting)
        {
            StopDustEffect();
        }

        if (_isGrounded)
        {
            _rb.velocity = new Vector3(movement.x * currentSpeed, _rb.velocity.y, movement.z * currentSpeed);
        }
        else
        {
            _rb.velocity += new Vector3(
                movement.x * currentSpeed * airControlMultiplier * Time.deltaTime,
                0,
                movement.z * currentSpeed * airControlMultiplier * Time.deltaTime
            );

            Vector3 horizontalVelocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            _rb.AddForce(-horizontalVelocity * jumpDrag * Time.deltaTime, ForceMode.VelocityChange);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isGrounded)
            {
                Jump(currentSpeed);
                _canDoubleJump = true;
            }
            else if (_canDoubleJump)
            {
                Jump(currentSpeed);
                _canDoubleJump = false;
            }
        }

        if (_isGrounded) _canDoubleJump = true;
        ApplyRealisticGravity();
    }

    private void Jump(float horizontalSpeed)
    {
        float adjustedJumpForce = jumpForce + (horizontalSpeed * 0.2f);
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z); 
        _rb.AddForce(Vector3.up * adjustedJumpForce, ForceMode.Impulse);
    }

    private void ApplyRealisticGravity()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rb.velocity.y > 0)
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * (gravityScale - 1) * Time.deltaTime;
        }
    }

    private void PlayDustEffect()
    {
        if (dustEffect != null && !dustEffect.isPlaying)
        {
            dustEffect.Play();
        }
    }

    private void StopDustEffect()
    {
        if (dustEffect != null && dustEffect.isPlaying)
        {
            dustEffect.Stop();
        }
    }
}
