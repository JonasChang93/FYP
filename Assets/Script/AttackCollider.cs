using System.Collections;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    new Collider collider;
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        
    }

    public void AttackStart()
    {
        collider.enabled = true;
    }
    public void AttackEnd()
    {
        collider.enabled = false;
    }
}
