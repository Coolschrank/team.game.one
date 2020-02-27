using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shild : MonoBehaviour
{
    public float stayActiveTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, stayActiveTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
