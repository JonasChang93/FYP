using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public GameObject Trail;
    public Transform ShashEffect01PL;
    public GameObject slashLeft;
    public Transform ShashEffect01PR;
    public GameObject slashRight;
    float waitTime;
    float timerT;
    bool isAttackingT;
    float timerL;
    bool isAttackingL;
    float timerR;
    bool isAttackingR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartTimerT();
        StartTimerL();
        StartTimerR();
    }

    void StartTimerT()
    {
        if (!isAttackingT) return;
        timerT += Time.deltaTime;
        if (timerT >= waitTime)
        {
            waitTime = 0;
            timerT = 0;
            isAttackingT = false;
            Trail.SetActive(false);
        }
    }

    void StartTimerL()
    {
        if (!isAttackingL) return;
        timerL += Time.deltaTime;
        if (timerL >= 0.5)
        {
            timerL = 0;
            isAttackingL = false;
            slashLeft.SetActive(false);
        }
    }

    void StartTimerR()
    {
        if (!isAttackingR) return;
        timerR += Time.deltaTime;
        if (timerR >= 0.5)
        {
            timerR = 0;
            isAttackingR = false;
            slashRight.SetActive(false);
        }
    }

    void TrailStart(float waitingTime)
    {
        waitTime = waitingTime;
        Trail.SetActive(true);
        isAttackingT = true;
    }

    void SlashStart(int combo)
    {
        switch (combo)
        {
            case 1:
                slashRight.SetActive(true);
                ShashEffect01PR.localPosition = new Vector3(0, 0.4f, 0.2f);
                ShashEffect01PR.localRotation = Quaternion.Euler(0, 0, 45);
                slashRight.transform.rotation = transform.parent.rotation;
                isAttackingR = true;
                break;
            case 2:
                slashLeft.SetActive(true);
                ShashEffect01PL.localPosition = new Vector3(0, 0.4f, 0.2f);
                ShashEffect01PL.localRotation = Quaternion.Euler(0, 0, 15);
                slashLeft.transform.rotation = transform.parent.rotation;
                isAttackingL = true;
                break;
            case 3:
                slashLeft.SetActive(true);
                ShashEffect01PL.localPosition = new Vector3(0, 0.4f, 0.2f);
                ShashEffect01PL.localRotation = Quaternion.Euler(0, 0, 15);
                slashLeft.transform.rotation = transform.parent.rotation;
                isAttackingL = true;
                break;
        }
    }
}
