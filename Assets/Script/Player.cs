using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

 
	
	public float walkSpeed = 3f;
    public float runSpeed = 6f;

    public float jumpHeight = 1f;
    public float gravity = -11f;

    public float speedSmoothTime = 0.2f;
    public float rotSmoothTime = 0.1f;

    private float speedSmoothVelocity;
    private float rotSmoothVelocity;

    private float currentHorizontalSpeed;

    private Vector2 directionInput;
    private float xInput;
    private float yInput;

    private bool isRunning = false;

    private Vector3 currentVelocity;
    private float yVelocity;
    private float xzVelocity;

    private Transform cameraTransform;
    private CharacterController characterController;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        

        directionInput = new Vector2(xInput, yInput).normalized;

        yVelocity += gravity * Time.deltaTime;
        
        if (directionInput != Vector2.zero)
        {
            Rotate();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        Movement();

    }

    private void Jump()
    {
       
        if (characterController.isGrounded)
        {
            yVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
        }
    }

    private void Movement()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift);

        float targetSpeed = ((isRunning == true) ? runSpeed : walkSpeed) * directionInput.magnitude;
        xzVelocity = Mathf.SmoothDamp(xzVelocity, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);


        currentVelocity = transform.forward * xzVelocity + Vector3.up * yVelocity;
        characterController.Move(currentVelocity * Time.deltaTime);
        xzVelocity = new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;

        if (characterController.isGrounded)
            yVelocity = -0.00001f;
    }

    private void Rotate()
    {
        float targetRot = Mathf.Atan2(directionInput.x, directionInput.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        float smoothedRot = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRot, ref rotSmoothVelocity, rotSmoothTime);
        transform.localEulerAngles = Vector3.up * smoothedRot;
    }
}
