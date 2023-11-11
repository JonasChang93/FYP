using System.Collections;
using UnityEngine;

public class EnemyBeAttack : MonoBehaviour
{
    bool isAttacking = false;

    public int combo;
    float comboCooldown;

    void Update()
    {
        if (isAttacking || comboCooldown > 0)
        {
            comboCooldown += Time.deltaTime;
            if (comboCooldown > 0.25)
            {
                comboCooldown = 0;
                combo = 0;
            }
        }
    }

    public void BeAttack()
    {
        comboCooldown = 0;

        if (!isAttacking)
        {
            Debug.Log("Attack");
            isAttacking = true;

            StartCoroutine(Attacting());
        }
    }

    IEnumerator Attacting()
    {
        GetComponent<EnemyType1Data>().DeductHealth(PlayerData.instance.attack);

        combo += 1;
        if (combo > 3)
        {
            combo = 0;
            comboCooldown = 0;
        }

        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
}
