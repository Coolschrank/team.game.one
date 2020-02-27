using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    GameObject player;
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerHealth>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            return;
        }
        Vector3 playerDirection = player.transform.position - transform.position;
        float step = rotateSpeed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, playerDirection, step, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
