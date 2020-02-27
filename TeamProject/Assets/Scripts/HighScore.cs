using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour
{
    public float sceneTranisitionTime;
    public GameObject transition;

    public GameObject currentShip;
    public Vector3 shipPosition;

    public int highScore;
    HighScore[] score;
    GameObject text;


    public bool easyMode;
    public float spawnSlowDown, shotSlowDown, enemyFireRateSlowDown;
    

    private void Awake()
    {
        score = FindObjectsOfType<HighScore>();
        if (score.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddScore(int score)
    {
        if(highScore < score)
        {
            highScore = score;
        }
    }

    public void SpawnPlayer()
    {
        GameObject player = Instantiate(currentShip, shipPosition, Quaternion.identity) as GameObject;
    }

    public void NextScene(int i)
    {
        StartCoroutine(LoadScene(i));
    }

    public IEnumerator LoadScene(int i)
    {
        transition.GetComponent<Animator>().SetBool("StartFade", true);
        yield return new WaitForSeconds(sceneTranisitionTime);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + i);
        transition.GetComponent<Animator>().SetBool("StartFade", false);
    }


}
