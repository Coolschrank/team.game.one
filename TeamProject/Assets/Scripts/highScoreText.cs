using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class highScoreText : MonoBehaviour
{
    HighScore[] hScore;
    HighScore cScore;
    int iScore =0;
    Text text;



    void Start()
    {
        hScore = FindObjectsOfType<HighScore>();
        foreach(HighScore score in hScore)
        {
            if(score.highScore >= iScore)
            {
                iScore = score.highScore;
                cScore = score;
            }
        }
        
        text = GetComponent<Text>();
    }

    
    void Update()
    {
        if (text == null)
            return;
        text.text = "HighScore\n " + cScore.highScore.ToString();
    }

    


}
