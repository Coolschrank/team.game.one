using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpText : MonoBehaviour
{
    public Vector3 rotation, particalRotation;
    public GameObject partical;
    public float aliveTime;
    void Start()
    {
        transform.rotation = Quaternion.Euler(rotation);
        Destroy(gameObject,aliveTime);
        Instantiate(partical, transform.position, Quaternion.Euler(particalRotation));
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
