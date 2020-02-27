using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EPPickUP : MonoBehaviour
{
    public bool permanet;
    public int SP;
    public float speed;
    public Vector3 rotation;
    public GameObject pickUpText;
    Rigidbody rb;

    void Start()
    {
        transform.rotation = Quaternion.Euler(rotation);
        rb = GetComponent<Rigidbody>();
        if (SP > 0 && !permanet)
        {
            SP = FindObjectOfType<PlayerGun>().SPG;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerGun>().SPGain(SP, permanet);
            other.gameObject.GetComponent<PlayerGun>().SetUIColor();
            GameObject text = Instantiate(pickUpText, transform.position, Quaternion.identity) as GameObject;
            if (SP != 0)
            {
                if (permanet)
                {
                    text.GetComponent<TextMeshPro>().text = "Energy UP";
                }
                else
                {
                    text.GetComponent<TextMeshPro>().text = SP + " EP";
                }

            }
            Destroy(gameObject);
        }
    }
}
