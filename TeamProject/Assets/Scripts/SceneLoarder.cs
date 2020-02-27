using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoarder : MonoBehaviour
{
    public int timeTillNextScene;

    HighScore[] hScore;
    HighScore cScore;
    int iScore = 0;



    void Awake()
    {
        hScore = FindObjectsOfType<HighScore>();
        foreach (HighScore score in hScore)
        {
            if (score.highScore >= iScore)
            {
                iScore = score.highScore;
                cScore = score;
            }
        }
        if (SceneManager.GetActiveScene().name == "Game")
            cScore.SpawnPlayer();
    }


    public void StartNextScene()
    {
        StartCoroutine(Re());
    }

    IEnumerator Re()
    {
        yield return new WaitForSeconds(timeTillNextScene);
        FindObjectOfType<HighScore>().NextScene(0);
    }
}
