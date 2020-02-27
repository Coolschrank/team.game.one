using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : MonoBehaviour
{
    public GameObject shot;
    public float spreadDis;
    float rb2n, rb1n, rb1, rb2;

    private void Awake()
    {
        rb2n = transform.eulerAngles.y - spreadDis*2;
        rb1n = transform.eulerAngles.y - spreadDis;
        rb1 = transform.eulerAngles.y + spreadDis;
        rb2 = transform.eulerAngles.y + spreadDis * 2;
    }

    void Start()
    {




        GameObject b2n = Instantiate(shot, transform.position, Quaternion.Euler(0,rb2n,0)) as GameObject;
        GameObject b1n = Instantiate(shot, transform.position, Quaternion.Euler(0, rb1n, 0)) as GameObject;
        GameObject b1 = Instantiate(shot, transform.position, Quaternion.Euler(0, rb1, 0)) as GameObject;
        GameObject b2 = Instantiate(shot, transform.position, Quaternion.Euler(0, rb2, 0)) as GameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
