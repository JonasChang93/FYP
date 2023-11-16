using System.Collections;
using UnityEngine;

public class PlayerCombo : MonoBehaviour
{
    PlayerAnimator playerAnimator;

    float comboCooldown;
    int combo;

    private void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        if (!playerAnimator.isAttacking)
        {
            comboCooldown += Time.deltaTime;
            if (comboCooldown >= playerAnimator.timerCooldown)
            {
                comboCooldown = 0;
                combo = 0;
            }
        }
        else
        {
            comboCooldown = 0;
        }
    }

    public int GetCombo()
    {
        comboCooldown = 0;

        if (!playerAnimator.isAttacking)
        {
            combo += 1;
            if (combo > 3)
            {
                combo = 1;
                comboCooldown = 0;
            }
            return combo;
        }

        return combo;
    }
}
