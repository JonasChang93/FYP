using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.Image;

public class EnemyAIMovement : MonoBehaviour
{
    Vector3 newPlayerPosition;

    float alertRadius = 10;
    float attackRadius = 2;

    bool isTracking = false;
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
        //Distance
        Vector3 playerPosition = PlayerData.instance.transform.position;
        float distance = Vector3.Distance(playerPosition, transform.position);

        //Ray
        Vector3 origin = transform.position + new Vector3(0, 1.5f);
        Vector3 direction = Vector3.Normalize(playerPosition - transform.position);
        Ray ray = new Ray(origin, direction);

        //Vector3 direction
        Vector3 playerDir = Vector3.Normalize(playerPosition - transform.position);
        Vector3 enemyDir = transform.forward;
        float cosAngle = Vector3.Dot(playerDir, enemyDir) * Mathf.Rad2Deg;

        if (distance < alertRadius)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Player" && cosAngle > 30)
            {
                isTracking = true;
                newPlayerPosition = playerPosition;

                if (distance < attackRadius)
                {
                    Rotate(enemyDir, playerDir);
                    //Attack
                    animator.SetBool("isWalking", false);
                    if (!isAttacking)
                    {
                        isAttacking = true;
                        animator.Play("Attack");
                        StartCoroutine(Attacking());
                        agent.ResetPath();
                    }
                }
                else
                {
                    Walk();
                }
            }
            else if (isTracking)
            {
                if (distance < attackRadius)
                {
                    Rotate(enemyDir, playerDir);
                }
                else
                {
                    Walk();
                    if (Vector3.Distance(transform.position, newPlayerPosition) < 0.1f)
                    {
                        EndTeacking();
                    }
                }
            }
            else
            {
                //Do nothing
                animator.SetBool("isWalking", false);
                agent.ResetPath();
            }
        }
        else
        {
            EndTeacking();
        }
    }

    void Walk()
    {
        //Walk towards player
        animator.SetBool("isWalking", true);
        agent.SetDestination(newPlayerPosition);
    }

    void Rotate(Vector3 enemyDir, Vector3 playerDir)
    {
        //Rotate to player
        Vector3 targetDir = Vector3.Slerp(enemyDir, playerDir, Time.deltaTime);
        targetDir.y = 0;
        transform.LookAt(transform.position + targetDir);
    }

    void EndTeacking()
    {
        isTracking = false;
        Debug.Log(isTracking);
        //Do nothing
        animator.SetBool("isWalking", false);
        agent.ResetPath();
    }

    IEnumerator Attacking()
    {
        //Wait then reduce health
        PlayerData.instance.DeductHealth(attack);
        yield return new WaitForSeconds(1);

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
