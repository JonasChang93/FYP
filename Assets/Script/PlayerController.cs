using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    PlayerAnimator playerAnimator;
    PlayerCombo playerCombo;

    Vector3 velocity;
    Vector3 velocityXZ;
    Vector3 targetPosition;

    public Transform model;
    public Transform CameraRotateY;
    public Transform CameraRotateZ;

    public bool isAttacking = false;
    public bool isGrounded;
    bool topCollision;

    float movingSpeed = 2;
    float jumpForce = 5;
    float jumpCooldown;

    float currentRotationAngle;

    float SphereRadius = 0.25f;
    public LayerMask GroundLayers;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerCombo = GetComponent<PlayerCombo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            GroundCheck();
            Attack();
            Move();
            Jump();
            ModelRotation();
            CameraRotation();
        }
        //Debug.Log(velocityXZ);
    }

    public void GroundCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition1 = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        isGrounded = Physics.CheckSphere(spherePosition1, SphereRadius, GroundLayers, QueryTriggerInteraction.Ignore);

        Vector3 spherePosition2 = new Vector3(transform.position.x, transform.position.y + 1.6f, transform.position.z);
        topCollision = Physics.CheckSphere(spherePosition2, SphereRadius, GroundLayers, QueryTriggerInteraction.Collide);
        if (topCollision) velocity.y = 0;
    }

    public void OnDrawGizmosSelected()
    {
        //Top sphere
        Color transparentGreen1 = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed1 = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (topCollision) Gizmos.color = transparentGreen1;
        else Gizmos.color = transparentRed1;

        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + 1.6f, transform.position.z), SphereRadius);

        //Bottom sphere
        Color transparentGreen2 = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed2 = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (isGrounded) Gizmos.color = transparentGreen2;
        else Gizmos.color = transparentRed2;

        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), SphereRadius);
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isGrounded)
        {
            playerAnimator.Attack(playerCombo.GetCombo());
        }

        if (isAttacking && playerCombo.isAttacking)
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 2);
            characterController.Move(smoothedPosition - transform.position);
        }
    }

    public void AttackMovement()
     {
        velocityXZ = Vector3.zero;

        Vector3 targetPosition = model.TransformPoint(model.localPosition + new Vector3(0, 0, 2));
        this.targetPosition = targetPosition;
         //StartCoroutine(AttackCoroutine(attackDuration, targetPosition));
     }

    /**IEnumerator AttackCoroutine(float attackDuration, Vector3 targetPosition)
    {
        float durationCounter = 0;
        while (durationCounter < attackDuration)
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 2);
            characterController.Move(smoothedPosition - transform.position);
            durationCounter += Time.deltaTime;
            yield return null;
        }
    }**/

    void Move()
    {
        if (isGrounded && !playerCombo.isAttacking)
        {
            Vector3 motion = -CameraRotateY.right * Input.GetAxisRaw("Vertical") * movingSpeed + CameraRotateY.forward * Input.GetAxisRaw("Horizontal") * movingSpeed;
            velocityXZ = Vector3.Lerp(velocityXZ, motion * playerAnimator.Movement(isGrounded), Time.deltaTime * 4);

            characterController.Move(velocityXZ * Time.deltaTime);
        }
        else if (!playerCombo.isAttacking)
        {
            velocityXZ = Vector3.Lerp(velocityXZ, Vector3.zero, Time.deltaTime);
            playerAnimator.Movement(isGrounded);

            //characterController.Move(velocityXZ * Time.deltaTime);
        }
        characterController.Move(velocityXZ * Time.deltaTime + velocity * Time.deltaTime);
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
                velocity.y = jumpForce;

                isGrounded = false;

                playerAnimator.Jumping();
            }
        }

        if (isGrounded)
        {
            if (velocity.y <= 0)
            {
                velocity.y = -10;

                playerAnimator.Landing();
            }
        }
        else
        {
            jumpCooldown = 1;

            velocity.y += -10 * Time.deltaTime;

            playerAnimator.Falling();
        }

        //characterController.Move(velocity * Time.deltaTime);
    }

    // Rotate when you WASD
    void ModelRotation()
    {
        if (!playerCombo.isAttacking)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            float targetRotationAngle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;

            float angleDifference = targetRotationAngle - currentRotationAngle;

            currentRotationAngle += angleDifference;

            Quaternion targetRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                model.localRotation = Quaternion.Slerp(model.localRotation, targetRotation * Quaternion.LookRotation(-CameraRotateY.right), 10 * Time.deltaTime);
            }
        }
    }

    void CameraRotation()
    {
        CameraRotateZ.Rotate(0, 0, ClampAngle(Input.GetAxis("Mouse Y")));
        CameraRotateY.Rotate(0, Input.GetAxis("Mouse X") * SaveLoadSettingManager.instance.rotatingSpeed * SaveLoadSettingManager.instance.offsetX, 0);
    }

    //Clamp angle before rotate
    float ClampAngle(float input)
    {
        float xRotation = CameraRotateZ.localEulerAngles.z;
        float finalSpeed = input * SaveLoadSettingManager.instance.rotatingSpeed * SaveLoadSettingManager.instance.offsetY;

        if (xRotation + finalSpeed <= 90 || xRotation + finalSpeed >= 270)
        {
            return finalSpeed;
        }
        else if (xRotation + finalSpeed > 90 && xRotation + finalSpeed < 180)
        {
            //Debug.Log("90");
            return 90 - xRotation;
        }
        else
        {
            //Debug.Log("270");
            return 270 - xRotation;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject != gameObject && !isGrounded)
        {
            velocityXZ = Vector3.zero;
        }
    }
}
