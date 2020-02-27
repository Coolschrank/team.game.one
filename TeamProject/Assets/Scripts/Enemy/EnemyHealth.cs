using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int HP, score;
    public float DATime;
    public GameObject KOAnimation;
    public Vector3 KORotation;
    public Material damageMaterial;
    public MeshRenderer mesh;
    Material[] damageMesh, normalMesh;
    GameObject lastplayerShot; // for when a shot hits twice

    private void Start()
    {
        damageMesh = new Material[mesh.materials.Length];
        normalMesh = new Material[mesh.materials.Length];
        for (int i = 0; i < mesh.materials.Length; i++)
        {
            normalMesh[i] = mesh.materials[i];
            damageMesh[i] = damageMaterial;
        }
    }
    public void TakeDamage(int damage, GameObject playerShot)
    {
        if (playerShot == lastplayerShot)
        {
            return;
        }
        HP -= damage;
        if (HP <= 0)
            KO();
        else
        {
            lastplayerShot = playerShot;
            StartCoroutine(DamageAnimation());
        }
    }

    public void KO()
    {
        GameObject item = FindObjectOfType<DropRate>().ScoreEnemy(score);
        if(item != null)
        {
            GameObject itemDrop = Instantiate(item, transform.position,Quaternion.identity) as GameObject;
        }
        GameObject explosion = Instantiate(KOAnimation, transform.position, Quaternion.Euler(KORotation)) as GameObject;
        Destroy(gameObject);
    }

    IEnumerator DamageAnimation()
    {
        mesh.materials = damageMesh;
        yield return new WaitForSeconds(DATime);
        mesh.materials = normalMesh;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage();
        }
    }


}
