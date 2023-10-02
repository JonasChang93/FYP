using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    PlayerAnimator playerAnimator;

    Vector3 veloctity;

    public Transform mainCamera;
    public Transform model;

    bool isGrounded;
    float currentRotationAngle = 0.0f;

    float movingSpeed = 10f;
    float rotatingSpeed = 5f;
    float rotatingSpeedX;
    float rotatingSpeedY;
    float jumpForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<PlayerAnimator>();

        LoadData();
    }

    void LoadData()
    {
        rotatingSpeedX = SaveLoadManager.instance.mouseX;
        rotatingSpeedY = SaveLoadManager.instance.mouseY;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        ModelRotation();
        CameraRotation();
    }

    void Move()
    {
        Vector3 motion = transform.forward * Input.GetAxis("Vertical") * movingSpeed + transform.right * Input.GetAxis("Horizontal") * movingSpeed;
        characterController.Move(motion * (Input.GetKey(KeyCode.LeftShift) == true ? 2f : 1f) * Time.deltaTime);

        playerAnimator.Walking();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            veloctity.y = jumpForce;
            isGrounded = false;
            
            playerAnimator.Jumping();
        }

        characterController.Move(veloctity * Time.deltaTime);

        if (isGrounded)
        {
            veloctity.y = 0f;

            playerAnimator.Landing();
        }
        else
        {
            veloctity.y += -10f * Time.deltaTime;

            playerAnimator.Falling();
        }
    }

    void ModelRotation()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        
        float targetRotationAngle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;

        float angleDifference = targetRotationAngle - currentRotationAngle;

        currentRotationAngle += angleDifference;

        Quaternion targetRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            model.localRotation = Quaternion.Slerp(model.localRotation, targetRotation, rotatingSpeed * Time.deltaTime);
        }
    }

    void CameraRotation()
    {
        mainCamera.transform.Rotate(0f, 0f, ClampAngle(Input.GetAxis("Mouse Y")));
        transform.Rotate(0f, Input.GetAxis("Mouse X") * rotatingSpeedX, 0f);
    }
    float ClampAngle(float input)
    {
        float xRotation = mainCamera.localEulerAngles.z;

        if (xRotation + input * rotatingSpeedY <= 90f || xRotation + input * rotatingSpeedY >= 270f)
        {
            return input * rotatingSpeedY;
        }
        else if (xRotation + input * rotatingSpeedY > 90 && xRotation + input * rotatingSpeedY < 180)
        {
            Debug.Log("90");
            return 90f - xRotation;
        }
        else
        {
            Debug.Log("270");
            return 270f - xRotation;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject != gameObject)
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject != gameObject)
        {
            isGrounded = false;
        }
    }
}
