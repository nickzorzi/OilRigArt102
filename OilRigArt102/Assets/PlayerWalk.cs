using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerWalk : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        #region Handles Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Handles Jumping

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity -= Time.deltaTime;
        }    

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if(canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0 , 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion
    }
    
    
    // [Header("Movement Parameters")]
    // [SerializeField] private float walkSpeed = 3.0f;
    // [SerializeField] private float gravity = 30.0f;

    // [Header("Look Parameters")]
    // [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    // [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    // [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    // [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    // private Camera playerCamera;
    // private CharacterController characterController;

    // private Vector3 moveDirection;
    // private Vector2 currentInput;

    // private float rotationX = 0;

    // void Awake()
    // {
    //     playerCamera = GetComponentInChildren<Camera>();
    //     characterController = GetComponent<CharacterController>();

    //     Cursor.lockState = CursorLockMode.Locked;
    //     Cursor.visible = false;
    // }

    // void Update()
    // {
    //         HandleMovementInput();
    //         HandleMouseLook();
    //         ApplyFinalMovement();
    // }

    // private void HandleMovementInput() //Keyboard Controller
    // {
    //     currentInput = new Vector2((walkSpeed) * Input.GetAxis("Vertical"), (walkSpeed) * Input.GetAxis("Horizontal"));

    //     // Debug.Log("Vertical Input: " + currentInput.x);
    //     // Debug.Log("Horizontal Input: " + currentInput.y);

    //     float moveDirectionY = moveDirection.y;
    //     moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
    //     moveDirection.y = moveDirectionY;
    // }

    // private void HandleMouseLook()
    // {
    //     rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
    //     rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
    //     playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    //     transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    // }

    // private void ApplyFinalMovement()
    // {
    //     if (!characterController.isGrounded)
    //     {
    //         moveDirection.y -= gravity * Time.deltaTime;
    //     }
    //     else
    //     {
    //         //Debug.Log("Grounded");
    //     }

    //     characterController.Move(moveDirection * Time.deltaTime);

    //     //Debug.Log("Move Direction: " + moveDirection);
    // }
}
