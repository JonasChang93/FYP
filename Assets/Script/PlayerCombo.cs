using System.Collections;
using UnityEngine;

public class PlayerCombo : MonoBehaviour
{
    PlayerAnimator playerAnimator;
    PlayerController playerController;

    public Collider AttackBox;

    float comboCooldown;
    int combo;
    bool cooldownOnOff = false;

    public bool isAttacking = false;

    private void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (cooldownOnOff)
        {
            if (!playerAnimator.isAttacking)
            {
                comboCooldown += Time.deltaTime;
                if (comboCooldown >= playerAnimator.timerCooldown)
                {
                    CleanupCombo();
                }
            }
            else
            {
                comboCooldown = 0;
                isAttacking = true;
            }
            //Debug.Log(comboCooldown);
        }
    }

    public void CleanupCombo()
    {
        comboCooldown = 0;
        combo = 0;
        isAttacking = false;
        cooldownOnOff = false;
        playerController.isAttacking = false;
        AttackBox.enabled = false;
    }

    public int GetCombo()
    {
        comboCooldown = 0;
        cooldownOnOff = true;

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
