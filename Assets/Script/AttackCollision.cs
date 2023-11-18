using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    PlayerCombo playerCombo;

    // Start is called before the first frame update
    void Start()
    {
        playerCombo = GetComponent<PlayerCombo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (playerCombo.isAttacking)
            {
                EnemyType1Data enemyType1Data = other.GetComponent<EnemyType1Data>();
                enemyType1Data.DeductHealth(10);
            }
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
