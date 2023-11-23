using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIMovement : MonoBehaviour
{
    float alertRadius = 10;
    float attackRadius = 2;
    float turningSpeed = 2;

    bool isAttacking = false;
    float attack = 5;

    Animator animator;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = PlayerData.instance.transform.position;
        float distance = Vector3.Distance(playerPosition, transform.position);

        if (distance < alertRadius)
        {
            // Look at player
            Vector3 forwardDir = Vector3.Normalize(transform.forward);
            Vector3 playerDir = Vector3.Normalize(playerPosition - transform.position);
            forwardDir.y = 0;
            playerDir.y = 0;
            float angle = Vector3.Angle(forwardDir, playerDir);
            Vector3 targetDir = Vector3.Slerp(forwardDir, playerDir, turningSpeed / angle);
            transform.LookAt(transform.position + targetDir);

            if (distance < attackRadius)
            {
                // Attack
                animator.SetBool("isWalking", false);
                if (!isAttacking)
                {
                    animator.Play("Attack");
                    isAttacking = true;
                    StartCoroutine(Attacking());
                    agent.ResetPath();
                }
            }
            else
            {
                // Walk towards player
                animator.SetBool("isWalking", true);
                agent.SetDestination(playerPosition);
            }
        }
        else
        {
            // Do nothing
            animator.SetBool("isWalking", false);
            agent.ResetPath();
        }
    }

    IEnumerator Attacking()
    {
        // Wait then reduce health
        yield return new WaitForSeconds(0.5f);
        PlayerData.instance.DeductHealth(attack);
        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
