using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed, tilt, xMove, zMove, timeMultiplier;
    Rigidbody rb;
    CapsuleCollider dashBox;
    Vector3 dashDirection;

    public float dashTime, dashSpeed;
    public bool inDash;
    

    void Start()
    {
        timeMultiplier = 1f; 
        rb = GetComponent<Rigidbody>();
        dashBox = GetComponentInChildren<CapsuleCollider>();
        dashBox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // movement with controler and keybord.
        xMove = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");
        
        if (xMove==0)
        {
            xMove = Input.GetAxisRaw("Horizontal2");
        }
        if (zMove == 0)
        {
            zMove = Input.GetAxisRaw("Vertical2");
        }
        if (xMove == 0)
        {
            xMove = Input.GetAxisRaw("Horizontal3");
        }
        if (zMove == 0)
        {
            zMove = Input.GetAxisRaw("Vertical3");
        }


    }

    private void FixedUpdate()
    {
        if (inDash)
        {
            dashBox.transform.position = transform.position;
            rb.velocity =dashDirection * timeMultiplier;
        }
        else
        {
            rb.AddForce(new Vector3(xMove * playerSpeed *timeMultiplier, 0, zMove * playerSpeed * timeMultiplier));
        }
        // rb.velocity = (new Vector3(xMove * playerSpeed, 0, zMove * playerSpeed));

        transform.rotation = Quaternion.Euler(new Vector3(0, rb.velocity.x * tilt/ timeMultiplier, 0));

        /*if (transform.position.x > xMaxDis)
            transform.position = new Vector3(xMaxDis, 0, transform.position.z);
        else if (transform.position.x < -xMaxDis)
            transform.position = new Vector3(-xMaxDis, 0, transform.position.z);

        if (transform.position.z > zMaxDis)
            transform.position = new Vector3(transform.position.x, 0, zMaxDis);
        else if (transform.position.z < -zMaxDis)
            transform.position = new Vector3(transform.position.x, 0, -zMaxDis);
            */
    }

    public void Dash()
    {
        
        dashDirection = new Vector3(xMove * dashSpeed, 0f, zMove*dashSpeed);
        StartCoroutine(InDash());
    }

    IEnumerator InDash()
    {
        inDash = true;
        dashBox.gameObject.SetActive(true);
        yield return new WaitForSeconds(dashTime / timeMultiplier);
        inDash = false;
        dashBox.gameObject.SetActive(false);
    }
}
