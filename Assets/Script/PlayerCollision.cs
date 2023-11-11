using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerAnimator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyBeAttack enemyBeAttack = other.GetComponent<EnemyBeAttack>();
            enemyBeAttack.BeAttack();
            playerAnimator.Attack(enemyBeAttack.combo);
        }
    }

    void OnTriggerExit(Collider other)
    {

    }

    void OnTriggerStay(Collider other)
    {

    }

    void OnCollisionEnter(Collision collision)
    {

    }

    void OnCollisionExit(Collision collision)
    {

    }

    void OnCollisionStay(Collision collision)
    {

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {

    }
}
