using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;

    float timer = 0.5f;
    bool timerOnOff = false;

    public bool isAttacking = false;
    public float timerCooldown = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
        switch (combo)
        {
            case 1:
                timerOnOff = false;
                yield return new WaitForSeconds(timer);
                timer = timerCooldown;
                animator.Play("Attack1");
                yield return new WaitForSeconds(0.5f);
                timerOnOff = true;
                break;
            case 2:
                timerOnOff = false;
                yield return new WaitForSeconds(timer);
                timer = timerCooldown;
                animator.Play("Attack2");
                yield return new WaitForSeconds(0.5f);
                timerOnOff = true;
                break;
            case 3:
                timerOnOff = false;
                yield return new WaitForSeconds(timer);
                timer = timerCooldown;
                animator.Play("Attack3");
                yield return new WaitForSeconds(1);
                timerOnOff = true;
                break;
            default:
                Debug.Log("combo = 0");
                break;
        }
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
    }
}
