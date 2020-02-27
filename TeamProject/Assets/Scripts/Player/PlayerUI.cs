using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public BoxCollider[] healthPoints;
    PlayerHealth player;
    public Material healthColor,damageColor, noHealthColor;
    public float damageTime;


    void Start()
    {
        healthPoints = GetComponentsInChildren<BoxCollider>();
        player = FindObjectOfType<PlayerHealth>();
    }

    public void takeDamage()
    {
        healthPoints[player.HP].gameObject.GetComponent<Renderer>().material = noHealthColor;

        for(int i = 0; i <= player.HP;i++)
        {
            var healthPoint = healthPoints[i];
                StartCoroutine(DamageColor(healthPoint, healthPoint.gameObject.GetComponent<Renderer>().material));
        }
    }

    public void gainHP()
    {
        healthPoints[player.HP-1].gameObject.GetComponent<Renderer>().material = healthColor;
    }

    IEnumerator DamageColor(BoxCollider hpp, Material ogColor)
    {
            hpp.gameObject.GetComponent<Renderer>().material = damageColor;
            yield return new WaitForSeconds(damageTime);
            hpp.gameObject.GetComponent<Renderer>().material = ogColor;
            SetColor();
    }

    public void SetColor()
    {
        for (int i = 0; i <= player.HP-1; i++)
        {
            
            healthPoints[i].gameObject.GetComponent<Renderer>().material = healthColor;
        }
    }
}
