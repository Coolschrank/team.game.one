using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionTime;
    public GameObject partical;
    public Vector3 pRotation;
    // Start is called before the first frame update
    void Start()
    {
        GameObject p = Instantiate(partical, transform.position, Quaternion.Euler(pRotation)) as GameObject;
        Destroy(p, 2f);
        Destroy(gameObject, explosionTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
