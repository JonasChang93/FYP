using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttactEnemy : Interactable
{
    public override void Interact()
    {
        base.Interact();

        if (!PlayerData.instance.isAttacking)
        {
            Debug.Log("Attack");
            PlayerData.instance.GetComponent<Animator>().Play("Attack");
            PlayerData.instance.isAttacking = true;

            StartCoroutine(Attacting());
        }
    }

    IEnumerator Attacting()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<PlayerData>().DeductHealth(PlayerData.instance.attack);
        yield return new WaitForSeconds(0.5f);

        PlayerData.instance.isAttacking = false;
    }
}
