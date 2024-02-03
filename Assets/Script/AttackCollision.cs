using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    public GameObject Spark01;
    //float timer;
    //bool isAttacking;
    //bool inCollision = false;

    //float sphereRadius = 0.5f;
    //public LayerMask GroundLayers;
    //public LayerMask EnemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //StartTimer();
        //CollisionCheck();
    }

    /**void StartTimer()
    {
        if (!isAttacking) return;
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;
            isAttacking = false;
            Spark01.SetActive(false);
        }
    }

    void CollisionCheck()
    {
        Vector3 spherePosition = transform.TransformPoint(new Vector3(0, 0, 0));
        bool groundLayers = false;
        bool enemyLayers = false;
        groundLayers = Physics.CheckSphere(spherePosition, sphereRadius, GroundLayers);
        enemyLayers = Physics.CheckSphere(spherePosition, sphereRadius, EnemyLayers);
        if (groundLayers || enemyLayers)
        {
            inCollision = true;
        }

        if (inCollision)
        {
            Spark01.SetActive(true);
            isAttacking = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Vector3 spherePosition = transform.TransformPoint(new Vector3(0, 0, 0));
        Gizmos.DrawWireSphere(spherePosition, sphereRadius);
    }**/

    void OnTriggerEnter(Collider other)
    {
        Instantiate(Spark01, transform.position, transform.rotation, transform.parent);
        //Spark01.SetActive(true);
        //isAttacking = true;

        if (other.tag == "Enemy")
        {
            EnemyType1Data enemyType1Data = other.GetComponent<EnemyType1Data>();
            enemyType1Data.DeductHealth(10);
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
