using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(int combo)
    {
        switch (combo)
        {
            case 0:
                animator.Play("Attack1");
                break;
            case 1:
                animator.Play("Attack2");
                break;
            case 2:
                animator.Play("Attack3");
                break;
            default:
                animator.Play("Attack1");
                break;
        }
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
