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

    bool isGrounded;
    float movingSpeed = 10f;
    float rotatingSpeed = 10f;
    float jumpForce = 10f;

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
        Vector3 mousePosition = Input.mousePosition;

        Vector3 mouseDirection = mousePosition - mainCamera.WorldToScreenPoint(model.position);

        float targetRotationAngle = Mathf.Atan2(mouseDirection.x, mouseDirection.y) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0, targetRotationAngle, 0);

        model.rotation = Quaternion.Slerp(model.rotation, targetRotation, rotatingSpeed * Time.deltaTime);
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
