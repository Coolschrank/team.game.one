using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackUpgrade : MonoBehaviour
{
    
    public int ammoPlus;
    public float speed, fireRateUP;
    public Vector3 rotation;
    public GameObject pickUpText;
    Rigidbody rb;

    void Start()
    {
        transform.rotation = Quaternion.Euler(rotation);
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject text = Instantiate(pickUpText, transform.position, Quaternion.identity) as GameObject;
            text.GetComponent<TextMeshPro>().text = "Fire Rate UP";
            other.gameObject.GetComponent<PlayerGun>().fireRate = other.gameObject.GetComponent<PlayerGun>().fireRate / fireRateUP;
            other.gameObject.GetComponent<PlayerGun>().maxAmmo += ammoPlus;



            Destroy(gameObject);
        }
    }
}
