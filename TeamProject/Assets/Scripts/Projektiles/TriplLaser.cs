using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriplLaser : MonoBehaviour
{
    public GameObject laser;
    public float rotation;
    // Start is called before the first frame update
    void Start()
    {
        GameObject LaserL = Instantiate(laser, transform.position, Quaternion.Euler(0,180 -rotation, 0)) as GameObject;
        GameObject LaserR = Instantiate(laser, transform.position, Quaternion.Euler(0,180 +rotation,0)) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
