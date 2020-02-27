using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HPPickUP : MonoBehaviour
{
    public bool permanet;
    public float speed;
    public Vector3 rotation;
    public GameObject pickUpText;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(rotation);
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().Heal(1, permanet);
            GameObject text = Instantiate(pickUpText, transform.position, Quaternion.identity) as GameObject;
        
                if (permanet)
                {
                    text.GetComponent<TextMeshPro>().text = "Health UP";
                }
                else
                {
                    text.GetComponent<TextMeshPro>().text = "1 HP";
                }
            Destroy(gameObject);
        }
    }
}
