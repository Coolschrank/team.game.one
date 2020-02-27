using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PysEnemyMovement : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    public float rotateSpeed,speed,gravitySpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerHealth>().gameObject;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerDirection = player.transform.position - transform.position;
        float step = rotateSpeed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, playerDirection, step, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void FixedUpdate()
    {
        Vector3 move = transform.forward * speed;
        rb.AddForce(move + new Vector3(0,0, -gravitySpeed));
    }
}
