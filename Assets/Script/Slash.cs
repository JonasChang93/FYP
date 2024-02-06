using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public GameObject Trail01;
    public GameObject Trail02;
    public Transform AttackBox;
    public GameObject Slash01;
    public GameObject Slash02;
    public Transform Player;
    public GameObject Vortex01;
    public GameObject Vortex02;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TrailStart(float waitTime)
    {
        switch (waitTime)
        {
            case 1:
                Instantiate(Trail01, AttackBox.position, AttackBox.rotation, AttackBox.parent);
                break;
            case 1.5f:
                Instantiate(Trail02, AttackBox.position, AttackBox.rotation, AttackBox.parent);
                break;
        }
    }

    void SlashStart(int combo)
    {
        Transform modelBox = transform.parent;
        switch (combo)
        {
            case 1:
                Instantiate(Slash02, modelBox.TransformPoint(-0.3f, 1.5f, 0.3f), Quaternion.Euler(0, modelBox.eulerAngles.y, 45), Player);
                Instantiate(Vortex02, modelBox.position, modelBox.rotation, Player);
                break;
            case 2:
                Instantiate(Slash01, modelBox.TransformPoint(0, 1.5f, 0.3f), Quaternion.Euler(0, modelBox.eulerAngles.y, 15), Player);
                Instantiate(Vortex01, modelBox.position, modelBox.rotation, Player);
                break;
            case 3:
                Instantiate(Slash01, modelBox.TransformPoint(0, 1.5f, 0.3f), Quaternion.Euler(0, modelBox.eulerAngles.y, 20), Player);
                Instantiate(Vortex01, modelBox.position, modelBox.rotation, Player);
                break;
        }
    }
}
