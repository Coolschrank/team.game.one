using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed, tilt;
    public Path path;
    public Transform[] points;
    int currentPoint = 1;
    public bool reverse;
    Vector3 reverseMovement;
    Rigidbody rb;
    

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        points = path.GetComponentsInChildren<Transform>();
        if (reverse)
        {
            var RP = points[currentPoint].transform.position;
            transform.position = new Vector3(-RP.x, RP.y, RP.z);
            ReverseMovement();
        }
        else
        {
            transform.position = points[1].transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (reverse)
        {
            rb.transform.position = Vector3.MoveTowards(transform.position, reverseMovement, speed * Time.deltaTime);
            if (transform.position == reverseMovement)
            {
                currentPoint++;
                if (currentPoint >= points.Length - 1)
                {
                    EnemyDissolve();
                }
                else
                {
                    ReverseMovement();
                }
                
            }
        }
        else
        {
            rb.transform.position = Vector3.MoveTowards(transform.position, points[currentPoint + 1].transform.position, speed * Time.deltaTime);
            if (transform.position == points[currentPoint + 1].transform.position )
            {
                currentPoint++;
                if (currentPoint >= points.Length - 1)
                {
                    EnemyDissolve();
                }
            }
        }
       
    }

    public void ReverseMovement()
    {
        var RP = points[currentPoint + 1].transform.position;
        reverseMovement = new Vector3(-RP.x, RP.y, RP.z);
    }

    public void EnemyDissolve()
    {
        Destroy(gameObject);
    }
}
