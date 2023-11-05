using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public Slider healthBar;
    public Slider expBar;
    public Text levelUI;

    public bool isPlayer;

    public float attack;
    public float defense;

    float curHealth = 100;
    float maxHealth = 100;
    float exp;
    float maxExp = 10;
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
        UpdateUI();
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

    void LevelCheck()
    {
        if (exp >= maxExp)
        {
            levels += 1;
            levelUP = true;
            exp = 0;
            maxExp *= 2;

            if (levelUP)
            {
                maxExp += 5;
                attack += 5;
                defense += 5;

                UpdateUI();
                Banner.instance.PlayBanner("You level up!");
                levelUP = false;
            }
        }
        
    }

    void UpdateUI()
    {
        levelUI.text = "Level: " + Convert.ToString(levels);
        healthBar.value = curHealth / maxHealth;
        expBar.value = exp / maxExp;
    }
}
