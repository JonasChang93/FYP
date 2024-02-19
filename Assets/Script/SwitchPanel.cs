using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPanel : MonoBehaviour
{
    public RectTransform Page1;
    public RectTransform Page2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToPage1()
    {
        Page1.SetAsLastSibling();
    }

    public void SwitchToPage2()
    {
        Page2.SetAsLastSibling();
    }
}
