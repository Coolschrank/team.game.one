using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemAbility : MonoBehaviour
{
    public GameObject[] abilities;
    public GameObject itemText, pickUpText;
    public Vector3 rotation;
    public float speed;
    Rigidbody rb;
    GameObject abilitiy;
    PlayerGun player;
    
    void Start()
    {
        player = FindObjectOfType<PlayerGun>();
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(rotation);
        SetAbility();
    }

    public void SetAbility()
    {

        abilitiy = abilities[UnityEngine.Random.Range(0, abilities.Length)];
        if(abilitiy == player.shipAbility)
        {
            SetAbility();
            return;
        }
        if(player.item != null)
        {
                if(player.item == abilitiy)
            {
                SetAbility();
                return;
            }
        }
        
        itemText.GetComponent<TextMeshPro>().text = abilitiy.name;
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var playerG = other.gameObject.GetComponent<PlayerGun>();
            playerG.item = abilitiy;
            playerG.SetAbility();
            playerG.SetUIColor();
            GameObject text = Instantiate(pickUpText, transform.position, Quaternion.identity) as GameObject;
            text.GetComponent<TextMeshPro>().text = abilitiy.GetComponent<Ability>().abilityName;
            Destroy(gameObject);
        }
    }
}
