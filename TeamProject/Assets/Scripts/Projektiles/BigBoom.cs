using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BigBoom : MonoBehaviour
{
    public int damage;
    public float delay, nukeTime, coolDown;
    public Color nukeColor;
    Color normalColor;
    Camera mainCam;


    void Start()
    {
        StartCoroutine(ThirdImpact());
        FindObjectOfType<PlayerGun>().NukeCoolDown(coolDown);
    }

    IEnumerator ThirdImpact()
    {
        yield return new WaitForSeconds(delay);
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        foreach(EnemyHealth enemy in enemies)
        {
            enemy.TakeDamage(damage, gameObject);
        }
        LaserBasic[] lasers = FindObjectsOfType<LaserBasic>();
        foreach (LaserBasic laser in lasers)
        {
            if(!laser.fromPlayer)
            {
                Destroy(laser.gameObject);
            }
        }
        StartCoroutine(ScreenChange());
    }

    IEnumerator ScreenChange()
    {
        Camera[] cams = FindObjectsOfType<Camera>();
        foreach(Camera cam in cams)
        {
            if (cam.tag == "MainCamera")
                mainCam = cam;
        }
        normalColor = mainCam.backgroundColor;
        mainCam.backgroundColor = nukeColor;

        yield return new WaitForSeconds(nukeTime);
        mainCam.backgroundColor = normalColor;
        Destroy(gameObject);
    }
}
