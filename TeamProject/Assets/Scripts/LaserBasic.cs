using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBasic : MonoBehaviour
{
    Rigidbody rb;
    public int damage;
    public float speed;
    public bool fromPlayer, powerAmor;
    void Start()
    {
        if (FindObjectOfType<HighScore>().easyMode&& !fromPlayer)
        {
            speed = speed * FindObjectOfType<HighScore>().shotSlowDown;
        }

        rb = GetComponent<Rigidbody>();
        if(FindObjectOfType<PlayerGun>().inTimeSlowDown && fromPlayer)
        {
            speed = speed / FindObjectOfType<TimeSlowDown>().timeSlowDown;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyHealth>()&& fromPlayer&& damage>0)
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage,gameObject);
            if(!powerAmor)
                Destroy(gameObject);
        }
        if (other.gameObject.GetComponent<PlayerHealth>() && !fromPlayer)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage();
            if (!powerAmor)
                Destroy(gameObject);
        }
        if (other.gameObject.GetComponent<LaserBasic>() && powerAmor)
        {
            if(other.gameObject.GetComponent<LaserBasic>().fromPlayer == false)
            Destroy(other.gameObject);
        }
        /*if (other.gameObject.tag == "Enemy" && fromPlayer)
        {
            other.GetComponent<enemy>().GoodbyeStranger();
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player" && !fromPlayer)
        {
            other.GetComponent<playerHealth>().Shrek();
            Destroy(gameObject);
        }
        */
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BoundaryBullet" )
        {
            Destroy(gameObject);
        }
    }
}
