using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Vector3 origin;
    Vector3 direction;
    Vector3 hitPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CameraTransform();
    }

    void CameraTransform()
    {
        origin = transform.parent.position + new Vector3(0, 1);
        direction = transform.parent.right;
        Ray ray = new Ray(origin, direction);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 7))
        {
            hitPoint = hit.point;
        }
        if (hit.collider != null && transform.parent.InverseTransformPoint(hitPoint).x < 2)
        {
            transform.localPosition = new Vector3(transform.parent.InverseTransformPoint(hitPoint).x, 1);
        }
        else
        {
            transform.localPosition = new Vector3(2, 1);
        }
    }
}