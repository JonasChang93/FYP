using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.Image;

public class EnemyAIMovement : MonoBehaviour
{
    Vector3 newPlayerPosition;
    Vector3 destination;
    Vector3 location1 = new Vector3(0, 0, 10);
    Vector3 location2 = new Vector3(0, 0, -10);
    Vector3 location3 = new Vector3(10, 0, 10);

    float alertRadius = 16;
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

        destination = location1;
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

        RaycastHit hit;
        if (distance < alertRadius)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if ((hit.collider.tag == "Player" && cosAngle > 30) || (hit.collider.tag == "Player" && isTracking))
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
                    Walk();
                    if (Vector3.Distance(transform.position, newPlayerPosition) < 0.1f)
                    {
                        EndTeacking();
                    }
                }
                else
                {
                    if (distance < attackRadius)
                    {
                        Rotate(enemyDir, playerDir);

                        //Do nothing
                        animator.SetBool("isWalking", false);
                        agent.ResetPath();
                    }
                    else if (!isTracking)
                    {
                        Patrol();
                    }
                }
            }
        }
        else
        {
            EndTeacking();

            Patrol();
        }
    }

    void Patrol()
    {
        //Patrol to destination
        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            if (destination == location1)
            {
                destination = location2;
            }
            else if (destination == location2)
            {
                destination = location3;
            }
            else if (destination == location3)
            {
                destination = location1;
            }
        }

        animator.SetBool("isWalking", true);
        agent.SetDestination(destination);
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
        Vector3 targetDir = Vector3.Slerp(enemyDir, playerDir, Time.deltaTime * 2);
        targetDir.y = 0;
        transform.LookAt(transform.position + targetDir);
    }

    void EndTeacking()
    {
        isTracking = false;
        //Do nothing
        animator.SetBool("isWalking", false);
        agent.ResetPath();
    }

    public void StartTeacking()
    {
        isTracking = true;
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
