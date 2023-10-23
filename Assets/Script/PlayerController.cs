using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    PlayerAnimator playerAnimator;

    Vector3 veloctity;

    public Transform model;
    public Transform mainCameraParent;

    bool isGrounded;
    float movingSpeed = 10;
    float rotatingSpeed = 10;
    float jumpForce = 5;
    float currentRotationAngle;
    float groundedCounter;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<PlayerAnimator>();
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

        characterController.Move(veloctity * Time.deltaTime);
    }

    // Rotate when you WASD
    void ModelRotation()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        float targetRotationAngle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;

        float angleDifference = targetRotationAngle - currentRotationAngle;

        currentRotationAngle += angleDifference;

        Quaternion targetRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            model.localRotation = Quaternion.Slerp(model.localRotation, targetRotation, rotatingSpeed * Time.deltaTime);
        }
    }

    void CameraRotation()
    {
        mainCameraParent.Rotate(0f, 0f, ClampAngle(Input.GetAxis("Mouse Y")));
        transform.Rotate(0f, Input.GetAxis("Mouse X") * rotatingSpeed * SaveLoadManager.instance.offsetX, 0f);
    }

    //Clamp angle before rotate
    float ClampAngle(float input)
    {
        float xRotation = mainCameraParent.localEulerAngles.z;
        float finalSpeed = input * rotatingSpeed * SaveLoadManager.instance.offsetY;

        if (xRotation + finalSpeed <= 90f || xRotation + finalSpeed >= 270f)
        {
            return finalSpeed;
        }
        else if (xRotation + finalSpeed > 90 && xRotation + finalSpeed < 180)
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != gameObject)
        {
            groundedCounter += 1;
            isGrounded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject != gameObject)
        {
            groundedCounter += -1;

            if (groundedCounter <= 0)
            {
                isGrounded = false;
            }
        }
    }
}
