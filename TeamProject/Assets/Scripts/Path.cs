using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public bool reverse;

    void Start()
    {
        if(reverse)
        {
            Reverse();
        }
    }

    public void Reverse()
    {
        Transform[] points = GetComponentsInChildren<Transform>();
        foreach (Transform point in points)
        {
            point.transform.position = new Vector3(-point.transform.position.x, point.transform.position.y, point.transform.position.z);
        }
    }
}
