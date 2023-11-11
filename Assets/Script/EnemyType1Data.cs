using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class EnemyType1Data : MonoBehaviour
{
    public Slider healthBar;

    public bool isPlayer = false;

    [HideInInspector] public float attack;
    [HideInInspector] public float defense;
    [HideInInspector] public float curHealth = 100;
    [HideInInspector] public float maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeductHealth(float health)
    {
        curHealth -= health;
        curHealth = Mathf.Max(curHealth, 0f);
        healthBar.value = curHealth / maxHealth;

        if (curHealth <= 0)
        {
            if (isPlayer)
            {
                Banner.instance.PlayBanner("You die!");
            }
            else
            {
                //isAttacking = false;
                Destroy(gameObject);
            }
        }
    }
}
