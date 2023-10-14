using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    PlayerAnimator playerAnimator;

    Vector3 veloctity;

    public Transform model;
    public Camera mainCamera;

    bool onOff = true;
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
        ModelRotation1();
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
    void ModelRotation1()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        float targetRotationAngle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;

        float angleDifference = targetRotationAngle - currentRotationAngle;

        currentRotationAngle += angleDifference;

        Quaternion targetRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        if (!Input.GetKey(KeyCode.Mouse0))
        {
            if (onOff)
            {
                if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {
                    model.localRotation = Quaternion.Slerp(model.localRotation, targetRotation, rotatingSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            if (onOff)
            {
                StartCoroutine(WaitForRotate());
            }
        }
    }

    // Attack delay
    IEnumerator WaitForRotate()
    {
        onOff = false;
        ModelRotation2();
        yield return new WaitForSeconds(1);
        onOff = true;
    }

    // Rotate when you click
    void ModelRotation2()
    {
        Vector3 mousePosition = Input.mousePosition;

        Vector3 mouseDirection = mousePosition - mainCamera.WorldToScreenPoint(model.position);

        float targetRotationAngle = Mathf.Atan2(mouseDirection.x, mouseDirection.y) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0, targetRotationAngle, 0);

        model.rotation = targetRotation;
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
            groundedCounter -= 1;

            if (groundedCounter <= 0)
            {
                isGrounded = false;
            }
        }
    }
}
