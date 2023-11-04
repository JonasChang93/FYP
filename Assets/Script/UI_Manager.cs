using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public delegate void VoidFunctions();
    public VoidFunctions VoidUpdateUIFunctions;

    public static UI_Manager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UI_Update()
    {
        VoidUpdateUIFunctions();
    }
}
