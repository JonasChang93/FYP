using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttactEnemy : Interactable
{
    bool isAttacking = false;

    public override void Interact()
    {
        base.Interact();

        if (!isAttacking)
        {
            Debug.Log("Attack");
            PlayerData.instance.GetComponent<Animator>().Play("Attack");
            isAttacking = true;

            StartCoroutine(Attacting());
        }
    }

    IEnumerator Attacting()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<PlayerData>().DeductHealth(PlayerData.instance.attack);
        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
    }
}
