using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    public GameObject gameOverAnimation, damageAnimation;
    public Vector3 gameOverRotation;
    public int HP, maxHP;
    public float IFrames, iFramesLook;
    public bool IFramesBool, visibility= true, alive = true;
    public GameObject mesh;
    BoxCollider hitBox;
    
    
    void Start()
    {
        hitBox = GetComponent<BoxCollider>();
        HP = maxHP;
    }

    IEnumerator Visibility()
    {
        while (IFramesBool)
        {
            visibility = !visibility;
            yield return new WaitForSeconds(iFramesLook);
        }
        visibility = true;


    }

    private void Update()
    {
        mesh.SetActive(visibility);
    }

    public void TakeDamage()
    {
        if (IFramesBool)
            return;
            HP--;
        FindObjectOfType<PlayerUI>().takeDamage();
        if (HP > 0)
        {
            GameObject explosion = Instantiate(damageAnimation, transform.position, Quaternion.Euler(gameOverRotation)) as GameObject;
            StartCoroutine(Invincibility());
        }
        else
            GameOver();
    }

    IEnumerator Invincibility()
    {
        //hitBox.isTrigger = true;
        IFramesBool = true;
        StartCoroutine(Visibility());
        yield return new WaitForSeconds(IFrames);
        IFramesBool = false;
        //hitBox.isTrigger = false;

    }

    public void GameOver()
    {
        FindObjectOfType<HighScore>().AddScore(FindObjectOfType<DropRate>().score);
        GameObject explosion = Instantiate(gameOverAnimation, transform.position, Quaternion.Euler(gameOverRotation)) as GameObject;
        FindObjectOfType<SceneLoarder>().StartNextScene();
        Destroy(gameObject);
    }

    public void Heal(int healtGain, bool p)
    {
        HP += healtGain;
        if(p)
        {
            maxHP += healtGain;
        }
        if(HP >= maxHP)
        {
            HP = maxHP;
        }
        FindObjectOfType<PlayerUI>().gainHP();
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="Enemy")
        {
            TakeDamage();
        }
    }
    */

}
