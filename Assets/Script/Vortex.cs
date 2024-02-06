using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour
{
    float timer;
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
        timer += Time.deltaTime;
        if (timer >= 0.5)
        {
            timer = 0;
            GameObject.Destroy(gameObject);
        }
    }
}
