using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    PlayerController playerController;
    PlayerCombo playerCombo;
    public Animator animator;

    public Collider AttackBox;

    int combo;
    float extraWaitTime;
    float waitTime;

    float extraTimer;
    float timer;
    bool timerOnOff = false;

    bool waitOnOff = true;

    public bool isAttacking = false;
    public float timerCooldown = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerCombo = GetComponent<PlayerCombo>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //timer for attack delay
        if (timerOnOff)
        {
            timer += -Time.deltaTime;
            if (timer <= 0)
            {
                timerOnOff = false;
                timer = 0;
            }
        }
        Attack();
        //Debug.Log(waitTime);
    }

    public void StartAttack()
    {
        combo = playerCombo.GetCombo();
        isAttacking = true;
    }

    void Attack()
    {
        if (isAttacking)
        {
            timerOnOff = false;
            waitTime += Time.deltaTime;
            if (!playerController.isGrounded)
            {
                timer = 0;
                waitTime = 0;
                extraWaitTime = 0;
                waitOnOff = true;
                isAttacking = false;
                playerCombo.CleanupCombo();
            }
            else if (waitTime >= timer)
            {
                if (waitOnOff)
                {
                    waitOnOff = false;
                    AttackBox.enabled = false;

                    switch (combo)
                    {
                        case 1:
                            animator.Play("Attack1");
                            extraTimer = 0.5f;
                            break;
                        case 2:
                            animator.Play("Attack2");
                            extraTimer = 0.5f;
                            break;
                        case 3:
                            animator.Play("Attack3");
                            extraTimer = 1;
                            break;
                        default:
                            Debug.Log("combo = 0");
                            break;
                    }

                    playerController.AttackMovement();
                    AttackBox.enabled = true;
                }
                extraWaitTime += Time.deltaTime;
                if (extraWaitTime >= extraTimer)
                {
                    timer = timerCooldown;
                    waitTime = 0;
                    extraWaitTime = 0;
                    waitOnOff = true;
                    timerOnOff = true;
                    isAttacking = false;
                }
            }
        }
    }

    public float Speed(bool grounded)
    {
        if ((Input.GetKey(KeyCode.LeftShift) && grounded) && (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0))
        {
            animator.SetFloat("speed", Mathf.Lerp(animator.GetFloat("speed"), 1, Time.deltaTime * 2));
            return 1;
        }
        else if (grounded && (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0))
        {
            animator.SetFloat("speed", Mathf.Lerp(animator.GetFloat("speed"), 0.5f, Time.deltaTime * 2));
            return 0.5f;
        }
        else if (grounded)
        {
            animator.SetFloat("speed", Mathf.Lerp(animator.GetFloat("speed"), 0, Time.deltaTime * 2));
            return 0;
        }
        else
        {
            animator.SetFloat("speed", Mathf.Lerp(animator.GetFloat("speed"), 0, Time.deltaTime));
            return 0;
        }
    }

    public void Jumping()
    {
        animator.SetBool("isJumping", true);
        animator.SetBool("isLanding", false);
    }

    public void Landing()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isLanding", true);
        animator.SetBool("isFalling", false);
    }

    public void Falling()
    {
        animator.SetBool("isFalling", true);
        animator.SetBool("isLanding", false);
    }
}
