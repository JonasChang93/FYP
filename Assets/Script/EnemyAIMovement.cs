using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIMovement : MonoBehaviour
{
    public float alertRadius;
    public float attackRadius;

    public float movingSpeed = 0.05f;
    public float turningSpeed = 2f;

    public bool isAttacking = false;
    public float attack = 5f;

    CharacterController characterController;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = PlayerData.instance.transform.position;
        float distance = Vector3.Distance(playerPosition, transform.position);

        if (distance < alertRadius)
        {
            //Look at player
            Vector3 forwardDir = Vector3.Normalize(transform.forward);
            Vector3 playerDir = Vector3.Normalize(playerPosition - transform.position);
            float angle = Vector3.Angle(forwardDir, playerDir);
            Vector3 tagetDir = Vector3.Slerp(forwardDir, playerDir, turningSpeed / angle);
            transform.LookAt(transform.position + tagetDir);

            if (distance < attackRadius)
            {
                // Attack
                animator.SetBool("isWalking", false);
                if (!isAttacking)
                {
                    animator.Play("Attack");
                    isAttacking = true;
                    StartCoroutine(Attacking());
                }
            }
            else
            {
                // Walk towards player
                Vector3 motion = transform.forward * movingSpeed;

                animator.SetBool("isWalking", true);
                characterController.Move(motion);
            }
        }
        else
        {
            // Do nothing
            animator.SetBool("isWalking", false);
        }
    }

    IEnumerator Attacking()
    {
        //Wait then reduce health
        yield return new WaitForSeconds(0.5f);
        PlayerData.instance.DeductHealth(attack);
        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, alertRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
