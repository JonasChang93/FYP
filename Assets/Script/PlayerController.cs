using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    PlayerAnimator playerAnimator;

    Vector3 veloctity;
    Vector3 veloctityXZ;

    public Transform model;
    public Transform CameraRotateY;
    public Transform CameraRotateZ;

    bool isGrounded;
    float movingSpeed = 10;
    float rotatingSpeed = 10;
    float jumpForce = 5;
    float currentRotationAngle;
    float groundedCounter;
    float jumpCooldown;

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
        if (isGrounded)
        {
            Vector3 motion = -CameraRotateY.right * Input.GetAxisRaw("Vertical") * movingSpeed + CameraRotateY.forward * Input.GetAxisRaw("Horizontal") * movingSpeed;
            veloctityXZ = Vector3.Lerp(motion * playerAnimator.WalkOrRun(), Vector3.zero, Time.deltaTime);

            characterController.Move(veloctityXZ * Time.deltaTime);
        }
        else
        {
            characterController.Move(veloctityXZ * Time.deltaTime);
        }
    }

    void Jump()
    {
        if (jumpCooldown > 0)
        {
            jumpCooldown += -Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (jumpCooldown <= 0)
            {
                veloctity.y = jumpForce;

                isGrounded = false;

                playerAnimator.Jumping();
            }
        }

        if (isGrounded)
        {
            veloctity.y = 0f;

            playerAnimator.Landing();
        }
        else
        {
            jumpCooldown = 1;

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
            model.localRotation = Quaternion.Slerp(model.localRotation, targetRotation * Quaternion.LookRotation(-CameraRotateY.right), rotatingSpeed * Time.deltaTime);
        }
    }

    void CameraRotation()
    {
        CameraRotateZ.Rotate(0f, 0f, ClampAngle(Input.GetAxis("Mouse Y")));
        CameraRotateY.Rotate(0f, Input.GetAxis("Mouse X") * rotatingSpeed * SaveLoadManager.instance.offsetX, 0f);
    }

    //Clamp angle before rotate
    float ClampAngle(float input)
    {
        float xRotation = CameraRotateZ.localEulerAngles.z;
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
