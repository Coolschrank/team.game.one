using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetDifficulty : MonoBehaviour
{
    

    public void easy()
    {
        FindObjectOfType<HighScore>().easyMode = true;
        FindObjectOfType<HighScore>().NextScene(1);
    }

    public void hard()
    {
        FindObjectOfType<HighScore>().easyMode = false;
        FindObjectOfType<HighScore>().NextScene(1);
    }

    
}
