using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public Slider healthBar;
    public Slider ExpBar;
    public Text levelUI;

    public bool isPlayer;
    public bool isAttacking = false;
    public float attack;
    public float defense;

    float maxHealth = 100;
    float curHealth = 100;
    float exp;
    float expCount = 10;
    float levels;
    bool levelUP;

    public static PlayerData instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        levelUI.text = "Level: " + Convert.ToString(levels);
        ExpBar.value = exp / expCount;
    }

    // Update is called once per frame
    void Update()
    {
        LevelsSystem();
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
                //Banner.instance.PlayBanner("You die!");
            }
            else
            {
                isAttacking = false;
                Destroy(gameObject);
            }
        }
    }

    void LevelsSystem()
    {
        if (exp >= expCount)
        {
            levels += 1;
            levelUP = true;
            exp = 0;
            expCount *= 2;
        }
        if (levelUP)
        {
            LevelsUP();
        }
    }

    void LevelsUP()
    {
        attack += 5;
        defense += 5;
        levelUI.text = "Level: " + Convert.ToString(levels);
        ExpBar.value = exp / expCount;
        Banner.instance.PlayBanner("You level up!");
        levelUP = false;
    }
}
