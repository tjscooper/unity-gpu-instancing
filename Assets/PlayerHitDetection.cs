
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 1))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
            print(hitInfo.transform.gameObject.tag);
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 1, Color.green);
        }
    }
}