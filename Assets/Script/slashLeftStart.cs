using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashLeftStart : MonoBehaviour
{
    public Transform ShashEffect01P;
    public GameObject slashLeft;
    float timer;
    bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartTimer();
    }

    void StartTimer()
    {
        if (!isAttacking) return;
        timer += Time.deltaTime;
        if (timer >= 0.5)
        {
            timer = 0;
            isAttacking = false;
            slashLeft.SetActive(false);
        }
    }

    void LeftStart(int combo)
    {
        slashLeft.SetActive(true);
        slashLeft.transform.rotation = transform.parent.rotation;
        switch (combo)
        {
            case 2:
                ShashEffect01P.localPosition = new Vector3(0, 0.4f, 0.2f);
                ShashEffect01P.localRotation = Quaternion.Euler(0, 0, 15);
                break;
            case 3:
                ShashEffect01P.localPosition = new Vector3(0, 0.4f, 0.2f);
                ShashEffect01P.localRotation = Quaternion.Euler(0, 0, 15);
                break;
        }
        isAttacking = true;
    }
}
