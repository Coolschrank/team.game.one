using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    public bool mediKit, permanet;
    public int SP;
    public float speed;
    public Vector3 rotation;
    public GameObject pickUpText;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(SP > 0&& !permanet)
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
        if(other.gameObject.tag == "Player")
        {
            if(mediKit)
            {
                other.gameObject.GetComponent<PlayerHealth>().Heal(1,permanet);
            }
            other.gameObject.GetComponent<PlayerGun>().SPGain(SP,permanet);
            other.gameObject.GetComponent<PlayerGun>().SetUIColor();
            GameObject text = Instantiate(pickUpText, transform.position, Quaternion.identity) as GameObject;
            if (SP != 0)
            {
                if(permanet)
                {
                    text.GetComponent<TextMeshPro>().text = "Energy UP";
                }
                else
                {
                    text.GetComponent<TextMeshPro>().text = SP + " EP";
                }
               
            }
            else
            {
                if (permanet)
                {
                    text.GetComponent<TextMeshPro>().text = "Health UP";
                }
                else
                {
                    text.GetComponent<TextMeshPro>().text = "1 HP";
                }
                
            }

            Destroy(gameObject);
        }
    }
}
