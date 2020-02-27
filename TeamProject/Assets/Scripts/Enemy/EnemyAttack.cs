using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject eBullet, enemyGun;
    public float coolDown, startUpTime, startUpTimeR, coolDownAS;
    public int multiShots;
    public bool alive;
    int i = 0;
    

    void Start()
    {
        var easy = FindObjectOfType<HighScore>();
        if (easy.easyMode)
        {
            coolDown = coolDown * easy.enemyFireRateSlowDown;
            startUpTime = startUpTime * easy.enemyFireRateSlowDown;
            startUpTimeR = startUpTimeR * easy.enemyFireRateSlowDown;

        }
        transform.rotation = Quaternion.Euler(0, 180, 0);
        StartCoroutine(FirstShot());
    }

    
    IEnumerator ShootBullet()
    {
        while (alive)
        {
            yield return new WaitForSeconds(coolDown);
            GameObject bullet = Instantiate(eBullet, enemyGun.transform.position, enemyGun.transform.rotation) as GameObject;
            if (multiShots > 0)
            {
                StartCoroutine(AdditonalShots());
            }
        }
    }

    IEnumerator FirstShot()
    {
            yield return new WaitForSeconds(startUpTime + UnityEngine.Random.Range(0,startUpTimeR));
            GameObject bullet = Instantiate(eBullet, enemyGun.transform.position, enemyGun.transform.rotation) as GameObject;
        if(multiShots>0)
        {
            StartCoroutine(AdditonalShots());
        }
            StartCoroutine(ShootBullet());
    }

    IEnumerator AdditonalShots()
    {
        for (int i = 1; i < multiShots; i++)
        {
            yield return new WaitForSeconds(coolDownAS);
            GameObject bullet = Instantiate(eBullet, enemyGun.transform.position, enemyGun.transform.rotation) as GameObject;
        }
    }
}
