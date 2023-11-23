using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    PlayerController playerController;
    Animator animator;

    public AttackCollider attackCollider;

    float timer;
    bool timerOnOff = false;

    public bool isAttacking = false;
    public float timerCooldown = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
    }

    public void Attack(int combo)
    {
        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(Attacking(combo));
        }
    }

    IEnumerator Attacking(int combo)
    {
        timerOnOff = false;
        //waitTime before animation start(input time)
        float waitTime = 0;
        while (waitTime < timer)
        {
            waitTime += Time.deltaTime;
            if (!playerController.isGrounded)
            {
                timer = 0;
                isAttacking = false;
                playerController.isAttacking = false;
                yield break;
            }
            yield return null;
        }

        timer = timerCooldown;
        attackCollider.AttackEnd();

        float extraWaitTime = 0;

        switch (combo)
        {
            case 1:
                animator.Play("Attack1");
                playerController.AttackMovement();
                extraWaitTime = 0.5f;
                break;
            case 2:
                animator.Play("Attack2");
                playerController.AttackMovement();
                extraWaitTime = 0.5f;
                break;
            case 3:
                animator.Play("Attack3");
                playerController.AttackMovement();
                extraWaitTime = 1;
                break;
            default:
                Debug.Log("combo = 0");
                break;
        }

        playerController.isAttacking = true;
        attackCollider.AttackStart();
        //waitTime after animation start(basic timer)
        waitTime = 0;
        while (waitTime < extraWaitTime)
        {
            waitTime += Time.deltaTime;
            if (!playerController.isGrounded)
            {
                timer = 0;
                isAttacking = false;
                playerController.isAttacking = false;
                yield break;
            }
            yield return null;
        }

        timerOnOff = true;
        isAttacking = false;
    }


    public float Movement(bool grounded)
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
